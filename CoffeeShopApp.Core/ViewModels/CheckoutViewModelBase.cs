using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CoffeeShopApp.Core.Models;
using CoffeeShopApp.Core.Services;

namespace CoffeeShopApp.Core.ViewModels;

public abstract partial class CheckoutViewModelBase : BaseViewModel
{
    private readonly BasketViewModel _basket;
    private readonly IDatabaseService _database;

    protected CheckoutViewModelBase(BasketViewModel basket, IDatabaseService database)
    {
        _basket = basket;
        _database = database;
    }

    [ObservableProperty]
    string customerName = string.Empty;

    [ObservableProperty]
    string phone = string.Empty;

    [RelayCommand]
    public async Task PlaceOrder()
    {
        if (IsBusy) return;

        if (string.IsNullOrWhiteSpace(CustomerName) || string.IsNullOrWhiteSpace(Phone))
        {
            await ShowAlertAsync("Missing Info", "Please enter your name and phone number.");
            return;
        }

        if (!System.Text.RegularExpressions.Regex.IsMatch(Phone.Trim(), @"^\+?\d{7,15}$"))
        {
            await ShowAlertAsync("Invalid Phone", "Please enter a valid phone number (digits only, 7–15 characters).");
            return;
        }

        IsBusy = true;
        try
        {
            var order = new Order
            {
                OrderNumber = "ATU-" + Guid.NewGuid().ToString("N")[..8].ToUpper(),
                CustomerName = CustomerName,
                Phone = Phone,
                Total = _basket.Total,
                CreatedAt = DateTime.Now
            };

            var items = _basket.Items.ToList();
            await _database.SaveOrderAsync(order, items);
            _basket.Clear();
            await OnOrderPlacedAsync(order);
        }
        finally
        {
            IsBusy = false;
        }
    }

    protected abstract Task ShowAlertAsync(string title, string message);
    protected abstract Task OnOrderPlacedAsync(Order order);
}
