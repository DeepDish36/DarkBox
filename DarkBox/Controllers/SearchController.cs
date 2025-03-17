using DarkBox.Models;
using Microsoft.AspNetCore.Mvc;

namespace DarkBox.Controllers
{
    [ApiController]
    [Route("api/search")]
    public class SearchController : Controller
    {
        private readonly AppDbContext _context;

        public SearchController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Search([FromQuery] string query)
        {
            if (string.IsNullOrEmpty(query))
                return BadRequest("A pesquisa não pode estar vazia.");

            var users = _context.Users
                .Where(u => u.Username.Contains(query))
                .Select(u => new { u.UserId, u.Username})
                .ToList();

            var projects = _context.Projects
                .Where(p => p.Title.Contains(query))
                .Select(p => new { p.ProjectId, p.Title })
                .ToList();

            return Ok(new { users, projects });
        }
    }
}
