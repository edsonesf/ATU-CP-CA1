using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using CoffeeShopApp.Core.Models;
using CoffeeShopApp.Core.Services;

namespace CoffeeShopApp.ViewModels;

public partial class OrderHistoryViewModel : BaseViewModel
{
    private readonly IDatabaseService _database;

    public ObservableCollection<Order> Orders { get; } = new();

    public OrderHistoryViewModel(IDatabaseService database)
    {
        _database = database;
    }

    [RelayCommand]
    async Task SelectOrder(Order order) =>
        await Shell.Current.GoToAsync(nameof(Views.OrderDetailPage), new Dictionary<string, object>
        {
            ["orderId"]      = order.Id,
            ["orderNumber"]  = order.OrderNumber,
            ["customerName"] = order.CustomerName,
            ["total"]        = order.Total,
            ["createdAt"]    = order.CreatedAt
        });

    [RelayCommand]
    async Task Load()
    {
        IsBusy = true;
        Orders.Clear();
        var orders = await _database.GetRecentOrdersAsync();
        foreach (var order in orders)
            Orders.Add(order);
        IsBusy = false;
    }
}
