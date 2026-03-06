using SQLite;
using CoffeeShopApp.Core.Models;

namespace CoffeeShopApp.Core.Services;

public class DatabaseService : IDatabaseService
{
    private readonly string _dbPath;
    SQLiteAsyncConnection? _db;

    public DatabaseService(string dbPath)
    {
        _dbPath = dbPath;
    }

    async Task InitAsync()
    {
        if (_db is not null) return;
        _db = new SQLiteAsyncConnection(_dbPath, Constants.Flags);
        await _db.CreateTableAsync<Order>();
        await _db.CreateTableAsync<OrderItem>();
    }

    public async Task SaveOrderAsync(Order order, List<OrderItem> items)
    {
        await InitAsync();
        await _db!.RunInTransactionAsync(tran =>
        {
            tran.Insert(order);
            foreach (var item in items)
            {
                item.OrderId = order.Id;
                tran.Insert(item);
            }
        });
    }

    public async Task<List<Order>> GetRecentOrdersAsync()
    {
        await InitAsync();
        var since = DateTime.Today.AddDays(-7);
        return await _db!.Table<Order>()
            .Where(o => o.CreatedAt >= since)
            .ToListAsync();
    }
}
