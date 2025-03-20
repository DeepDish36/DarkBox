using DarkBox.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace DarkBox.ViewComponents
{
    public class ProjetosSidebarViewComponent : ViewComponent
    {
        private readonly AppDbContext _context;

        public ProjetosSidebarViewComponent(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return View(new List<ProjectRequest>());
            }

            var userId = int.Parse(((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");

            var projetos = await _context.ProjectRequests
                .Where(p => p.Status == "in_progress" && p.ClientId == userId)
                .ToListAsync();

            return View(projetos);
        }
    }
}
