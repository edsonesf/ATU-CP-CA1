namespace CoffeeShopApp.Views;

public partial class HotDrinksPage : ContentPage
{
    public HotDrinksPage(ViewModels.MenuViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}
