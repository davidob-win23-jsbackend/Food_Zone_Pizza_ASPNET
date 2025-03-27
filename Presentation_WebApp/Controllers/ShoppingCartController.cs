using Microsoft.AspNetCore.Mvc;

namespace Presentation_WebApp.Controllers;

public class ShoppingCartController : Controller
{
    [Route("/cart")]
    public IActionResult Index()
    {
        return View();
    }

    [Route("/cart/paymentmethod")]
    public IActionResult PaymentMethod()
    {
        return View();
    }

    [Route("/cart/paymentdetails")]
    public IActionResult PaymentDetails()
    {
        return View();
    }
}
