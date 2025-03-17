using DarkBox.Models;
using Microsoft.AspNetCore.Mvc;

namespace DarkBox.Controllers
{
    public class SearchController : Controller
    {
        private readonly AppDbContext _context;

        public SearchController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Search(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return View(new PesquisaViewModel { Users = new List<User>(), Projetos = new List<Project>() });
            }

            var usuarios = _context.Users
                .Where(u => u.Username.Contains(query) || u.Email.Contains(query))
                .ToList();

            var projetos = _context.Projects
                .Where(p => p.Title.Contains(query) || p.Description.Contains(query))
                .ToList();

            var viewModel = new PesquisaViewModel
            {
                Users = usuarios,
                Projetos = projetos,
                Query = query
            };

            return View(viewModel);
        }
    }
}
