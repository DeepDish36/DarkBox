using DarkBox.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DarkBox.Controllers
{
    public class LoginController : Controller
    {
        private readonly AppDbContext _context;

        public LoginController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                ViewBag.Error = "E-mail e senha são obrigatórios!";
                return View();
            }

            var emailNormalizado = email.Trim().ToLower();

            // Verifica se o usuário existe no banco de dados
            var user = _context.Users.FirstOrDefault(u => u.Email.Trim().ToLower() == emailNormalizado);

            // Verifica se o usuário existe e se a senha está correta usando BCrypt
            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            {
                ViewBag.Error = "E-mail ou senha inválidos!";
                return View();
            }

            // Criar os Claims (dados da sessão)
            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, user.Username),
        new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
        new Claim(ClaimTypes.Email, emailNormalizado),
        new Claim(ClaimTypes.Role, user.Role ?? "user") // Adiciona o papel do usuário
    };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            // Criar a sessão persistente
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true, // Mantém a sessão após fechar o navegador
                ExpiresUtc = DateTime.UtcNow.AddHours(1) // Expira em 1 hora
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                claimsPrincipal,
                authProperties);

            // Redirecionamento com base no papel do usuário
            return user.Role switch
            {
                "client" => RedirectToAction("ClienteDashboard", "Dashboard"),
                "developer" => RedirectToAction("ProgramadorDashboard", "Dashboard"),
                "admin" => RedirectToAction("AdminDashboard", "Dashboard"),
                _ => RedirectToAction("Index", "Home") // Redireciona para a página inicial em caso de erro inesperado
            };
        }



        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login","Login");
        }
    }
}
