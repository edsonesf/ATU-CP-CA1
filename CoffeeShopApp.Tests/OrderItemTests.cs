using CoffeeShopApp.Core.Models;

namespace CoffeeShopApp.Tests;

public class OrderItemTests
{
    [Fact]
    public void Subtotal_IsPrice_MultipliedByQuantity()
    {
        var item = new OrderItem { Price = 3.20m, Quantity = 3 };
        Assert.Equal(9.60m, item.Subtotal);
    }

    [Fact]
    public void Subtotal_QuantityOne_EqualsPrice()
    {
        var item = new OrderItem { Price = 2.50m, Quantity = 1 };
        Assert.Equal(2.50m, item.Subtotal);
    }
}
