using Microsoft.AspNetCore.Mvc;

namespace Presentation_WebApp.Controllers
{
    public class HomeController : Controller
    {
        [Route("/")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("/error")]
        public IActionResult Error404(int statusCode)
        {
            return View();
        }
    }
}
