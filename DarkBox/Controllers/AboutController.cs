using Microsoft.AspNetCore.Mvc;

namespace DarkBox.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult About()
        {
            return View();
        }
    }
}
