namespace CoffeeShopApp.Views;

public partial class MainMenuPage : ContentPage
{
    public MainMenuPage(ViewModels.MenuViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}
