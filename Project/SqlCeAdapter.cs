using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlServerCe;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace Droid_database
{
    public enum DBType
    {
        BIT,
        INT,
        NVARCHART
    }

    public class SqlCeAdapter
    {
        #region Attribute
        private DBConnection _conn;
        #endregion

        #region Properties
        public DBConnection Connection
        {
            get { return _conn; }
            set { _conn = value; }
        }
        #endregion

        #region Constructor
        public SqlCeAdapter()
        {
            _conn = new DBConnection();
            _conn.Connect();
            _conn.InitBase();
        }
        ~SqlCeAdapter()
        {
            if (_conn != null) _conn.Disconnect();
        }
        #endregion

        #region Methods public
        public static void InitBase(string scriptPath)
        {
            SqlCeConnection sqlConnection = new SqlCeConnection();
            sqlConnection.ConnectionString = @"Data Source=|DataDirectory|\DATABASE\DBTOBI.sdf;";
            var fileContent = File.ReadAllText(scriptPath);
            var sqlqueries = fileContent.Split(new[] { " GO " }, StringSplitOptions.RemoveEmptyEntries);

            var cmd = new SqlCeCommand("filePath", sqlConnection);

            if (sqlConnection.State != ConnectionState.Open) sqlConnection.Open();
            foreach (var query in sqlqueries)
            {
                try
                {
                    cmd.CommandText = query;
                    cmd.ExecuteNonQuery();
                }
                catch (Exception exp)
                {
                    Console.WriteLine(exp.Message);
                    //MessageBox.Show("Cannot manage that filePath : \n" + filePath + "\n\n" + exp.Message, "Crash !!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            sqlConnection.Close();
        }

        public static List<List<string>> GetVers(string rime, int pieds, string domain)
        {
            List<List<string>> masterList = new List<List<string>>();
            List<string> tempList;

            SqlCeConnection sqlConnection = new SqlCeConnection();
            sqlConnection.ConnectionString = @"Data Source=|DataDirectory|\DATABASE\DBTOBI.sdf;";

            sqlConnection.Open();

            string sql = "select * from TINS_VERS where rime = '" + rime + "' and PIEDS = " + pieds + " and DOMAINE = '" + domain + "'";
            //string sql = "select * from TINS_VERS where rime = '" + rime + "' and PIEDS = " + pieds;
            SqlCeCommand cmdGetOldMasterId = new SqlCeCommand(sql, sqlConnection);
            SqlCeDataReader reader = cmdGetOldMasterId.ExecuteReader();
            while (reader.Read())
            {
                tempList = new List<string>();
                tempList.Add(reader[1].ToString()); // text
                tempList.Add(reader[2].ToString()); // rime
                tempList.Add(reader[3].ToString()); // domain

                masterList.Add(tempList);
            }
            sqlConnection.Close();

            return masterList;
        }
        public static List<string> GetRime(int pieds, List<string> domains)
        {
            if (domains == null) return null;
            List<string> retList = new List<string>();
            string formatedDomain = domains.Count > 2 ? " and DOMAINE in ('" + domains[0] + "', '" + domains[1] + "', '" + domains[2] + "', '" + domains[3] + "') " : "";

            SqlCeConnection sqlConnection = new SqlCeConnection();
            sqlConnection.ConnectionString = @"Data Source=|DataDirectory|\DATABASE\DBTOBI.sdf;";

            sqlConnection.Open();

            string sql = "select RIME from TINS_VERS where PIEDS = " + pieds + formatedDomain + " _group by RIME";
            SqlCeCommand cmdGetOldMasterId = new SqlCeCommand(sql, sqlConnection);
            SqlCeDataReader reader = cmdGetOldMasterId.ExecuteReader();
            while (reader.Read())
            {
                retList.Add(reader[0].ToString());
            }
            sqlConnection.Close();

            return retList;
        }
        public static List<List<string>> GetSchema()
        {
            List<List<string>> ls = new List<List<string>>();
            List<string> tempList;
            try
            {

                SqlCeConnection sqlConnection = new SqlCeConnection();
                sqlConnection.ConnectionString = @"Data Source=|DataDirectory|\DATABASE\DBTOBI.sdf;";

                sqlConnection.Open();

                string sql = "select DOMAINE1, DOMAINE2, DOMAINE3, DOMAINE4 from TINS_SCHEMA";
                SqlCeCommand cmdGetOldMasterId = new SqlCeCommand(sql, sqlConnection);
                SqlCeDataReader reader = cmdGetOldMasterId.ExecuteReader();
                while (reader.Read())
                {
                    tempList = new List<string>();
                    tempList.Add(reader[0].ToString());
                    tempList.Add(reader[1].ToString());
                    tempList.Add(reader[2].ToString());
                    tempList.Add(reader[3].ToString());
                    ls.Add(tempList);
                }
                sqlConnection.Close();
            }
            catch (Exception exp)
            {
                MessageBox.Show("Aïe aïe aïe, there is again some bugs : \n\n" + exp.Message, "Crash !!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return ls;
        }
        public static int GetTotalVers(string rime, int pieds, List<string> domains)
        {
            int ret = 0;
            string formatedDomain = domains.Count > 2 ? " and DOMAINE in ('" + domains[0] + "', '" + domains[1] + "', '" + domains[2] + "', '" + domains[3] + "')" : "";

            SqlCeConnection sqlConnection = new SqlCeConnection();
            sqlConnection.ConnectionString = @"Data Source=|DataDirectory|\DATABASE\DBTOBI.sdf;";

            sqlConnection.Open();

            //string sql = "select count(*) from TINS_VERS where RIME = '" + rime + "' and PIEDS = " + pieds;
            string sql = "select count(*) from TINS_VERS where RIME = '" + rime + "' and PIEDS = " + pieds + formatedDomain;
            SqlCeCommand cmdGetOldMasterId = new SqlCeCommand(sql, sqlConnection);
            SqlCeDataReader reader = cmdGetOldMasterId.ExecuteReader();
            while (reader.Read())
            {
                int.TryParse(reader[0].ToString(), out ret);
            }
            sqlConnection.Close();

            return ret;
        }
        #endregion

        #region Methods private
        #endregion
    }
}
