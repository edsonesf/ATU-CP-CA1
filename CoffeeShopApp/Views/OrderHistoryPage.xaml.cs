namespace CoffeeShopApp.Views;

public partial class OrderHistoryPage : ContentPage
{
    public OrderHistoryPage(ViewModels.OrderHistoryViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}
