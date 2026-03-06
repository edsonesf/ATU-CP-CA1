using System.Globalization;

namespace CoffeeShopApp.Core.Helpers;

public static class CurrencyHelper
{
    private static readonly CultureInfo IrishCulture = new("en-IE");

    public static string FormatEuro(decimal amount) =>
        string.Format(IrishCulture, "{0:C}", amount);
}
