namespace CoffeeShopApp.Views;

public partial class SplashPage : ContentPage
{
    public SplashPage()
    {
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await Task.Delay(2000);
        var shell = IPlatformApplication.Current.Services.GetRequiredService<AppShell>();
        Application.Current!.Windows[0].Page = shell;
    }
}
