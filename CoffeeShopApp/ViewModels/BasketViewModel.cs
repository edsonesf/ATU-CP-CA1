using CommunityToolkit.Mvvm.Input;

namespace CoffeeShopApp.ViewModels;

public partial class BasketViewModel : Core.ViewModels.BasketViewModel
{
    [RelayCommand]
    async Task GoToCheckout() => await Shell.Current.GoToAsync(nameof(Views.CheckoutPage));
}
