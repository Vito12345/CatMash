using Microsoft.AspNetCore.Mvc;

namespace CatMash.UI.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Vote()
        {
            return new JsonResult(new { success = true });
        }
    }
}
