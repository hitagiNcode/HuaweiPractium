using Microsoft.AspNetCore.Mvc;

namespace CertiEx.Web.Controllers
{
    public class ScoreController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Result()
        {
            return View();
        }
    }
}
