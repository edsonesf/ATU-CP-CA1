namespace CoffeeShopApp.Views;

public partial class CheckoutPage : ContentPage
{
    public CheckoutPage(ViewModels.CheckoutViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}
