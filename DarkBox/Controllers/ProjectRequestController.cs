using DarkBox.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using DarkBox.Interfaces;
using DarkBox.Services;
using Microsoft.EntityFrameworkCore;

namespace DarkBox.Controllers
{
    [Authorize]
    public class ProjectRequestController : Controller
    {
        private readonly AppDbContext _context;
        private readonly INotificationService _notificationsService; // Adicionado
        private readonly IEmailService _emailService; // Adicionado

        public ProjectRequestController(AppDbContext context, INotificationService notificationsService, IEmailService emailService) // Modificado
        {
            _context = context;
            _notificationsService = notificationsService; // Adicionado
            _emailService = emailService; // Adicionado
        }

        public IActionResult NovoPedido()
        {
            return View();
        }

        // Processar o envio do pedido
        [HttpPost]
        public async Task<IActionResult> NovoPedido(ProjectRequest request)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Unauthorized(); // Garante que o usuário está autenticado
            }

            // Pega o ID do usuário logado
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");

            if (userId == 0)
            {
                ModelState.AddModelError("", "Erro ao identificar o usuário.");
                return View(request);
            }

            // Define automaticamente o ClientId com o usuário logado
            request.ClientId = userId;
            request.CreatedAt = DateTime.Now;
            _context.ProjectRequests.Add(request);
            await _context.SaveChangesAsync();

            // Buscar o cliente
            var cliente = await _context.Users
                .FirstOrDefaultAsync(u => u.UserId == userId);

            if (cliente == null)
            {
                ModelState.AddModelError("", "Erro ao identificar o cliente.");
                return View(request);
            }

            // Criar a mensagem da notificação
            string mensagem = $"{cliente.Username} efetuou um novo pedido.";

            // Buscar todos os programadores
            var programadores = await _context.Users
                .Where(u => u.Role == "Programador")
                .ToListAsync();

            // Enviar a notificação para cada programador
            foreach (var programador in programadores)
            {
                await _notificationsService.SendNotification(programador.UserId, mensagem);
            }

            return RedirectToAction("MeusPedidos"); // Ou outra página de sucesso
        }

        //Mostrar pedidos pendentes (programador)
        [HttpGet]
        public async Task<IActionResult> Pendentes()
        {
            var projetosPendentes = await _context.ProjectRequests
                .Where(p => p.Status == "pending")
                .Include(p => p.Client) // Para buscar os detalhes do cliente que fez o pedido
                .ToListAsync();

            return View(projetosPendentes);
        }

        [HttpPost]
        public async Task<IActionResult> Aceitar(int id)
        {
            var projeto = await _context.ProjectRequests.FindAsync(id);

            if (projeto == null)
            {
                return NotFound();
            }

            var programadorId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");

            // Atualiza o status e define o programador
            projeto.Status = "accepted";
            projeto.DeveloperId = programadorId;

            await _context.SaveChangesAsync();

            // Buscar o cliente do projeto
            var cliente = await _context.Users.FindAsync(projeto.ClientId);

            if (cliente != null)
            {
                // Enviar notificação via sistema
                await _notificationsService.SendNotification(cliente.UserId, "O seu projeto foi aceite!");

                // Verificar se o email do cliente não é nulo
                if (!string.IsNullOrEmpty(cliente.Email))
                {
                    // Enviar notificação por email
                    string mensagemEmail = $"Olá {cliente.Username}, o seu projeto '{projeto.RequestedTitle}' foi aceite por um programador!";
                    await _emailService.SendEmailAsync(cliente.Email, "Projeto Aceito", mensagemEmail);
                }
                else
                {
                    // Log ou tratamento de erro para email nulo
                    Console.WriteLine("O email do cliente é nulo. Não foi possível enviar a notificação por email.");
                }
            }

            return RedirectToAction("Pendentes"); // Redireciona de volta para a lista de pendentes
        }

        // Listar os pedidos do usuário
        public IActionResult MeusPedidos()
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)); // Pega o ID do usuário autenticado
            var pedidos = _context.ProjectRequests.Where(p => p.ClientId == userId).ToList();
            return View(pedidos);
        }

        // Listar todos os pedidos para um administrador
        [Authorize]
        public IActionResult GerirPedidos()
        {
            var tipouser = User.FindFirstValue("Role");

            if (tipouser != "admin")
            {
                return RedirectToAction("MeusPedidos");
            }

            var pedidos = _context.ProjectRequests.ToList();
            return View(pedidos);
        }

        // Aprovar ou rejeitar um pedido
        [HttpPost]
        [Authorize]
        public IActionResult AlterarStatus(int id, string status)
        {
            var tipouser = User.FindFirstValue("Role");

            if (tipouser != "admin")
            {
                return RedirectToAction("MeusPedidos");
            }

            var pedido = _context.ProjectRequests.Find(id);
            if (pedido != null)
            {
                pedido.Status = status;
                _context.SaveChanges();
            }
            return RedirectToAction("GerirPedidos");
        }

        // Excluir um pedido
        [HttpPost]
        [Authorize]
        public IActionResult Excluir(int id)
        {
            var tipouser = User.FindFirstValue("Role");

            if (tipouser != "admin")
            {
                return RedirectToAction("MeusPedidos");
            }

            var pedido = _context.ProjectRequests.Find(id);
            if (pedido != null)
            {
                _context.ProjectRequests.Remove(pedido);
                _context.SaveChanges();
            }
            return RedirectToAction("GerirPedidos");
        }
    }
}

