using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CoffeeShopApp.Core.Models;
using CoffeeShopApp.Core.Services;

namespace CoffeeShopApp.ViewModels;

public partial class CheckoutViewModel : BaseViewModel
{
    private readonly BasketViewModel _basket;
    private readonly IDatabaseService _database;

    public CheckoutViewModel(BasketViewModel basket, IDatabaseService database)
    {
        _basket = basket;
        _database = database;
    }

    [ObservableProperty]
    string customerName = string.Empty;

    [ObservableProperty]
    string phone = string.Empty;

    [RelayCommand]
    async Task PlaceOrder()
    {
        if (IsBusy) return;

        if (string.IsNullOrWhiteSpace(CustomerName) || string.IsNullOrWhiteSpace(Phone))
        {
            await Shell.Current.DisplayAlert("Missing Info", "Please enter your name and phone number.", "OK");
            return;
        }

        if (!System.Text.RegularExpressions.Regex.IsMatch(Phone.Trim(), @"^\+?\d{7,15}$"))
        {
            await Shell.Current.DisplayAlert("Invalid Phone", "Please enter a valid phone number (digits only, 7–15 characters).", "OK");
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

            await Shell.Current.DisplayAlert(
                "Order Placed! ☕",
                $"Thank you, {order.CustomerName}!\nYour order number is: {order.OrderNumber}",
                "OK");

            await Shell.Current.GoToAsync("//MainMenuPage");
        }
        finally
        {
            IsBusy = false;
        }
    }
}
