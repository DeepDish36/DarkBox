using DarkBox.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace DarkBox.Controllers
{
    public class LoginController : Controller
    {
        private readonly AppDbContext _context;

        public LoginController(AppDbContext context)
        {
            _context = context;
        }

        // Exibe a View de Login (GET)
        public IActionResult Login()
        {
            return View();
        }

        // Processa o login (POST)
        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // Busca o usuário na base de dados
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email);

            if (user == null || user.PasswordHash != password)
            {
                ModelState.AddModelError(string.Empty, "Email ou senha inválidos.");
                ViewBag.Email = email; // Para manter o email preenchido na view
                return View();
            }

            // Armazena a sessão do usuário
            HttpContext.Session.SetString("UserEmail", user.Email);
            return RedirectToAction("DashboardClient", "DashboardUser");
        }

        // Logout
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
