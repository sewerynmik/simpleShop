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
        var userId = User.FindFirst("UserId")?.Value;
        
        Console.Write(userId);

        if (userId == null) return RedirectToAction("Index" ,"Home");

        var user = _context.Users.FirstOrDefault(u => u.Id.ToString() == userId);

        if (user == null) return NotFound();
        
        return View(user);
    }

    [Authorize]
    public IActionResult Edit()
    {
        var userId = User.FindFirst("UserId")?.Value;

        if (userId == null) return RedirectToAction("Index", "Home");

        var user = _context.Users.FirstOrDefault(u => u.Id.ToString() == userId);

        if (user == null) return NotFound();

        return View(user);
    }

    [Authorize]
    [HttpPut("{id}")]
    public IActionResult Edit(int id, [FromBody] Users updatedUser)
    {
        var user = _context.Users.Find(id);

        if (user == null) return NotFound();

        user.Name = updatedUser.Name;
        user.Surname = updatedUser.Surname;
        user.Login = updatedUser.Login;
        user.Password = updatedUser.Password;

        _context.SaveChanges();
        return RedirectToAction("Profile");
    }
}