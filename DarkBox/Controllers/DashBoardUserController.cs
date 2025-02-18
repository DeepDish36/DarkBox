using Microsoft.AspNetCore.Mvc;

namespace DarkBox.Controllers
{
    public class DashBoardUserController : Controller
    {
        public IActionResult DashBoardClient()
        {
            return View();
        }
    }
}
