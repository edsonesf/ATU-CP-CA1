using SQLite;
using CoffeeShopApp.Models;

namespace CoffeeShopApp.Services;

public class DatabaseService
{
    SQLiteAsyncConnection? _db;

    async Task InitAsync()
    {
        if (_db is not null) return;
        _db = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        await _db.CreateTableAsync<Order>();
        await _db.CreateTableAsync<OrderItem>();
    }

    public async Task SaveOrderAsync(Order order, List<OrderItem> items)
    {
        await InitAsync();
        await _db!.InsertAsync(order);
        foreach (var item in items)
        {
            item.OrderId = order.Id;
            await _db.InsertAsync(item);
        }
    }

    public async Task<List<Order>> GetTodaysOrdersAsync()
    {
        await InitAsync();
        var today = DateTime.Today;
        var tomorrow = today.AddDays(1);
        return await _db!.Table<Order>()
            .Where(o => o.CreatedAt >= today && o.CreatedAt < tomorrow)
            .ToListAsync();
    }
}
