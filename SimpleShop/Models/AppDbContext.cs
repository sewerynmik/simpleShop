using Microsoft.EntityFrameworkCore;

namespace SimpleShop.Models;

public class AppDbContext : DbContext
{
    public DbSet<Products> Products { get; set; }
    public DbSet<Users> Users { get; set; }
    public DbSet<Producers> Producers { get; set; }
    public DbSet<OrdersProducts> OrdersProducts { get; set; }
    public DbSet<Orders> Orders { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlite("Data Source=DataBase.db");
}