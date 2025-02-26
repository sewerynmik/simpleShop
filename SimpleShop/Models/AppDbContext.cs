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
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Orders>()
            .HasOne(o => o.User)
            .WithMany(u => u.Orders)
            .HasForeignKey(o => o.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Products>()
            .HasOne(p => p.Producer)
            .WithMany(pr => pr.Products)
            .HasForeignKey(p => p.ProducerId)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<OrdersProducts>()
            .HasKey(op => new { op.OrderId, op.ProductId });
        
        modelBuilder.Entity<OrdersProducts>()
            .HasOne(op => op.Order)
            .WithMany(o => o.OrdersProducts)
            .HasForeignKey(op => op.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<OrdersProducts>()
            .HasOne(o => o.Products)
            .WithMany(p => p.OrdersProducts)
            .HasForeignKey(o => o.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

    }
}