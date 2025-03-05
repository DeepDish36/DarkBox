using DarkBox.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DarkBox.Controllers
{
    public class CriarContaController : Controller
    {
        private readonly AppDbContext _context;

        public CriarContaController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult CriarConta()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CriarConta(SignupViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Verifica se o email já está cadastrado
            var usuarioExistente = _context.Users.FirstOrDefault(u => u.Email == model.Email);
            if (usuarioExistente != null)
            {
                ModelState.AddModelError("Email", "Este email já está cadastrado.");
                return View(model);
            }

            // Criptografa a senha com BCrypt
            string senhaCriptografada = BCrypt.Net.BCrypt.HashPassword(model.Senha);

            // Criar um novo usuário
            var novoUsuario = new User
            {
                Username = model.Nome,
                Email = model.Email,
                PasswordHash = senhaCriptografada, // Senha armazenada de forma segura
                SubscriptionPlanId = 1,
                CreatedAt = DateTime.UtcNow
            };

            _context.Users.Add(novoUsuario);
            await _context.SaveChangesAsync();

            // Redirecionar para login após cadastro bem-sucedido
            return RedirectToAction("Login", "Login");
        }
    }
}
