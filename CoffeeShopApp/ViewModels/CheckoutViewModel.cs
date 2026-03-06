using CoffeeShopApp.Core.Models;
using CoffeeShopApp.Core.Services;
using CoffeeShopApp.Core.ViewModels;

namespace CoffeeShopApp.ViewModels;

public partial class CheckoutViewModel : CheckoutViewModelBase
{
    public CheckoutViewModel(BasketViewModel basket, IDatabaseService database)
        : base(basket, database) { }

    protected override async Task ShowAlertAsync(string title, string message) =>
        await Shell.Current.DisplayAlert(title, message, "OK");

    protected override async Task OnOrderPlacedAsync(Order order)
    {
        await Shell.Current.DisplayAlert(
            "Order Placed! ☕",
            $"Thank you, {order.CustomerName}!\nYour order number is: {order.OrderNumber}",
            "OK");
        await Shell.Current.GoToAsync("//MainMenuPage");
    }
}
