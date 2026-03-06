using CommunityToolkit.Mvvm.ComponentModel;

namespace CoffeeShopApp.Core.ViewModels;

public partial class BaseViewModel : ObservableObject
{
    [ObservableProperty]
    bool isBusy;

    [ObservableProperty]
    string title = string.Empty;
}
