namespace CoffeeShopApp.Views;

public partial class BasketPage : ContentPage
{
    public BasketPage(ViewModels.BasketViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}
