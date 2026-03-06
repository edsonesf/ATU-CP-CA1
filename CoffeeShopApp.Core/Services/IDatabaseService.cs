using CoffeeShopApp.Core.Models;

namespace CoffeeShopApp.Core.Services;

public interface IDatabaseService
{
    Task SaveOrderAsync(Order order, List<OrderItem> items);
    Task<List<Order>> GetRecentOrdersAsync();
    Task<List<OrderItem>> GetOrderItemsAsync(int orderId);
}
