using Microsoft.AspNetCore.Mvc;
using Presentation_WebApp.ViewModels;

namespace Presentation_WebApp.Controllers;

public class OrderController : Controller
{
    [Route("/order")]
    public IActionResult Index()
    {
        return View();
    }

    [Route("/order-delivery")]
    public IActionResult Delivery()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Delivery(DeliveryViewModel model)
    {
        if (ModelState.IsValid)
        {
            // Save delivery details or process order
            return RedirectToAction("OrderConfirmation");
        }
        return View(model);
    }

    [Route("/order-pickup")]
    public IActionResult Pickup()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Pickup(PickupViewModel model)
    {
        if (ModelState.IsValid)
        {
            // Save pickup details or process order
            return RedirectToAction("OrderConfirmation");
        }
        return View(model);
    }

    public IActionResult OrderConfirmation()
    {
        return View();
    }
}
