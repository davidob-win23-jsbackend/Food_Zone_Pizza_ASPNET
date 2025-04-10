using Microsoft.AspNetCore.Mvc;

namespace Presentation_WebApp.Controllers
{
    public class ChatController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
