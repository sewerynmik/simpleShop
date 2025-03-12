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
    [Route("/Orders")]
    public IActionResult Index()
    {
        var userId = User.FindFirst("UserId")?.Value;

        if (userId == null) return NotFound();

        List<Orders> orders;
        
        if (User.IsInRole("A"))
        {
            orders = context.Orders
                .Include(o => o.OrdersProducts)
                .Include(o => o.User)
                .ToList();
        }
        else
        {
            orders = context.Orders
                .Where(o => o.UserId == int.Parse(userId))
                .Include(o => o.OrdersProducts)
                .ToList();
        }

        return View(orders);
    }


    [Authorize]
    [HttpGet]
    public IActionResult Show(int id)
    {
        var order = context.Orders
            .Include(o => o.OrdersProducts)
            .ThenInclude(op => op.Products)
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

        var order = new Orders
        {
            UserId = int.Parse(userId)
        };

        context.Add(order);
        context.SaveChanges();

        order.OrderNumber = order.Id + 1000;

        var ordersProducts = new List<OrdersProducts>();

        foreach (var item in cart)
        {
            ordersProducts.Add(
                new OrdersProducts
                {
                    OrderId = order.Id,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity
                });
        }

        context.AddRange(ordersProducts);
        context.SaveChanges();

        HttpContext.Session.Remove("Cart");

        return RedirectToAction("Index");
    }
}