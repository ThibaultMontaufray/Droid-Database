namespace Droid_Database
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Data;
    using System.IO;
    using System.Diagnostics;
    using MySql.Data.MySqlClient;
    using Tools4Libraries;

    public static class MySqlAdapter
    {
        #region Attribute
        private static string _connectionString = @"Data Source={0};Database={1};Uid={2};Pwd={3};Persist Security Info=yes";
        private static string _dumpBackupString = @" -u {0} -p {1} {2} --result-file ";

        private static string _user = Tools4Libraries.Params.DatabaseLogin; // like tmontaufray
        private static string _source = Tools4Libraries.Params.DatabaseHost; // localhost
        private static string _password = Tools4Libraries.Params.DatabasePassword; // Ch@ng3It
        private static string _database = Tools4Libraries.Params.DatabaseName; // mydatabasedev01
        #endregion

        #region Properties
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
        public static string Source
        {
            get { return _source; }
            set { _source = value; }
        }
        public static string User
        {
            get { return _user; }
            set { _user = value; }
        }
        #endregion

        #region Methods public
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
                foreach (var item in ExecuteReader("show databases"))
                {
                    if (!sysDatabases.Contains(item[0].ToLower()))
                    { 
                        retVal.Add(item[0]);
                    }
                }
                return retVal;
            }
            catch (Exception exp)
            {
                Console.WriteLine("No database connection.");
                return null;
            }
        }
        public static List<string> ShowTable()
        {
            try
            {
                List<string> retVal = new List<string>();
                foreach (var item in ExecuteReader("show tables"))
                {
                    retVal.Add(item[0]);
                }
                return retVal;
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
        public static bool IsConnectionPossible()
        {
            try
            {
                MySqlConnection conDatabase = new MySqlConnection(string.Format(_connectionString, _source, _database, _user, _password));
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
                    MySqlDump.StartInfo.Arguments = string.Format(_dumpBackupString, _user, _password, _database) + filePath;
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
                Log.Write("[ ERR : 4200 ] Cannot export data from database.\n" + exp4200.Message);
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
                    MySqlConnection conDatabase = new MySqlConnection(string.Format(_connectionString, _source, _database, _user, _password));
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
        public static bool ExecuteQuery(string query)
        {
            if (!string.IsNullOrEmpty(query))
            {
                try
                {
                    MySqlConnection conDatabase = new MySqlConnection(string.Format(_connectionString, _source, _database, _user, _password));
                    MySqlCommand cmdDatabase = new MySqlCommand(query, conDatabase);

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
        public static List<string[]> ExecuteReader(string query)
        {
            try
            {
                Console.WriteLine("QUERY : " + query);
                List<string[]> ret = new List<string[]>();
                if (!string.IsNullOrEmpty(query))
                {
                    MySqlConnection conDatabase = new MySqlConnection(string.Format(_connectionString, _source, _database, _user, _password));
                    MySqlCommand cmdDatabase = new MySqlCommand(query, conDatabase);

                    conDatabase.Open();
                    MySqlDataReader reader = cmdDatabase.ExecuteReader();
                    while (reader.Read())
                    {
                        string[] str = new string[reader.FieldCount];
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            str[i] = reader[i].ToString();
                        }
                        ret.Add(str);
                    }
                    conDatabase.Close();
                }
                return ret;
            }
            catch (Exception exp4242)
            {
                Log.Write("[ ERR : 4242 ] Cannot execute query on database.\n" + exp4242.Message);
                return new List<string[]>();
            }
        }
        public static void LoadData(string fileName, string table)
        {
            MySqlConnection conDatabase = new MySqlConnection(string.Format(_connectionString, _source, _database, _user, _password));
            MySqlBulkLoader loader = new MySqlBulkLoader(conDatabase);
            loader.TableName = table;
            loader.FileName = fileName;
            loader.FieldTerminator = ";";
            loader.Load();
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

                    connection.ConnectionString = string.Format(_connectionString, _source, _database, _user, _password);
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
        #endregion

        #region Event
        #endregion
    }
}
