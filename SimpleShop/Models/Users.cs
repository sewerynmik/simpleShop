namespace SimpleShop.Models;

public class Users
{
    public int Id { set; get; }
    public string Name { set; get; }
    public string Surname { set; get; }
    public char Role { set; get; }
    public string Login { set; get; }
    public string Password { set; get; }

    public List<Orders> Orders { get; set; } = new();
}