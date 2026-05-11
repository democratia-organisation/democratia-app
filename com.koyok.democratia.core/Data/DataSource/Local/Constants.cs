using Microsoft.Maui.Storage;

namespace com.koyok.democratia.Data.DataSource.Local
{
    public static class Constants
    {
        public const string DatabaseFilename = "com.koyok.democratia.db3";

        public const SQLite.SQLiteOpenFlags Flags =
            SQLite.SQLiteOpenFlags.ReadWrite |
            SQLite.SQLiteOpenFlags.Create |
            SQLite.SQLiteOpenFlags.SharedCache;

        public static string DatabasePath =>
            Path.Combine(FileSystem.AppDataDirectory, DatabaseFilename);
    }
}
