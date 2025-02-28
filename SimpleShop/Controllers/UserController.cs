using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleShop.Models;

namespace SimpleShop.Controllers;

public class UserController(AppDbContext context) : Controller
{
    private readonly AppDbContext _context = context;
    // GET
    public IActionResult Index()
    {
        return View(null);
    }

    [Authorize]
    public IActionResult Profile()
    {
        var userId = User.FindFirst("UserId");

        if (userId == null) return RedirectToAction("Index" ,"Home");
        
        return View(userId);
    }
}