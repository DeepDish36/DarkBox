using Microsoft.AspNetCore.Mvc;

namespace DarkBox.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
