using CoffeeShopApp.Models;

namespace CoffeeShopApp.Services;

public class DatabaseService
{
    public Task SaveOrderAsync(Order order, List<OrderItem> items) => Task.CompletedTask;

    public Task<List<Order>> GetTodaysOrdersAsync() => Task.FromResult(new List<Order>());
}
