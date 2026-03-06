namespace CoffeeShopApp.Views;

public partial class ColdDrinksPage : ContentPage
{
    public ColdDrinksPage(ViewModels.MenuViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}
