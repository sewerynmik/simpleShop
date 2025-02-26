namespace SimpleShop.Models;

public class OrdersProducts
{
    public int Id { get; set; }
    
    public int OrderId { get; set; }
    public Orders Order { get; set; }
    
    public int ProductId { get; set; }
    public Products Products { get; set; }
    
    public int Quantity { get; set; }
}