namespace SimpleShop.Models;

public class Producers
{
    public int Id { set; get; }
    public string Name { set; get; }

    public List<Products> Products { get; set; } = new();
}