using CoffeeShopApp.Views;

namespace CoffeeShopApp;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute(nameof(HotDrinksPage), typeof(HotDrinksPage));
        Routing.RegisterRoute(nameof(ColdDrinksPage), typeof(ColdDrinksPage));
        Routing.RegisterRoute(nameof(FoodPage), typeof(FoodPage));
        Routing.RegisterRoute(nameof(BasketPage), typeof(BasketPage));
        Routing.RegisterRoute(nameof(CheckoutPage), typeof(CheckoutPage));
        Routing.RegisterRoute(nameof(OrderHistoryPage), typeof(OrderHistoryPage));
        Routing.RegisterRoute(nameof(OrderDetailPage), typeof(OrderDetailPage));
    }
}
