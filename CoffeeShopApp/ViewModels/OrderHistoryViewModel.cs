using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using CoffeeShopApp.Models;
using CoffeeShopApp.Services;

namespace CoffeeShopApp.ViewModels;

public partial class OrderHistoryViewModel : BaseViewModel
{
    private readonly DatabaseService _database;

    public ObservableCollection<Order> Orders { get; } = new();

    public OrderHistoryViewModel(DatabaseService database)
    {
        _database = database;
    }

    [RelayCommand]
    async Task Load()
    {
        IsBusy = true;
        Orders.Clear();
        var orders = await _database.GetTodaysOrdersAsync();
        foreach (var order in orders)
            Orders.Add(order);
        IsBusy = false;
    }
}
