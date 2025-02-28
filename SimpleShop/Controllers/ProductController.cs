using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleShop.Models;

namespace SimpleShop.Controllers;

public class ProductController(AppDbContext context) : Controller
{
    private readonly AppDbContext _context = context;
    
    // GET
    [AllowAnonymous]
    public IActionResult Index()
    {
        return View(_context.Products.ToList());
    }
}