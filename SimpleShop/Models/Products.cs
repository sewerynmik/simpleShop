namespace SimpleShop.Models;

public class Products
{
    public int Id { get; set; }
    public int ProducerId { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
}