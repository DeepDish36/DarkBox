using DarkBox.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace DarkBox.Controllers
{
    public class DashBoardController : Controller
    {
        private readonly AppDbContext _context;

        public DashBoardController(AppDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "client")]
        public IActionResult ClienteDashboard()
        {
            if (User.Identity?.IsAuthenticated != true)
            {
                return RedirectToAction("Login", "Login");
            }

            // 🔹 Recupera o ID e Nome do usuário das Claims
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var username = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;

            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId) || string.IsNullOrEmpty(username))
            {
                return RedirectToAction("Login", "Login"); // Redireciona caso falte informação
            }

            // 🔹 Obtém os projetos do usuário
            var projetos = _context.Projects
                .Where(p => p.ClientId == userId)
                .ToList();

            // 🔹 Obtém as notificações do usuário
            var notificacoes = _context.Notifications
                .Where(n => n.UserId == userId)
                .ToList();

            // 🔹 Define a ViewBag.Projetos para ser usada no Layout.cshtml
            ViewBag.Projetos = projetos;

            // Define os dados no ViewModel
            var model = new ClienteDashboardViewModel
            {
                NomeUsuario = username,
                Projetos = projetos,
                Notificacoes = notificacoes
            };

            return View(model);
        }

        [Authorize(Roles = "developer")]
        public async Task<IActionResult> ProgramadorDashboard()
        {
            // Verifica se o usuário está autenticado
            if (User.Identity?.IsAuthenticated != true)
            {
                return RedirectToAction("Login", "Login");
            }

            // Obtém o ID e Nome do usuário a partir das Claims
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var username = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;

            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId) || string.IsNullOrEmpty(username))
            {
                return RedirectToAction("Login", "Login"); // Redireciona se faltar alguma informação
            }

            // Obtém os projetos associados ao programador (onde o programador está como desenvolvedor)
            var projetos = await _context.Projects
                .Where(p => p.DeveloperId == userId) // Filtra pelos projetos onde o programador está envolvido
                .ToListAsync();

            // Cria o ViewModel para o Dashboard
            var model = new ProgramadorDashboardViewModel
            {
                NomeUsuario = username,
                Projetos = projetos
            };

            return View(model);
        }
    }
}
