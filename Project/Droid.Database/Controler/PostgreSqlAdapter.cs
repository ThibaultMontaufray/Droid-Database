using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Droid.Database
{
    public static class PostgreSqlAdapter
    {
        #region Attributes
        private const string CONNCECTIONSTRINGFORMAT = @"Data Source={0};Database={1};Uid={2};Pwd={3};Persist Security Info=yes";
        private const string CONNECTIONSTRING = "Server={0};Port={1};User Id={2};Password={3};Database={4};";
        private const string GETUSERS = "" +
            "SELECT u.usename AS \"User name\"," +
            "  CASE WHEN u.usesuper AND u.usecreatedb THEN CAST('superuser, create database' AS pg_catalog.text)" +
            "       WHEN u.usesuper THEN CAST('superuser' AS pg_catalog.text)" +
            "       WHEN u.usecreatedb THEN CAST('create database' AS pg_catalog.text)" +
            "       ELSE CAST('' AS pg_catalog.text)" +
            "  END AS \"Attributes\"" +
            "FROM pg_catalog.pg_user u" +
            "ORDER BY 1;";

        public static DataSet Dataset;
        public static DataTable _datatable;
        public static NpgsqlConnection _conn;

        private static string _version;
        private static string _os;
        private static string _processor;
        private static string _user;
        private static string _password;
        private static string _server;
        private static string _port;
        private static string _database;
        #endregion

        #region Properties
        public static  string Database
        {
            get { return _database; }
            set { _database = value; }
        }
        public static  string Port
        {
            get { return _port; }
            set { _port = value; }
        }
        public static  string Server
        {
            get { return _server; }
            set { _server = value; }
        }
        public static  string Password
        {
            get { return _password; }
            set { _password = value; }
        }
        public static  string User
        {
            get { return _user; }
            set { _user = value; }
        }
        public static  string Processor
        {
            get { return _processor; }
            set { _processor = value; }
        }
        public static  string Os
        {
            get { return _os; }
            set { _os = value; }
        }
        public static string Version
        {
            get { return _version; }
            set { _version = value; }
        }

        public static  string CurrentDatabase
        {
            get { return GetAttribute(null, "SELECT current_database();"); }
        }
        public static  string ClientAddress
        {
            get { return GetAttribute(null, "SELECT inet_client_addr();"); }
        }
        public static  string ClientPort
        {
            get { return GetAttribute(null, "SELECT inet_client_port();"); }
        }
        public static  string ServerAddress
        {
            get { return GetAttribute(null, "SELECT inet_server_addr();"); }
        }
        public static  string ServerPort
        {
            get { return GetAttribute(null, "SELECT inet_server_port();"); }
        }
        public static  string StartTime
        {
            get { return GetAttribute(null, "SELECT pg_postmaster_start_time();"); }
        }
        public static  bool IsConnected
        {
            get
            {
                try
                {
                    _conn = new NpgsqlConnection(String.Format(CONNECTIONSTRING, _server, _port, _user, _password, _database));
                    _conn.Open();
                    _conn.Close();
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine("No connection on " + _server + "/" + _database + " : " + e.Message);
                    return false;
                }
            }
        }
        public static  string[] GetUsers
        {
            get
            {
                DataTable dt = new DataTable();
                try
                {
                    string[] result = null;
                    _conn = new NpgsqlConnection(String.Format(CONNECTIONSTRING, _server, Port, _user, _password, _database));
                    _conn.Open();
                    dt = ExecuteReader(null, GETUSERS);
                    _conn.Close();
                    
                    result = new string[dt.Rows.Count];
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        result[i] = dt.Rows[i].ItemArray[0].ToString();
                    }
                    return result;
                }
                catch (Exception e)
                {
                    Console.WriteLine("No connection on " + _server + "/" + _database + " : " + e.Message);
                    return null;
                }
            }
        }
        #endregion

        #region Constructor
        //PostgreSqlAdapter(string name, string user, string pwd, string server)
        //{
        //    _port = "5432";
        //    _name = name;
        //    _user = user;
        //    _password = pwd;
        //    _server = server;

        //    IsAlive();
        //}
        #endregion

        #region Methods public
        public static bool IsAlive()
        {
            try
            {
                DataTable dt = ExecuteReader(null, "SELECT version();");
                string dump = dt.Rows[0][0].ToString();
                string[] tabSpace = dump.Split(' ');
                string[] tabComma = dump.Split(',');
                _version = ExecuteReader(null, "SHOW server_version;").Rows[0][0].ToString();
                _os = tabComma.Length > 1 ? tabComma[1] : string.Empty;
                _processor = tabComma.Length > 1 ? tabComma[2] : string.Empty;

                return true;
            }
            catch
            {
                return false;
            }
        }
        public static bool ExecuteQuery(string schema, string query)
        {
            // TODO : use schema
            try
            {
                CleanQuery(ref query);
                using (NpgsqlConnection conn = new NpgsqlConnection(string.Format("Server={0};User Id={1}; Password={2};Database={3};;Port={4}", Server, User, Password, (string.IsNullOrEmpty(schema) ? "postgres" : schema), Port)))
                {
                    conn.Open();
                    NpgsqlCommand command = new NpgsqlCommand(query.Replace('`', '\''), conn);
                    object o = command.ExecuteScalar();
                }
                return true;
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.Message);
                return false;
            }
        }
        public static bool CreateDatabase(string name)
        {
            string defaultDb = GetDatabases().Last();
            return ExecuteQuery(defaultDb, string.Format("create database {0}", name));
        }
        public static bool CreateSchema(string database, string name)
        {
            return ExecuteQuery(database, string.Format("CREATE SCHEMA IF NOT EXISTS {0}", name));
        }
        public static bool SchemaExist(string database, string name)
        {
            DataTable table = ExecuteReader(database, string.Format("select * from pg_catalog.pg_namespace where nspname = '{0}';", name));
            return (table != null && table.Rows.Count > 0);
        }
        public static DataTable ExecuteReader(string schema, string query)
        {
            try
            {
                CleanQuery(ref query);
                object val;
                DataTable table = new DataTable();
                DataColumn column;
                DataRow row;

                using (NpgsqlConnection conn = new NpgsqlConnection(string.Format("Server={0};User Id={1}; Password={2};Database={3};Port={4}", Server, User, Password, (string.IsNullOrEmpty(schema) ? "postgres" : schema), Port)))
                {
                    conn.Open();
                    NpgsqlCommand command = new NpgsqlCommand(query, conn);
                    NpgsqlDataReader reader = command.ExecuteReader();

                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        column = new DataColumn();
                        // TODO : put the correct type, later ....
                        column.DataType = System.Type.GetType("System.String");
                        column.ColumnName = reader.GetName(i);
                        table.Columns.Add(column);
                    }

                    while (reader.Read())
                    {
                        row = table.NewRow();
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            try
                            {
                                val = reader.GetValue(i);
                                row[i] = val != null ? val.ToString() : string.Empty;
                            }
                            catch (Exception exp)
                            {
                                Console.WriteLine(exp.Message);
                                // TODO : see what we can do
                            }
                        }
                        table.Rows.Add(row);
                    }
                    conn.Close();
                }
                return table;
            }
            catch (Exception exp)
            {
                Console.WriteLine("Database critical error : " + exp.Message);
                return null;
            }
        }
        public static string GetAttribute(string schema, string query)
        {
            string ret = string.Empty;
            CleanQuery(ref query);
            DataTable dt = ExecuteReader(schema, query);
            if (dt != null && dt.Rows.Count > 0 && dt.Columns.Count > 0)
            {
                ret = dt.Rows[0][0].ToString();
            }
            return ret;
        }
        public static DataTable Desc(string schema, string tableName)
        {
            return ExecuteReader(schema, string.Format("select column_name, data_type, is_nullable, column_default from information_schema.columns  where table_name = '{0}.{1}';", schema, tableName));
        }
        public static void DropDatabase(string database)
        {
            ExecuteQuery("", "drop database " + database);
        }
        public static List<string> GetDatabases()
        {
            List<string> databases = new List<string>();
            DataTable table = ExecuteReader(null, "SELECT datname FROM pg_catalog.pg_database");
            for (int i = 0; i < table.Rows.Count; i++)
            {
                databases.Add(table.Rows[i].ItemArray[0].ToString());
            }
            return databases;
        }
        public static string[] ShowTables(string database, string schema)
        {
            DataTable dt = new DataTable();
            string[] result = null;
            //dt = ExecuteReader(schema, "SELECT * FROM pg_catalog.pg_tables WHERE schemaname != 'pg_catalog' AND schemaname != 'information_schema';");
            dt = ExecuteReader(database, string.Format("SELECT tablename FROM pg_catalog.pg_tables WHERE schemaname = '{0}';", schema));
            result = new string[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                result[i] = dt.Rows[i].ItemArray[0].ToString();
            }
            return result;
        }

        public static DataTable Query(string schema, string connectionstring, string query)
        {
            // TODO : use schema
            DataTable table;

            CleanQuery(ref query);
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionstring))
            {
                using (NpgsqlCommand command = new NpgsqlCommand())
                {
                    command.CommandType = CommandType.Text;
                    command.CommandText = query;
                    command.Connection = connection;

                    try
                    {
                        connection.Open();

                        using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command))
                        {
                            table = new DataTable();
                            adapter.Fill(table);
                        }

                        connection.Close();
                        connection.Dispose();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }

            return table;
        }
        public static DataTable Query(string schema, NpgsqlConnection connectionstring, string query)
        {
            // TODO : use schema
            DataTable table;

            CleanQuery(ref query);
            using (NpgsqlCommand command = new NpgsqlCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = query;
                command.Connection = connectionstring;

                try
                {
                    //connectionstring.Open();

                    using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command))
                    {
                        table = new DataTable();
                        adapter.Fill(table);
                    }

                    //connectionstring.Close();
                    //connectionstring.Dispose();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return table;
        }
        public static int QueryWithReturn(string connectionstring, string query)
        {
            int result;
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionstring))
            {
                CleanQuery(ref query);
                using (NpgsqlCommand command = new NpgsqlCommand())
                {
                    command.CommandType = CommandType.Text;
                    command.CommandText = query;
                    command.Connection = connection;

                    try
                    {
                        connection.Open();

                        result = (int)command.ExecuteScalar();

                        connection.Close();
                        connection.Dispose();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }

            return result;
        }
        public static int QueryWithReturn(NpgsqlConnection connection, string query)
        {
            int result;

            using (NpgsqlCommand command = new NpgsqlCommand())
            {
                CleanQuery(ref query);
                command.CommandType = CommandType.Text;
                command.CommandText = query;
                command.Connection = connection;

                try
                {
                    result = (int)command.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return result;
        }
        public static bool ExecuteNonQuery(string connectionstring, string query)
        {
            bool result = false;

            using (NpgsqlConnection connection = new NpgsqlConnection(connectionstring))
            {
                CleanQuery(ref query);
                using (NpgsqlCommand command = new NpgsqlCommand())
                {
                    command.CommandType = CommandType.Text;
                    command.CommandText = query;
                    command.Connection = connection;

                    try
                    {
                        connection.Open();

                        command.ExecuteNonQuery();

                        result = true;

                        connection.Close();
                        connection.Dispose();
                    }
                    catch (Exception ex)
                    {
                        result = false;
                        throw ex;
                    }
                }
            }

            return result;
        }
        public static bool ExecuteNonQuery(string connectionstring, string query, IDictionary<string, string> dico)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionstring))
            {
                CleanQuery(ref query);
                NpgsqlCommand sqlCmd = new NpgsqlCommand(query, connection);

                foreach (KeyValuePair<string, string> item in dico)
                {

                    Guid result;
                    bool isValid = Guid.TryParse(item.Value, out result);
                    if (isValid)
                    {
                        sqlCmd.Parameters.Add(new NpgsqlParameter(item.Key, result));
                    }
                    else
                    {
                        sqlCmd.Parameters.Add(new NpgsqlParameter(item.Key, item.Value));
                    }
                }

                try
                {
                    connection.Open();

                    sqlCmd.ExecuteNonQuery();
                    connection.Close();

                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }
        }
        public static bool Backup(string filePath)
        {
            try
            {
                if (!string.IsNullOrEmpty(filePath))
                {
                    Process MySqlDump = new Process();
                    MySqlDump.StartInfo.FileName = @"pg_dump.exe";
                    MySqlDump.StartInfo.UseShellExecute = false;
                    MySqlDump.StartInfo.Arguments = string.Format("dbname > {0}", filePath);
                    MySqlDump.StartInfo.RedirectStandardInput = false;
                    MySqlDump.StartInfo.RedirectStandardOutput = false;

                    MySqlDump.Start();

                    MySqlDump.WaitForExit();
                    MySqlDump.Close();
                    return true;
                }
                else return false;
            }
            catch (IOException exp4200)
            {
                Console.WriteLine("[ ERR : 4200 ] Cannot export data from database.\n" + exp4200.Message);
                return false;
            }
        }
        #endregion

        #region Methods private
        private static void CleanQuery(ref string query)
        {
            query = query.Replace("`", string.Empty);
            query = query.Replace('"', '\'');
            query = query.Replace("\"\"", "NULL");
            query = query.Replace("int,", "integer,");
            query = query.Replace("int)", "integer)");
            query = query.Replace("int NOT", "integer NOT");
            query = query.Replace("AUTO_INCREMENT", "SERIAL");
            query = query.Replace("datetime", "timestamp");
            query = query.Replace("nvarchar", "character varying");
            query = query.Replace("varchar", "character varying");
            query = query.Replace("ENGINE=InnoDB DEFAULT CHARSET=utf8", string.Empty); // TODO : fix it
            query = query.Replace("ON DUPLICATE KEY UPDATE", "ON CONFLICT (id) DO UPDATE SET");
        }
        #endregion

        // TODO CHANGE COLUMN TYPE

        //ALTER TABLE ppartobid01.s_contexte ALTER COLUMN mandatory DROP DEFAULT;
        //ALTER TABLE ppartobid01.s_contexte ALTER mandatory TYPE bool USING mandatory::boolean;;
        //ALTER TABLE ppartobid01.s_contexte ALTER COLUMN mandatory SET DEFAULT FALSE;



//INSERT INTO ppartobid01.s_contexte 
//(fr_theme,fr_category,fr_information,en_theme,en_category,en_information,priorite,mandatory,personactor,persontarget,question,object,available,call,webservice_code) 
//VALUES
//( 'prendre', 'image', 'nom', 'take', 'picture', 'name', 0, TRUE, 1, 0, '', 'System.String', TRUE, 'Droid.Image.Interface_image.ACTION_130_take_picture', 130)
//ON CONFLICT(fr_theme, fr_category, fr_information, en_theme, en_category, en_information)
//DO UPDATE SET
//priorite = 0, mandatory = True, personActor = 1, personTarget = 0, question = '',object='System.String',available=True,call='Droid.Image.Interface_image.ACTION_130_take_picture',webservice_code=130;


    }
}
