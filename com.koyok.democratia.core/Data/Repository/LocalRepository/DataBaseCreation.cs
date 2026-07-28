using Microsoft.Maui.Storage;
using SQLite;

namespace com.koyok.democratia.Data.Repository.LocalRepository
{
    public class DataBaseCreation<T> where T : new()
    {

        public SQLiteAsyncConnection? database;
        public SQLiteConnection? connection;
        public DataBaseCreation(DataBaseConnexion connexion)
        {            
            connexion.Init();
            database = connexion.database;
            connection = connexion.connection;
            Init();
        }   

        private void Init()
        {
            connection?.CreateTable<T>();
        }
    }

    public class DataBaseConnexion
    {
        public SQLiteAsyncConnection? database;
        public SQLiteConnection? connection;
        public void Init() 
        {
            if (database is not null)
                return;
            database = new (Constants.DatabasePath, Constants.Flags);
            if (connection is not null)
                return;
            connection = new(Constants.DatabasePath, Constants.Flags);
        }
        
    }

    public static class Constants
    {
        public const string DatabaseFilename = "com.koyok.democratia.db3";
#if DEBUG
        public static SQLiteOpenFlags Flags =
            SQLiteOpenFlags.ReadWrite |
            SQLiteOpenFlags.Create |
            SQLiteOpenFlags.SharedCache;

#elif !DEBUG
        public static SQLiteOpenFlags Flags =
            SQLiteOpenFlags.ReadWrite |
            SQLiteOpenFlags.Create |
            SQLiteOpenFlags.SharedCache | SQLiteOpenFlags.ProtectionComplete;
#endif

        public static string DatabasePath =>
            Path.Combine(FileSystem.AppDataDirectory, DatabaseFilename);
    }

}
