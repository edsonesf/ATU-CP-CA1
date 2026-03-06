using CoffeeShopApp.Core.Models;
using CoffeeShopApp.Core.Services;
using CoffeeShopApp.Core.ViewModels;

namespace CoffeeShopApp.ViewModels;

public partial class CheckoutViewModel : CheckoutViewModelBase
{
    public CheckoutViewModel(BasketViewModel basket, IDatabaseService database)
        : base(basket, database)
    {
        CustomerName = Preferences.Get("customer_name", string.Empty);
        Phone = Preferences.Get("customer_phone", string.Empty);
    }

    protected override async Task ShowAlertAsync(string title, string message) =>
        await Shell.Current.DisplayAlert(title, message, "OK");

    protected override async Task OnOrderPlacedAsync(Order order)
    {
        Preferences.Set("customer_name", order.CustomerName);
        Preferences.Set("customer_phone", order.Phone);
        await Shell.Current.DisplayAlert(
            "Order Placed! ☕",
            $"Thank you, {order.CustomerName}!\nYour order number is: {order.OrderNumber}",
            "OK");
        await Shell.Current.GoToAsync("//MainMenuPage");
    }
}
