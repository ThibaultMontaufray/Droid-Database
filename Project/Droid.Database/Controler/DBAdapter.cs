using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Droid.Database
{
    public static class DBAdapter
    {
        #region Attributes
        private static DataBasesType _dbType;
        private static string _user;
        private static string _port;
        private static string _server;
        private static string _password;
        private static string _database;
        #endregion

        #region Properties
        public static DataBasesType DbType
        {
            get { return _dbType; }
            set { _dbType = value; }
        }
        public static string Port
        {
            get { return _port; }
            set
            {
                _port = value;
                if (_dbType == DataBasesType.MYSQL)
                {
                    MySqlAdapter.Port = _port;
                }
                else if (_dbType == DataBasesType.POSTGRESQL)
                {
                    PostgreSqlAdapter.Port = _port;
                }
            }
        }
        public static string Database
        {
            get { return _database; }
            set
            {
                _database = value;
                if (_dbType == DataBasesType.MYSQL)
                {
                    MySqlAdapter.Database = _database;
                }
                else if (_dbType == DataBasesType.POSTGRESQL)
                {
                    PostgreSqlAdapter.Database = _database;
                }
            }
        }
        public static string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                if (_dbType == DataBasesType.MYSQL)
                {
                    MySqlAdapter.Password = _password;
                }
                else if (_dbType == DataBasesType.POSTGRESQL)
                {
                    PostgreSqlAdapter.Password = _password;
                }
            }
        }
        public static string Server
        {
            get { return _server; }
            set
            {
                _server = value;
                if (_dbType == DataBasesType.MYSQL)
                {
                    MySqlAdapter.Server = _server;
                }
                else if (_dbType == DataBasesType.POSTGRESQL)
                {
                    PostgreSqlAdapter.Server = _server;
                }
            }
        }
        public static string User
        {
            get { return _user; }
            set
            {
                _user = value;
                if (_dbType == DataBasesType.MYSQL)
                {
                    MySqlAdapter.User = _user;
                }
                else if (_dbType == DataBasesType.POSTGRESQL)
                {
                    PostgreSqlAdapter.User = _user;
                }
            }
        }
        public static bool IsConnected
        {
            get
            {
                if (_dbType == DataBasesType.MYSQL)
                {
                    return MySqlAdapter.IsConnectionPossible();
                }
                else if (_dbType == DataBasesType.POSTGRESQL)
                {
                    return PostgreSqlAdapter.IsConnected;
                }
                else
                {
                    return false;
                }
            }
        }
        #endregion

        #region Constructor
        #endregion

        #region Methods public
        public static bool CreateDatabase(string name)
        {
            try
            {
                if (_dbType == DataBasesType.MYSQL)
                {
                    return MySqlAdapter.CreateDatabase(name);
                }
                else if (_dbType == DataBasesType.POSTGRESQL)
                {
                    return PostgreSqlAdapter.CreateDatabase(name);
                }
                return false;
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.Message);
                return false;
            }
        }
        public static bool CreateSchema(string database, string name)
        {
            try
            {
                if (_dbType == DataBasesType.MYSQL)
                {
                    return MySqlAdapter.CreateSchema(database, name);
                }
                else if (_dbType == DataBasesType.POSTGRESQL)
                {
                    return PostgreSqlAdapter.CreateSchema(database, name);
                }
                return false;
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.Message);
                return false;
            }
        }
        public static bool SchemaExist(string database, string name)
        {
            try
            {
                if (_dbType == DataBasesType.MYSQL)
                {
                    return MySqlAdapter.SchemaExist(database, name);
                }
                else if (_dbType == DataBasesType.POSTGRESQL)
                {
                    return PostgreSqlAdapter.SchemaExist(database, name);
                }
                return false;
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.Message);
                return false;
            }
        }
        public static DataTable ExecuteReader(string schema, string query)
        {
            if (_dbType == DataBasesType.MYSQL)
            {
                return MySqlAdapter.ExecuteReader(schema, query);
            }
            else if (_dbType == DataBasesType.POSTGRESQL)
            {
                return PostgreSqlAdapter.ExecuteReader(schema, query);
            }
            else
            {
                return null;
            }
        }
        public static bool ExecuteQuery(string schema, string query)
        {
            if (_dbType == DataBasesType.MYSQL)
            {
                return MySqlAdapter.ExecuteQuery(schema, query);
            }
            else if (_dbType == DataBasesType.POSTGRESQL)
            {
                return PostgreSqlAdapter.ExecuteQuery(schema, query);
            }
            else
            {
                return false;
            }
        }
        public static void DropDatabase(string database)
        { 
            if (_dbType == DataBasesType.MYSQL)
            {
                MySqlAdapter.DropDatabase(database);
            }
            else if (_dbType == DataBasesType.POSTGRESQL)
            {
                PostgreSqlAdapter.DropDatabase(database);
            }
            else
            {
                throw new Exception("No database type");
            }
        }
        public static string[] ShowTable(string database, string schema)
        {
            if (_dbType == DataBasesType.MYSQL)
            {
                return MySqlAdapter.ShowTable(database, schema);
            }
            else if (_dbType == DataBasesType.POSTGRESQL)
            {
                return PostgreSqlAdapter.ShowTables(database, schema);
            }
            else
            {
                return null;
            }
        }
        public static DataTable Desc(string schema, string tableName)
        {
            if (_dbType == DataBasesType.MYSQL)
            {
                return MySqlAdapter.Desc(schema, tableName);
            }
            else if (_dbType == DataBasesType.POSTGRESQL)
            {
                return PostgreSqlAdapter.Desc(schema, tableName);
            }
            else
            {
                return null;
            }
        }
        public static List<string> GetDatabases()
        {
            if (_dbType == DataBasesType.MYSQL)
            {
                return MySqlAdapter.GetDatabases();
            }
            else if (_dbType == DataBasesType.POSTGRESQL)
            {
                return PostgreSqlAdapter.GetDatabases();
            }
            else
            {
                return null;
            }
        }
        public static bool Backup(string backupPath)
        {
            if (_dbType == DataBasesType.MYSQL)
            {
                return MySqlAdapter.Backup(backupPath);
            }
            else if (_dbType == DataBasesType.POSTGRESQL)
            {
                return PostgreSqlAdapter.Backup(backupPath);
            }
            else
            {
                throw new Exception("No database type");
                //return false;
            }
        }
        #endregion

        #region Methods private
        #endregion
    }
}
