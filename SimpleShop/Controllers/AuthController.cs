using Microsoft.AspNetCore.Mvc;
using SimpleShop.Models;

namespace SimpleShop.Controllers;

public class AuthController(AppDbContext context) : Controller
{
    private readonly AppDbContext _context = context;

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Register(string name, string surname, string login, string password)
    {
        if (_context.Users.Any(u=> u.Login == login))
        {
            ModelState.AddModelError("","Użytkownik o tym loginie już istnieje");
            return View();
        }

        var user = new Users
        {
            Name = name,
            Surname = surname,
            Role = 'U',
            Login = login,
            Password = password
        };

        _context.Users.Add(user);
        _context.SaveChanges();
        
        return RedirectToAction("Login");
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Login(string login, string password)
    {
        var user = _context.Users.SingleOrDefault(u => u.Login == login);

        if (user == null || user.Password != password)
        {
            ModelState.AddModelError("", "Nieprawidłowy login lub hasło.");
            return View();
        }
        
        HttpContext.Session.SetString("UserId", user.Id.ToString());
        HttpContext.Session.SetString("UserName", user.Name);

        return RedirectToAction("Index", "Home");
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Login");
    }
}