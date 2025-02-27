using System.ComponentModel.DataAnnotations;

namespace SimpleShop.Models;

public class Users
{
    public int Id { set; get; }
    [Required]
    public string Name { set; get; }
    [Required]
    public string Surname { set; get; }
    [Required]
    public char Role { set; get; }
    [Required]
    public string Login { set; get; }
    [Required]
    public string Password { set; get; }

    public List<Orders> Orders { get; set; } = new();
}