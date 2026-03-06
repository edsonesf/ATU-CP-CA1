using CoffeeShopApp.Core.Services;
using CoffeeShopApp.ViewModels;
using CoffeeShopApp.Views;
using Microsoft.Extensions.Logging;

namespace CoffeeShopApp;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        // Services — Singleton (one instance for the app lifetime)
        var dbPath = Path.Combine(FileSystem.AppDataDirectory, Core.Constants.DatabaseFilename);
        builder.Services.AddSingleton<IDatabaseService>(_ => new DatabaseService(dbPath));
        builder.Services.AddSingleton<AppShell>();

        // ViewModels — BasketViewModel is Singleton so basket state is shared
        builder.Services.AddSingleton<BasketViewModel>();
        builder.Services.AddTransient<MenuViewModel>();
        builder.Services.AddTransient<CheckoutViewModel>();
        builder.Services.AddTransient<OrderHistoryViewModel>();
        builder.Services.AddTransient<OrderDetailViewModel>();

        // Pages
        builder.Services.AddTransient<MainMenuPage>();
        builder.Services.AddTransient<HotDrinksPage>();
        builder.Services.AddTransient<ColdDrinksPage>();
        builder.Services.AddTransient<FoodPage>();
        builder.Services.AddTransient<BasketPage>();
        builder.Services.AddTransient<CheckoutPage>();
        builder.Services.AddTransient<OrderHistoryPage>();
        builder.Services.AddTransient<OrderDetailPage>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
