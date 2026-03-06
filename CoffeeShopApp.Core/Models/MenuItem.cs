using SQLite;

namespace CoffeeShopApp.Core.Models;

[Table("MenuItems")]
public class MenuItem
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Category { get; set; } = string.Empty;
    public string ImageName { get; set; } = string.Empty;
}
