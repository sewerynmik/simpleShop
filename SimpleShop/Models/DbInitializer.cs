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
            new Users { Name = "Piotr", Surname = "Nowak", Role = 'A', Login = "admin", Password = "admin" }
        );


        context.Producers.AddRange(
            new Producers { Name = "Asus" },
            new Producers { Name = "Iiyama" },
            new Producers { Name = "Nvidia" }
        );


        context.Products.AddRange(
            new Products { Name = "Myszka", Price = 12, ProducerId = 1 },
            new Products { Name = "Monitor", Price = 100, ProducerId = 2 }
        );
        
        context.SaveChanges();
    }
}