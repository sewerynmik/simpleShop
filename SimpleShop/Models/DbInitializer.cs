using Microsoft.EntityFrameworkCore;

namespace SimpleShop.Models;

public class DbInitializer
{
    public static void Seed(AppDbContext context)
    {
        if (context.Users.Any()) return; 
        
        context.Database.EnsureDeleted();
        context.Database.Migrate();

        context.Users.AddRange(
            new Users { Name = "Jan", Surname = "Kowalski", Role = 'U', Login = "JKowalski", Password = "1234" },
            new Users { Name = "Piotr", Surname = "Nowak", Role = 'A', Login = "admin", Password = "admin" },
            new Users { Name = "Anna", Surname = "Nowicka", Role = 'U', Login = "ANowicka", Password = "5678" },
            new Users { Name = "Katarzyna", Surname = "Wiśniewska", Role = 'U', Login = "KWiśniewska", Password = "abcd" },
            new Users { Name = "Michał", Surname = "Lewandowski", Role = 'A', Login = "mlewandowski", Password = "admin123" }
        );

        context.Producers.AddRange(
            new Producers { Name = "Asus" },
            new Producers { Name = "Iiyama" },
            new Producers { Name = "Nvidia" },
            new Producers { Name = "AMD" },
            new Producers { Name = "Intel" },
            new Producers { Name = "Logitech" },
            new Producers { Name = "Corsair" },
            new Producers { Name = "Dell" }
        );
        
        context.SaveChanges(); 

        var asus = context.Producers.First(p => p.Name == "Asus").Id;
        var iiyama = context.Producers.First(p => p.Name == "Iiyama").Id;
        var nvidia = context.Producers.First(p => p.Name == "Nvidia").Id;
        var amd = context.Producers.First(p => p.Name == "AMD").Id;
        var intel = context.Producers.First(p => p.Name == "Intel").Id;
        var logitech = context.Producers.First(p => p.Name == "Logitech").Id;
        var corsair = context.Producers.First(p => p.Name == "Corsair").Id;
        var dell = context.Producers.First(p => p.Name == "Dell").Id;

        context.Products.AddRange(
            new Products { Name = "Myszka", Price = 50, ProducerId = logitech },
            new Products { Name = "Klawiatura mechaniczna", Price = 300, ProducerId = corsair },
            new Products { Name = "Monitor 24\" LED", Price = 800, ProducerId = iiyama },
            new Products { Name = "Karta graficzna RTX 4070", Price = 3500, ProducerId = nvidia },
            new Products { Name = "Procesor Intel i7", Price = 1500, ProducerId = intel },
            new Products { Name = "Procesor AMD Ryzen 9", Price = 1800, ProducerId = amd },
            new Products { Name = "Laptop gamingowy", Price = 5000, ProducerId = asus },
            new Products { Name = "Stacja robocza Dell", Price = 7000, ProducerId = dell }
        );

        context.SaveChanges();

        var user1 = context.Users.First(u => u.Login == "JKowalski").Id;
        var user2 = context.Users.First(u => u.Login == "ANowicka").Id;
        var user3 = context.Users.First(u => u.Login == "KWiśniewska").Id;
        
        var orders = new List<Orders>
        {
            new Orders { UserId = user1, OrderNumber = 1001 },
            new Orders { UserId = user2, OrderNumber = 1002 },
            new Orders { UserId = user3, OrderNumber = 1003 }
        };
        
        context.Orders.AddRange(orders);
        context.SaveChanges(); 

        var order1 = orders[0].Id;
        var order2 = orders[1].Id;
        var order3 = orders[2].Id;

        var mouse = context.Products.First(p => p.Name == "Myszka").Id;
        var keyboard = context.Products.First(p => p.Name == "Klawiatura mechaniczna").Id;
        var monitor = context.Products.First(p => p.Name == "Monitor 24\" LED").Id;
        var gpu = context.Products.First(p => p.Name == "Karta graficzna RTX 4070").Id;
        var cpuIntel = context.Products.First(p => p.Name == "Procesor Intel i7").Id;
        var cpuAmd = context.Products.First(p => p.Name == "Procesor AMD Ryzen 9").Id;
        var laptop = context.Products.First(p => p.Name == "Laptop gamingowy").Id;
        var workstation = context.Products.First(p => p.Name == "Stacja robocza Dell").Id;

        context.OrdersProducts.AddRange(
            new OrdersProducts { OrderId = order1, ProductId = mouse, Quantity = 2 },
            new OrdersProducts { OrderId = order1, ProductId = keyboard, Quantity = 1 },
            new OrdersProducts { OrderId = order1, ProductId = monitor, Quantity = 1 },

            new OrdersProducts { OrderId = order2, ProductId = gpu, Quantity = 1 },
            new OrdersProducts { OrderId = order2, ProductId = cpuIntel, Quantity = 1 },

            new OrdersProducts { OrderId = order3, ProductId = cpuAmd, Quantity = 1 },
            new OrdersProducts { OrderId = order3, ProductId = laptop, Quantity = 1 },
            new OrdersProducts { OrderId = order3, ProductId = workstation, Quantity = 1 }
        );

        context.SaveChanges();
    }
}
