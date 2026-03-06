using CommunityToolkit.Mvvm.ComponentModel;
using SQLite;

namespace CoffeeShopApp.Core.Models;

[Table("OrderItems")]
public partial class OrderItem : ObservableObject
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    public int OrderId { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }

    [ObservableProperty]
    int quantity;

    [Ignore]
    public decimal Subtotal => Price * Quantity;
}
