namespace Droid.Database
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Data;
    using System.IO;
    using System.Diagnostics;
    using MySql.Data.MySqlClient;

    public static class MySqlAdapter
    {
        #region Attribute
        private const string CONNCECTIONSTRINGFORMAT = @"Data Source={0};Database={1};Uid={2};Pwd={3};Persist Security Info=yes";
        private const string DUMPBACKUPSTRINGFORMAT = @" -u {0} -p {1} {2} --result-file ";

        private static string _user = string.Empty; // Tools4Libraries.Params.DatabaseLogin; // like tmontaufray
        private static string _server = string.Empty; //Tools4Libraries.Params.DatabaseHost; // localhost
        private static string _password = string.Empty; //Tools4Libraries.Params.DatabasePassword; // Ch@ng3It
        private static string _database = string.Empty; //Tools4Libraries.Params.DatabaseName; // mydatabasedev01
        private static string _connectionString;
        private static string _port;
        #endregion

        #region Properties
        public static string ConnectionString
        {
            get { return _connectionString; }
            set { _connectionString = value; ParseConnectionString(); }
        }
        public static string Database
        {
            get { return _database; }
            set { _database = value; }
        }
        public static string Password
        {
            get { return _password; }
            set { _password = value; }
        }
        public static string Server
        {
            get { return _server; }
            set { _server = value; }
        }
        public static string User
        {
            get { return _user; }
            set { _user = value; }
        }
        public static string Port
        {
            get { return _port; }
            set { _port = value; }
        }
        #endregion

        #region Methods public
        public static bool CreateSchema(string database, string name)
        {
            return ExecuteQuery(string.Empty, string.Format("CREATE SCHEMA IF NOT EXISTS {0}", name));
        }
        public static bool SchemaExist(string database, string name)
        {
            return false;
        }
        public static bool CreateDatabase(string name)
        {
            return ExecuteQuery(string.Empty, string.Format("create database " + name));
        }
        public static List<string> GetDatabases()
        {
            List<string> sysDatabases = new List<string>();
            sysDatabases.Add("sys");
            sysDatabases.Add("information_schema");
            sysDatabases.Add("mysql");
            sysDatabases.Add("performance_schema");
            try
            {
                List<string> retVal = new List<string>();
                foreach (DataRow item in ExecuteReader(null, "SHOW DATABASES").Rows)
                {
                    if (!sysDatabases.Contains(item[0].ToString().ToLower()))
                    { 
                        retVal.Add(item[0].ToString());
                    }
                }
                return retVal;
            }
            catch (Exception exp)
            {
                Console.WriteLine("No database connection. " + exp.Message);
                return null;
            }
        }
        public static string[] ShowTable(string database, string schema)
        {
            try
            {
                DataTable table = ExecuteReader(schema, "show tables");
                string[] ret = new string[table.Rows.Count];
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    ret[i] = table.Rows[i].ItemArray[0].ToString();
                }
                return ret;
            }
            catch
            {
                Console.WriteLine("No database connection.");
                return null;
            }
        }
		public static bool InsertOnDuplicateKey(string[] allColumnsName, string[] primaryColumnsName, string[] values)
		{
            try
            {
				StringBuilder sb = new StringBuilder();
				// TODO : add this query with parameters
				// insert into keyprofil (bikey, offset, value, offsetdiff, usualdiff) values 
				// ('b_o', 1, 128, 1, 3), 
				// ('o_n', 1, 186, 1, 4)
				// on duplicate key update offset=VALUES(offset), value=VALUES(value), offsetdiff=VALUES(offsetdiff), usualdiff=VALUES(usualdiff);

                List<string> retVal = new List<string>();
                return false;
            }
            catch
            {
                Console.WriteLine("Fail to insert rows.");
                return false;
            }
		}
        public static void DropDatabase(string database)
        {
            ExecuteQuery("", "drop database " + database);
        }
        public static bool IsConnectionPossible()
        {
            try
            {
                MySqlConnection conDatabase = new MySqlConnection(string.Format(CONNCECTIONSTRINGFORMAT, _server, _database, _user, _password));
                MySqlCommand cmdDatabase = new MySqlCommand("show tables", conDatabase);

                conDatabase.Open();
                cmdDatabase.ExecuteNonQuery();
                conDatabase.Close();
                return true;
            }
            catch
            {
                Console.WriteLine("No database connection.");
                return false;
            }
        }
        public static bool Backup(string filePath)
        {
            try
            {
                if (!string.IsNullOrEmpty(filePath))
                {
                    Process MySqlDump = new Process();
                    MySqlDump.StartInfo.FileName = @"mysqldump.exe";
                    MySqlDump.StartInfo.UseShellExecute = false;
                    MySqlDump.StartInfo.Arguments = string.Format(DUMPBACKUPSTRINGFORMAT, _user, _password, _database) + filePath;
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
        public static bool ExecuteScript(string fileName)
        {
            if (!string.IsNullOrEmpty(fileName))
            {
                try
                {
                    string strCmd = File.ReadAllText(fileName);
                    MySqlConnection conDatabase = new MySqlConnection(string.Format(CONNCECTIONSTRINGFORMAT, _server, _database, _user, _password));
                    MySqlCommand cmdDatabase = new MySqlCommand(strCmd, conDatabase);

                    conDatabase.Open();
                    cmdDatabase.ExecuteNonQuery();
                    conDatabase.Close();
                    return true;
                }
                catch (Exception exp)
                {
                    Console.WriteLine("DB execute script error : " + exp.Message);
                    return false;
                }
            }
            else return false;
        }
        public static bool ExecuteQuery(string schema, string query)
        {
            if (!string.IsNullOrEmpty(query))
            {
                try
                {
                    MySqlConnection conDatabase = new MySqlConnection(string.Format(CONNCECTIONSTRINGFORMAT, _server, _database, _user, _password));
                    MySqlCommand cmdDatabase = string.IsNullOrEmpty(schema) ? new MySqlCommand(query, conDatabase) : new MySqlCommand(string.Format("use {0};", schema) + query, conDatabase);

                    string s = Environment.CurrentDirectory;
                    conDatabase.Open();
                    cmdDatabase.ExecuteNonQuery();
                    conDatabase.Close();
                    return true;
                }
                catch (Exception exp)
                {
                    Console.WriteLine("DB execute query error : " + exp.Message);
                    return false;
                }
            }
            else return false;
        }
        public static DataTable Query(string query)
        {
            try
            {
                DataTable dt = new DataTable();
                List<string[]> ret = new List<string[]>();
                if (!string.IsNullOrEmpty(query))
                {
                    MySqlConnection conDatabase = new MySqlConnection(string.Format(CONNCECTIONSTRINGFORMAT, _server, _database, _user, _password));
                    MySqlCommand cmdDatabase = new MySqlCommand(query, conDatabase);

                    conDatabase.Open();
                    MySqlDataReader reader = cmdDatabase.ExecuteReader();

                    dt.BeginLoadData();
                    dt.Load(reader);
                    dt.EndLoadData();

                    conDatabase.Close();
                    return dt;
                }
                return dt;
            }
            catch (Exception exp4242)
            {
                Console.WriteLine("[ ERR : 4242 ] Cannot execute query on database.\n" + exp4242.Message);
                return null;
            }
        }
        public static DataTable ExecuteReader(string schema, string query)
        {
            try
            {
                Console.WriteLine("QUERY : " + query);
                DataColumn column;
                DataRow row;
                DataTable table = new DataTable();
                if (!string.IsNullOrEmpty(query))
                {
                    MySqlConnection conDatabase = new MySqlConnection(string.Format(CONNCECTIONSTRINGFORMAT, _server, _database, _user, _password));
                    MySqlCommand cmdDatabase = string.IsNullOrEmpty(schema) ? new MySqlCommand(query, conDatabase) : new MySqlCommand(string.Format("use {0};", schema) + query, conDatabase);

                    conDatabase.Open();
                    MySqlDataReader reader = cmdDatabase.ExecuteReader();

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
                                if (!reader.IsDBNull(i))
                                { 
                                    row[i] = reader.GetString(i);
                                }
                            }
                            catch (Exception exp)
                            {
                                Console.WriteLine(exp.Message);
                                // TODO : see what we can do
                            }
                        }
                        table.Rows.Add(row);
                    }

                    conDatabase.Close();
                }
                return table;
            }
            catch (Exception exp4242)
            {
                Console.WriteLine("[ ERR : 4242 ] Cannot execute query on database.\n" + exp4242.Message);
                return new DataTable();
            }
        }
        public static void LoadData(string fileName, string table)
        {
            MySqlConnection conDatabase = new MySqlConnection(string.Format(CONNCECTIONSTRINGFORMAT, _server, _database, _user, _password));
            MySqlBulkLoader loader = new MySqlBulkLoader(conDatabase);
            loader.TableName = table;
            loader.FileName = fileName;
            loader.FieldTerminator = ";";
            loader.Load();
        }
        public static DataTable Desc(string schema, string tableName)
        {
            return ExecuteReader(schema, string.Format("desc {0}.{1};", schema, tableName));
        }
        #endregion

        #region Methods private
        private static void InsertData(string data, string tableName)
        {
            string[] parameters = data.Split(';');
            MySqlCommand cmd;
            using (MySqlConnection connection = new MySqlConnection())
            {
                try
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append(string.Format("INSERT INTO {0} VALUES(", tableName));
                    foreach (string item in parameters)
	                {
		                sb.Append(string.Format("'{0}', ", item));
	                }
                    sb.Append(")");

                    connection.ConnectionString = string.Format(CONNCECTIONSTRINGFORMAT, _server, _database, _user, _password);
                    connection.Open();
                    cmd = connection.CreateCommand();
                    cmd.CommandText = sb.ToString();
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
                finally
                {
                    connection.Close();
                }
            }
        }
        private static DataSet ReadData(string tableName)
        {
            DataSet ds = new DataSet();                    
            using (MySqlConnection connection = new MySqlConnection())
            {
                try
                {
                    connection.Open();
                    MySqlCommand cmd = connection.CreateCommand();
                    cmd.CommandText = @"SELECT * FROM " + tableName + ";";
                    MySqlDataAdapter adapt = new MySqlDataAdapter(cmd);
                    adapt.Fill(ds);
                    connection.Close();
                }
                finally
                {
                    connection.Close();
                }
            }
            return ds;
        }
        private static void ParseConnectionString()
        {
            if (!string.IsNullOrEmpty(_connectionString))
            { 
                string[] tab = _connectionString.Split(';');
                if (tab.Length > 3)
                {
                    _server = tab[0].Split('=')[1];
                    _database = tab[1].Split('=')[1];
                    _user = tab[2].Split('=')[1];
                    _password = tab[3].Split('=')[1];
                }
            }
        }
        #endregion

        #region Event
        #endregion
    }
}
