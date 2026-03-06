using CoffeeShopApp.Core.Models;
using CoffeeShopApp.Core.ViewModels;
using AppMenuItem = CoffeeShopApp.Core.Models.MenuItem;

namespace CoffeeShopApp.Tests;

public class BasketViewModelTests
{
    private BasketViewModel CreateBasket() => new();

    private AppMenuItem MakeItem(string name, decimal price) =>
        new() { Name = name, Price = price, Category = "Hot Drinks", ImageName = "test.png" };

    [Fact]
    public void AddItem_NewItem_AddsToBasket()
    {
        var basket = CreateBasket();
        basket.AddItem(MakeItem("Latte", 3.20m));
        Assert.Single(basket.Items);
    }

    [Fact]
    public void AddItem_SameItemTwice_IncrementsQuantity()
    {
        var basket = CreateBasket();
        var item = MakeItem("Latte", 3.20m);
        basket.AddItem(item);
        basket.AddItem(item);
        Assert.Single(basket.Items);
        Assert.Equal(2, basket.Items[0].Quantity);
    }

    [Fact]
    public void AddItem_UpdatesTotal()
    {
        var basket = CreateBasket();
        basket.AddItem(MakeItem("Espresso", 2.00m));
        basket.AddItem(MakeItem("Muffin", 2.80m));
        Assert.Equal(4.80m, basket.Total);
    }

    [Fact]
    public void RemoveItem_RemovesFromBasket()
    {
        var basket = CreateBasket();
        basket.AddItem(MakeItem("Latte", 3.20m));
        basket.RemoveItem(basket.Items[0]);
        Assert.Empty(basket.Items);
    }

    [Fact]
    public void RemoveItem_UpdatesTotal()
    {
        var basket = CreateBasket();
        basket.AddItem(MakeItem("Latte", 3.20m));
        basket.AddItem(MakeItem("Espresso", 2.00m));
        basket.RemoveItem(basket.Items[0]);
        Assert.Equal(2.00m, basket.Total);
    }

    [Fact]
    public void HasItems_EmptyBasket_ReturnsFalse()
    {
        var basket = CreateBasket();
        Assert.False(basket.HasItems);
    }

    [Fact]
    public void HasItems_AfterAddItem_ReturnsTrue()
    {
        var basket = CreateBasket();
        basket.AddItem(MakeItem("Latte", 3.20m));
        Assert.True(basket.HasItems);
    }

    [Fact]
    public void ItemCount_ReflectsTotalQuantity()
    {
        var basket = CreateBasket();
        var item = MakeItem("Latte", 3.20m);
        basket.AddItem(item);
        basket.AddItem(item);
        basket.AddItem(MakeItem("Espresso", 2.00m));
        Assert.Equal(3, basket.ItemCount);
    }

    [Fact]
    public void Clear_EmptiesBasketAndResetsTotal()
    {
        var basket = CreateBasket();
        basket.AddItem(MakeItem("Latte", 3.20m));
        basket.Clear();
        Assert.Empty(basket.Items);
        Assert.Equal(0m, basket.Total);
    }
}
