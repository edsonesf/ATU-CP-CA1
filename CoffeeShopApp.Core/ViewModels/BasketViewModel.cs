using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CoffeeShopApp.Core.Models;
using CoffeeShopApp.Core.Helpers;
using AppMenuItem = CoffeeShopApp.Core.Models.MenuItem;

namespace CoffeeShopApp.Core.ViewModels;

public partial class BasketViewModel : BaseViewModel
{
    public ObservableCollection<OrderItem> Items { get; } = new();

    [ObservableProperty]
    decimal total;

    public bool HasItems => Items.Count > 0;
    public int ItemCount => Items.Sum(i => i.Quantity);
    public string FormattedTotal => CurrencyHelper.FormatEuro(Total);

    [RelayCommand]
    public void AddItem(AppMenuItem menuItem)
    {
        var existing = Items.FirstOrDefault(i => i.Name == menuItem.Name);
        if (existing is not null)
            existing.Quantity++;
        else
            Items.Add(new OrderItem { Name = menuItem.Name, Price = menuItem.Price, Quantity = 1 });

        RecalculateTotal();
    }

    [RelayCommand]
    public void RemoveItem(OrderItem item)
    {
        Items.Remove(item);
        RecalculateTotal();
    }

    public void Clear()
    {
        Items.Clear();
        Total = 0;
    }

    private void RecalculateTotal()
    {
        Total = Items.Sum(i => i.Subtotal);
        OnPropertyChanged(nameof(HasItems));
        OnPropertyChanged(nameof(ItemCount));
        OnPropertyChanged(nameof(FormattedTotal));
    }
}
