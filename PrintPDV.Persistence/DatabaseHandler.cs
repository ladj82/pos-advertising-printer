using System;
using DapperExtensions;
using DapperExtensions.Mapper;
using DapperExtensions.Sql;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Reflection;
using PrintPDV.Utility;

namespace PrintPDV.Persistence
{
    public class DatabaseHandler
    {
        private static readonly object Locker = new object();

        private static IDatabase _instance;

        public static IDatabase Instance()
        {
            return SQLite();
        }

        public static IDatabase Instance(DatabaseType dbType)
        {
            switch (dbType)
            {
                case DatabaseType.SQLite:
                    return SQLite();
            }

            return null;
        }

        #region SQL Handlers

        private static IDatabase SQLite()
        {
            try
            {
                var dbFile = AppConfigUtility.AppDirectory + AppConfigUtility.DatabaseName;

                if (!File.Exists(dbFile))
                    throw new DatabaseNotFoundException();

                lock (Locker)
                {
                    if (_instance != null) return _instance;

                    var connectionString = AppConfigUtility.DatabaseConnString;
                    var connection = new SQLiteConnection(connectionString);
                    var config = new DapperExtensionsConfiguration(typeof(AutoClassMapper<>), new List<Assembly>(), new SqliteDialect());
                    var sqlGenerator = new SqlGeneratorImpl(config);

                    _instance = new Database(connection, sqlGenerator);
                }

                return _instance;
            }
            catch (Exception ex)
            {
                LogUtility.Log(LogUtility.LogType.SystemError, MethodBase.GetCurrentMethod().Name, ex.Message);
                throw new Exception("Erro ao instanciar o banco de dados"); //TODO: put on resource file
            }
        }

        #endregion
    }

    public enum DatabaseType
    {
        SQLite
    }
}
