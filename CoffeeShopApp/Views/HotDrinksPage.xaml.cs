namespace CoffeeShopApp.Views;

public partial class HotDrinksPage : ContentPage
{
    public HotDrinksPage(ViewModels.MenuViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }

    private async void OnAddClicked(object sender, EventArgs e)
    {
        if (sender is not Button btn) return;
        btn.BackgroundColor = Color.FromArgb("#4CAF50");
        await btn.ScaleTo(1.25, 80);
        await btn.ScaleTo(1.0, 80);
        btn.BackgroundColor = Color.FromArgb("#6F3D1F");
    }
}
