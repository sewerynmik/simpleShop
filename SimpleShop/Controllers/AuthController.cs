using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using SimpleShop.Models;

namespace SimpleShop.Controllers;

public class AuthController(AppDbContext context) : Controller
{
    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Register(string name, string surname, string login, string password)
    {
        if (context.Users.Any(u=> u.Login == login))
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

        context.Users.Add(user);
        context.SaveChanges();
        
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
        var user = context.Users.SingleOrDefault(u => u.Login == login && u.Password == password);

        if (user == null)
        {
            ModelState.AddModelError("", "Nieprawidłowy login lub hasło.");
            return View();
        }

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Name),
            new Claim("UserId", user.Id.ToString()),
            new Claim(ClaimTypes.Role, user.Role.ToString())
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