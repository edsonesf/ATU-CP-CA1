namespace CoffeeShopApp.Views;

public partial class OrderHistoryPage : ContentPage
{
    public OrderHistoryPage(ViewModels.OrderHistoryViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        ((ViewModels.OrderHistoryViewModel)BindingContext).LoadCommand.Execute(null);
    }
}
