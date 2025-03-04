﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleShop.Models;

namespace SimpleShop.Controllers;

public class UserController(AppDbContext context) : Controller
{
    private readonly AppDbContext _context = context;
    // GET
    [Authorize(Roles = "A")]
    [Authorize]
    [HttpGet]
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
    [HttpPost]
    public IActionResult Edit(Users updatedUser)
    {
        var userId = User.FindFirst("UserId").Value;
        
        var user = _context.Users.Find(int.Parse(userId));

        if (user == null) return NotFound();

        user.Name = updatedUser.Name;
        user.Surname = updatedUser.Surname;
        user.Login = updatedUser.Login;
        user.Password = updatedUser.Password;

        _context.SaveChanges();
        return RedirectToAction("Profile");
    }
}