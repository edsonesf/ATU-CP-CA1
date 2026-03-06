namespace CoffeeShopApp.Views;

public partial class OrderDetailPage : ContentPage
{
    public OrderDetailPage(ViewModels.OrderDetailViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}
