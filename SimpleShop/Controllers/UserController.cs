using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleShop.Models;

namespace SimpleShop.Controllers;

public class UserController(AppDbContext context) : Controller
{
    // GET
    [Authorize(Roles = "A")]
    [HttpGet]
    public IActionResult Index()
    {
        var users = context.Users.ToList();
        
        return View(users);
    }

    [Authorize]
    public IActionResult Profile()
    {
        var userId = User.FindFirst("UserId")?.Value;

        if (userId == null) return RedirectToAction("Index" ,"Home");

        var user = context.Users.FirstOrDefault(u => u.Id.ToString() == userId);

        if (user == null) return NotFound();
        
        return View(user);
    }

    [Authorize]
    [HttpGet]
    [Route("User/EditProfile")]
    public IActionResult Edit()
    {
        var userId = User.FindFirst("UserId")?.Value;

        if (userId == null) return RedirectToAction("Index", "Home");

        var user = context.Users.FirstOrDefault(u => u.Id.ToString() == userId);

        if (user == null) return NotFound();
        
        var roles = context.Users.Select(u => u.Role).Distinct().ToList();
        
        ViewBag.Roles = roles;

        return View(user);
    }

    [Authorize]
    [HttpPost]
    [Route("User/EditProfile")]
    public IActionResult Edit(Users updatedUser)
    {
        var userId = User.FindFirst("UserId")?.Value;

        if (userId == null) return NotFound();
        
        var user = context.Users.Find(int.Parse(userId));

        if (user == null) return NotFound();

        user.Name = updatedUser.Name;
        user.Surname = updatedUser.Surname;
        user.Login = updatedUser.Login;
        user.Password = updatedUser.Password;

        context.SaveChanges();
        return RedirectToAction("Profile");
    }

    [Authorize(Roles = "A")]
    [Route("User/Edit/{id}")]
    public IActionResult Edit(int id)
    {
        var user = context.Users.FirstOrDefault(u => u.Id == id);

        if (user == null) return NotFound();

        var roles = context.Users.Select(u => u.Role).Distinct().ToList();

        ViewBag.Roles = roles;

        return View(user);
    }

    [Authorize(Roles = "A")]
    [HttpPost]
    [Route("User/Edit/{id}")]
    public IActionResult Edit(int id, Users updatedUser)
    {
        var user = context.Users.FirstOrDefault(u => u.Id == id);

        if (user == null) return NotFound();
        
        user.Name = updatedUser.Name;
        user.Surname = updatedUser.Surname;
        user.Login = updatedUser.Login;
        user.Password = updatedUser.Password;
        user.Role = updatedUser.Role;

        context.SaveChanges();

        return RedirectToAction("Index");
    }

    [Authorize]
    [HttpPost]
    public IActionResult Delete(int userId)
    {
        var user = context.Users.FirstOrDefault(u => u.Id == userId);

        if (user == null) return NotFound();

        context.Remove(user);
        context.SaveChanges();
        
        return RedirectToAction("Index");
    }

    [Authorize(Roles = "A")]
    [HttpGet]
    public IActionResult Add()
    {
        var roles = context.Users.Select(u => u.Role).Distinct().ToList();

        ViewBag.Roles = roles;
        
        return View();
    }

    [Authorize(Roles = "A")]
    [HttpPost]
    public IActionResult Add(Users user)
    {
        var u = user;

        context.Add(u);
        context.SaveChanges();

        return RedirectToAction("Index");
    }
}