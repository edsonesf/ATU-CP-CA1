namespace CoffeeShopApp;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
        Resources.MergedDictionaries.Add(new Resources.Styles.Colors());
        Resources.MergedDictionaries.Add(new Resources.Styles.Styles());
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        return new Window(new Views.SplashPage());
    }
}
