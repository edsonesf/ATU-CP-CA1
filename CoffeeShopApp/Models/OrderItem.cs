using SQLite;

namespace CoffeeShopApp.Models;

[Table("OrderItems")]
public class OrderItem
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    public int OrderId { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Quantity { get; set; }

    [Ignore]
    public decimal Subtotal => Price * Quantity;
}
