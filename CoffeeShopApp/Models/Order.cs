using SQLite;

namespace CoffeeShopApp.Models;

[Table("Orders")]
public class Order
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    public string OrderNumber { get; set; } = string.Empty;
    public string CustomerName { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public decimal Total { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}
