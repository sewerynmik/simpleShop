using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleShop.Models;

namespace SimpleShop.Controllers;

public class ProductController(AppDbContext context) : Controller
{
    // GET
    [AllowAnonymous]
    public IActionResult Index()
    {
        var products = context.Products
            .Include(p => p.Producer)
            .ToList();
        
        return View(products);
    }

    [Authorize(Roles = "A")]
    [HttpGet]
    public IActionResult Edit(int id)
    {
        var product = context.Products.Include(p => p.Producer).FirstOrDefault(p => p.Id == id);

        if (product == null) return NotFound();

        ViewBag.Producers = context.Producers
            .Select(p => new {p.Name, p.Id})
            .ToList();
        
        return View(product);
    }

    [Authorize(Roles = "A")]
    [HttpPost]
    public IActionResult Edit(int id, Products updatedProducts, int producerId)
    {
        var product = context.Products
            .Include(p => p.Producer)
            .FirstOrDefault(p => p.Id == id);

        if (product == null) return NotFound();

        product.Name = updatedProducts.Name;
        product.Price = updatedProducts.Price;

        var producer = context.Producers.FirstOrDefault(p => p.Id == producerId);
        if (producer == null) return NotFound();
        
        product.ProducerId = producerId;

        context.SaveChanges();

        return RedirectToAction("Index");
    }

    [Authorize(Roles = "A")]
    [HttpGet]
    public IActionResult Add()
    {
        ViewBag.Producers = context.Producers.Select(p => new { p.Id, p.Name });
        
        return View();
    }

    [Authorize(Roles = "A")]
    [HttpPost]
    public IActionResult Add(Products products)
    {
        var p = products;
        
        context.Add(p);
        context.SaveChanges();

        return RedirectToAction("Index");
    }

    [Authorize(Roles = "A")]
    [HttpPost]
    public IActionResult Delete(int id)
    {
        var product = context.Products
            .Include(p => p.Producer)
            .FirstOrDefault(p => p.Id == id);

        if (product == null) return NotFound();

        context.Remove(product);
        context.SaveChanges();

        return RedirectToAction("Index");
    }
}