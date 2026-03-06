using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CoffeeShopApp.Core.Models;
using CoffeeShopApp.Core.Services;

namespace CoffeeShopApp.ViewModels;

public partial class OrderDetailViewModel : BaseViewModel, IQueryAttributable
{
    private readonly IDatabaseService _database;

    public ObservableCollection<OrderItem> Items { get; } = new();

    [ObservableProperty] string orderNumber = string.Empty;
    [ObservableProperty] string customerName = string.Empty;
    [ObservableProperty] decimal total;
    [ObservableProperty] DateTime createdAt;

    public OrderDetailViewModel(IDatabaseService database) => _database = database;

    public async void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        OrderNumber = query["orderNumber"].ToString()!;
        CustomerName = query["customerName"].ToString()!;
        Total = (decimal)query["total"];
        CreatedAt = (DateTime)query["createdAt"];

        var items = await _database.GetOrderItemsAsync((int)query["orderId"]);
        Items.Clear();
        foreach (var item in items) Items.Add(item);
    }
}
