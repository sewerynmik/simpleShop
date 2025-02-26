namespace SimpleShop.Models;

public class Orders
{
    public int Id { get; set; }
    public int OrderNumber { get; set; }
    
    public int UserId { get; set; }
    public Users User { get; set; }

    public List<OrdersProducts> OrdersProducts { get; set; } = new();
}