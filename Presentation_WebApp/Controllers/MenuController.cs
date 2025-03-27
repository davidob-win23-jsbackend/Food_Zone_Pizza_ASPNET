using Microsoft.AspNetCore.Mvc;
using Infrastructure.Models;

namespace Presentation_WebApp.Controllers
{
    public class MenuController : Controller
    {
        [Route("/menu-page")]
        public IActionResult Menu()
        {
            var pizzas = new List<Pizza>
        {
            new Pizza { Id = 1, Name = "California Pizza", Description = "California Style Pizza", Price = 89, ImageUrl = "/images/pizza-images/california-style-pizza.jpg" },
            new Pizza { Id = 2, Name = "Chicago pizza", Description = "Chicago Style Pizza", Price = 99, ImageUrl = "/images/pizza-images/chicago-pizza.jpg" },
            new Pizza { Id = 3, Name = "Detroit Pizza", Description = "Detroit Style Pizza", Price = 95, ImageUrl = "/images/pizza-images/detroit-style-pizza.jpg" },
            new Pizza { Id = 4, Name = "Greek Pizza", Description = "Greek Style Pizza", Price = 95, ImageUrl = "/images/pizza-images/greek-pizza.jpg" },
            new Pizza { Id = 5, Name = "Neapolita Pizza", Description = "Neapolita Style Pizza", Price = 95, ImageUrl = "/images/pizza-images/neapolitan-pizza.jpg" },
            new Pizza { Id = 6, Name = "New york Pizza", Description = "New York Style Pizza", Price = 95, ImageUrl = "/images/pizza-images/ny-style-pizza.jpg" },
            new Pizza { Id = 7, Name = "Pepperoni Pizza", Description = "Pepperoni Style Pizza", Price = 95, ImageUrl = "/images/pizza-images/pepperoni-pizza.jpg" },
            new Pizza { Id = 8, Name = "Roman Pizza", Description = "Roman Style Pizza", Price = 89, ImageUrl = "/images/pizza-images/roman-style-pizza.jpg" },
            new Pizza { Id = 9, Name = "Sicilian Pizza", Description = "Sicilian Style Pizza", Price = 89, ImageUrl = "/images/pizza-images/sicilian_pizza.jpg" },
            new Pizza { Id = 10, Name = "St Louis Pizza", Description = "St Louis Style Pizza", Price = 95, ImageUrl = "/images/pizza-images/st-louis-style-pizza.jpg" },
        };

            ViewBag.Pizzas = pizzas;
            return View("Menu");
        }
    }
}
