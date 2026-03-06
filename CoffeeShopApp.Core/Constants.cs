namespace CoffeeShopApp.Core;

public static class Constants
{
    public const string DatabaseFilename = "coffeeshop.db3";

    public const SQLite.SQLiteOpenFlags Flags =
        SQLite.SQLiteOpenFlags.ReadWrite |
        SQLite.SQLiteOpenFlags.Create |
        SQLite.SQLiteOpenFlags.SharedCache;
}
