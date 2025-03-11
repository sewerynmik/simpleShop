using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleShop.Extensions;
using SimpleShop.Models;

namespace SimpleShop.Controllers;

public class OrderController(AppDbContext context) : Controller
{
    [Authorize]
    [HttpGet]
    [Route("Orders")]
    public IActionResult Index(int id)
    {
        var orders = context.Orders
            .Where(o => o.UserId == id)
            .Include(o => o.OrdersProducts)
            .ToList();

        return View(orders);
    }

    [Authorize(Roles = "A")]
    [HttpGet]
    public IActionResult Index()
    {
        var orders = context.Orders
            .Include(o => o.OrderNumber)
            .ToList();

        return View(orders);
    }

    [Authorize]
    [HttpGet]
    public IActionResult Show(int id)
    {
        var order = context.Orders
            .Include(o => o.OrdersProducts)
            .FirstOrDefault(o => o.Id == id);

        if (order == null) return NotFound();

        return View(order);
    }

    [Authorize]
    [HttpPost]
    public IActionResult Add()
    {
        var userId = User.FindFirst("UserId")?.Value;

        if (userId == null) return NotFound();
        
        var cart = HttpContext.Session.GetObject<List<CartItem>>("Cart");

        if (!cart.Any()) return NotFound();

        var order = new Orders();

        order.OrderNumber = order.Id + 1000;
        order.UserId = int.Parse(userId);

        var ordersList = new List<OrdersProducts>();

        foreach (var item in cart)
        {
            ordersList.Add(
                new OrdersProducts
                {
                    OrderId = order.Id,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity
                });
        }

        context.Add(order);
        context.Add(ordersList);
        context.SaveChanges();
        
        return RedirectToAction("Index");
    }
}