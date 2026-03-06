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
