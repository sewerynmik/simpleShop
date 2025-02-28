using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
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
    public async Task<IActionResult> Login(string login, string password)
    {
        var user = _context.Users.SingleOrDefault(u => u.Login == login && u.Password == password);

        if (user == null)
        {
            ModelState.AddModelError("", "Nieprawidłowy login lub hasło.");
            return View();
        }

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Name),
            new Claim("UserId", user.Id.ToString())
        };

        var claimsIdentity = new ClaimsIdentity(claims, "Cookies");
        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

        await HttpContext.SignInAsync("Cookies", claimsPrincipal);

        return RedirectToAction("Index", "Home");
    }

    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync("Cookies");
        return RedirectToAction("Login");
    }
}