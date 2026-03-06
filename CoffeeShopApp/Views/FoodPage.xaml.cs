namespace CoffeeShopApp.Views;

public partial class FoodPage : ContentPage
{
    public FoodPage(ViewModels.MenuViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}
