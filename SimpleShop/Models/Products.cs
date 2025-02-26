namespace SimpleShop.Models;

public class Products
{
    public int Id { get; set; }
    
    public int ProducerId { get; set; }
    public Producers Producer { get; set; }
    
    public string Name { get; set; }
    public decimal Price { get; set; }
    
    public List<OrdersProducts> OrdersProducts { get; set; } = new();
}