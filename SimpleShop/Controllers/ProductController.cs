using Microsoft.AspNetCore.Mvc;
using SimpleShop.Models;

namespace SimpleShop.Controllers;

public class ProductController : Controller
{
    private static List<Products> products = new List<Products>
    {
        new Products { Id = 1, Name = "Laptop", Price = 3000 },
        new Products {Id = 2, Name = "Telefon", Price = 4000}
    };
    
    // GET
    public IActionResult Index()
    {
        return View(products);
    }
}