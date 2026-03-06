using CoffeeShopApp.Core.Models;
using CoffeeShopApp.Core.Services;
using CoffeeShopApp.Core.ViewModels;
using AppMenuItem = CoffeeShopApp.Core.Models.MenuItem;

namespace CoffeeShopApp.Tests;

// Fake database that tracks SaveOrderAsync calls
class FakeDatabaseService : IDatabaseService
{
    public List<(Order order, List<OrderItem> items)> SavedOrders { get; } = new();
    public Task SaveOrderAsync(Order order, List<OrderItem> items)
    {
        SavedOrders.Add((order, items));
        return Task.CompletedTask;
    }
    public Task<List<Order>> GetRecentOrdersAsync() => Task.FromResult(new List<Order>());
}

// Testable subclass — captures alerts instead of calling Shell
class TestableCheckoutViewModel : CheckoutViewModelBase
{
    public List<string> Alerts { get; } = new();
    public bool OrderPlaced { get; private set; }

    public TestableCheckoutViewModel(BasketViewModel basket, IDatabaseService database)
        : base(basket, database) { }

    protected override Task ShowAlertAsync(string title, string message)
    {
        Alerts.Add(title);
        return Task.CompletedTask;
    }

    protected override Task OnOrderPlacedAsync(Order order)
    {
        OrderPlaced = true;
        return Task.CompletedTask;
    }
}

public class CheckoutViewModelTests
{
    private static BasketViewModel BasketWith(string name, decimal price)
    {
        var basket = new BasketViewModel();
        basket.AddItem(new AppMenuItem { Name = name, Price = price });
        return basket;
    }

    [Fact]
    public async Task PlaceOrder_EmptyName_DoesNotSave()
    {
        var db = new FakeDatabaseService();
        var vm = new TestableCheckoutViewModel(new BasketViewModel(), db)
        {
            CustomerName = "",
            Phone = "0851234567"
        };

        await vm.PlaceOrderCommand.ExecuteAsync(null);

        Assert.Empty(db.SavedOrders);
        Assert.Contains("Missing Info", vm.Alerts);
    }

    [Fact]
    public async Task PlaceOrder_InvalidPhone_DoesNotSave()
    {
        var db = new FakeDatabaseService();
        var vm = new TestableCheckoutViewModel(new BasketViewModel(), db)
        {
            CustomerName = "Edson",
            Phone = "abc"
        };

        await vm.PlaceOrderCommand.ExecuteAsync(null);

        Assert.Empty(db.SavedOrders);
        Assert.Contains("Invalid Phone", vm.Alerts);
    }

    [Fact]
    public async Task PlaceOrder_ValidInput_SavesOrderAndClearsBasket()
    {
        var db = new FakeDatabaseService();
        var basket = BasketWith("Latte", 3.20m);
        var vm = new TestableCheckoutViewModel(basket, db)
        {
            CustomerName = "Edson",
            Phone = "0851234567"
        };

        await vm.PlaceOrderCommand.ExecuteAsync(null);

        Assert.Single(db.SavedOrders);
        Assert.Equal(3.20m, db.SavedOrders[0].order.Total);
        Assert.False(basket.HasItems);
        Assert.True(vm.OrderPlaced);
    }

    [Fact]
    public async Task PlaceOrder_ValidInput_OrderNumberHasATUPrefix()
    {
        var db = new FakeDatabaseService();
        var vm = new TestableCheckoutViewModel(BasketWith("Espresso", 2.00m), db)
        {
            CustomerName = "Edson",
            Phone = "0851234567"
        };

        await vm.PlaceOrderCommand.ExecuteAsync(null);

        Assert.StartsWith("ATU-", db.SavedOrders[0].order.OrderNumber);
    }
}
