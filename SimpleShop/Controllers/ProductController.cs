using Microsoft.AspNetCore.Mvc;
using SimpleShop.Models;

namespace SimpleShop.Controllers;

public class ProductController : Controller
{
    private static List<Product> products = new List<Product>
    {
        new Product { Id = 1, Name = "Laptop", Price = 3000 },
        new Product {Id = 2, Name = "Telefon", Price = 4000}
    };
    
    // GET
    public IActionResult Index()
    {
        return View(products);
    }
}