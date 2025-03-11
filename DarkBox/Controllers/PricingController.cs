using Microsoft.AspNetCore.Mvc;

namespace DarkBox.Controllers
{
    public class PricingController : Controller
    {
        [HttpGet]
        public IActionResult Pricing()
        {
            return View();
        }
    }
}
