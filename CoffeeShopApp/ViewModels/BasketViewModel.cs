using CommunityToolkit.Mvvm.Input;
using AppMenuItem = CoffeeShopApp.Models.MenuItem;

namespace CoffeeShopApp.ViewModels;

public partial class BasketViewModel : BaseViewModel
{
    [RelayCommand]
    public void AddItem(AppMenuItem item) { }
}
