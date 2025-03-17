using DarkBox.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using DarkBox.Services;

namespace DarkBox.Controllers
{
    [Authorize]
    public class ProjectRequestController : Controller
    {
        private readonly AppDbContext _context;
        private readonly INotificationsService _notificationsService; // Adicionado
        private readonly IEmailService _emailService; // Adicionado

        public ProjectRequestController(AppDbContext context, INotificationsService notificationsService, IEmailService emailService) // Modificado
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
            if (ModelState.IsValid)
            {
                _context.ProjectRequests.Add(request);
                await _context.SaveChangesAsync();

                // Notificação no site
                await _notificationsService.SendNotification(request.ClientId, "Seu pedido de projeto foi recebido!");

                // Envio de e-mail
                string emailBody = $"<p>Olá, seu pedido <strong>{request.RequestedTitle}</strong> foi recebido!</p>";
                await _emailService.SendEmailAsync("email_do_cliente@gmail.com", "Pedido Recebido!", emailBody);

                return RedirectToAction("MeusPedidos");
            }
            return View(request);
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

