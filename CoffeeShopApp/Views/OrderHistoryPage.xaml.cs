namespace CoffeeShopApp.Views;

public partial class OrderHistoryPage : ContentPage
{
    private readonly ViewModels.OrderHistoryViewModel _vm;

    public OrderHistoryPage(ViewModels.OrderHistoryViewModel vm)
    {
        InitializeComponent();
        BindingContext = _vm = vm;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _vm.LoadCommand.Execute(null);
    }
}
