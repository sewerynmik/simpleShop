using Microsoft.AspNetCore.Mvc;
using SimpleShop.Extensions;
using SimpleShop.Models;

namespace SimpleShop.Controllers;

public class CartController(AppDbContext context) : Controller
{
    public IActionResult Index()
    {
        var cart = HttpContext.Session.GetObject<List<CartItem>>("Cart") ?? new List<CartItem>();
        return View(cart);
    }

    public IActionResult AddToCart(int productId)
    {
        var product = context.Products.FirstOrDefault(p => p.Id == productId);

        if (product == null) return NotFound();

        var cart = HttpContext.Session.GetObject<List<CartItem>>("Cart") ?? new List<CartItem>();

        var cartItem = cart.FirstOrDefault(c => c.ProductId == productId);

        if (cartItem != null) cartItem.Quantity++;
        else
        {
            cart.Add(new CartItem
            {
                ProductId = product.Id,
                ProductName = product.Name,
                Quantity = 1,
                Price = product.Price
            });
        }
        
        HttpContext.Session.SetObject("Cart", cart);

        return RedirectToAction("Index");
    }

    public IActionResult DeleteFromCart(int productId)
    {
        var cart = HttpContext.Session.GetObject<List<CartItem>>("Cart");

        var cartItem = cart.FirstOrDefault(c => c.ProductId == productId);

        if (cartItem == null) return NotFound();
        
        cart.Remove(cartItem);
        
        HttpContext.Session.SetObject("Cart", cart);

        return RedirectToAction("Index");
    }

    public IActionResult ClearCart()
    {
        HttpContext.Session.Remove("Cart");
        return RedirectToAction("Index");
    }
    
}