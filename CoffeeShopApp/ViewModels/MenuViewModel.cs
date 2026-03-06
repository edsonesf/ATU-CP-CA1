using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using AppMenuItem = CoffeeShopApp.Models.MenuItem;

namespace CoffeeShopApp.ViewModels;

public partial class MenuViewModel : BaseViewModel
{
    private readonly BasketViewModel _basket;

    public MenuViewModel(BasketViewModel basket)
    {
        _basket = basket;
    }

    public ObservableCollection<AppMenuItem> HotDrinks { get; } = new(new[]
    {
        new AppMenuItem { Name = "Espresso",      Price = 2.00m, Category = "Hot Drinks", ImageName = "espresso.png" },
        new AppMenuItem { Name = "Americano",     Price = 2.50m, Category = "Hot Drinks", ImageName = "americano.png" },
        new AppMenuItem { Name = "Cappuccino",    Price = 3.00m, Category = "Hot Drinks", ImageName = "cappuccino.png" },
        new AppMenuItem { Name = "Latte",         Price = 3.20m, Category = "Hot Drinks", ImageName = "latte.png" },
        new AppMenuItem { Name = "Flat White",    Price = 3.20m, Category = "Hot Drinks", ImageName = "flat_white.png" },
        new AppMenuItem { Name = "Hot Chocolate", Price = 3.50m, Category = "Hot Drinks", ImageName = "hot_chocolate.png" },
    });

    public ObservableCollection<AppMenuItem> ColdDrinks { get; } = new(new[]
    {
        new AppMenuItem { Name = "Still Water",     Price = 1.00m, Category = "Cold Drinks", ImageName = "still_water.png" },
        new AppMenuItem { Name = "Sparkling Water", Price = 1.20m, Category = "Cold Drinks", ImageName = "sparkling_water.png" },
        new AppMenuItem { Name = "Canned Coke",     Price = 1.80m, Category = "Cold Drinks", ImageName = "canned_coke.png" },
        new AppMenuItem { Name = "Canned 7UP",      Price = 1.80m, Category = "Cold Drinks", ImageName = "canned_7up.png" },
    });

    public ObservableCollection<AppMenuItem> Food { get; } = new(new[]
    {
        new AppMenuItem { Name = "Croissant",         Price = 2.50m, Category = "Food", ImageName = "croissant.png" },
        new AppMenuItem { Name = "Muffin",            Price = 2.80m, Category = "Food", ImageName = "muffin.png" },
        new AppMenuItem { Name = "Toasted Sandwich",  Price = 4.50m, Category = "Food", ImageName = "toasted_sandwich.png" },
        new AppMenuItem { Name = "Scone with Butter", Price = 3.00m, Category = "Food", ImageName = "scone.png" },
        new AppMenuItem { Name = "Banana Bread",      Price = 2.80m, Category = "Food", ImageName = "banana_bread.png" },
    });

    [RelayCommand]
    void AddItem(AppMenuItem item) => _basket.AddItem(item);

    [RelayCommand]
    async Task GoToHotDrinks() => await Shell.Current.GoToAsync(nameof(Views.HotDrinksPage));

    [RelayCommand]
    async Task GoToColdDrinks() => await Shell.Current.GoToAsync(nameof(Views.ColdDrinksPage));

    [RelayCommand]
    async Task GoToFood() => await Shell.Current.GoToAsync(nameof(Views.FoodPage));

    [RelayCommand]
    async Task GoToBasket() => await Shell.Current.GoToAsync(nameof(Views.BasketPage));
}
