using Microsoft.AspNetCore.Mvc;
using DarkBox.Models;
using Microsoft.EntityFrameworkCore;

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
            var projetos = await _context.ProjectRequests
                .Where(p => p.Status == "accepted")
                .ToListAsync();
            return View(projetos);
        }
    }
}
