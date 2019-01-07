using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlServerCe;
using System.Data.SqlClient;
using System.Data;
using System.IO;

//using Microsoft.SqlServer.Management.Common;
//using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer;
using System.Collections;

namespace Droid.Database
{
    public delegate void DBConnectionEventHandler();

    public class DBConnection
    {
        #region Attribute
        public event DBConnectionEventHandler ConnectionStatusChanged;
        public event DBConnectionEventHandler ResultChanged;

        private static SqlCeDataAdapter _sda = null;
        private string _status;
        private SqlCeConnection _sqlConnection;
        private DataSet _ds;
        private const string CREATION_TABLE_CLEAN = @"C:\Users\Amos\Google_Drive\_Assistant\Assistant\TOBI\DATABASE\CreationTable_clean.sql";
        private const string CREATION_TABLE_BUILD = @"C:\Users\Amos\Google_Drive\_Assistant\Assistant\TOBI\DATABASE\CreationTable_build.sql";
        #endregion

        #region Properties
        public string Status
        {
            get
            {
                return "Connection : " + _sqlConnection.State.ToString();
            }
            set
            {
                _status = value;
                OnStatusChanged();
            }
        }
        public DataSet DBDataSet
        {
            get { return _ds; }
        }
        #endregion

        #region Constructor
        public DBConnection()
        {
            _status = "";
            InitBase();
            CurdOperation();
            this.BindGrid();
        }
        #endregion

        #region Methods public
        public void Connect()
        {
            try
            {
                _sqlConnection.Open();

                Status = "Connected";
            }
            catch (Exception exp)
            {
                Status = "No connection : " + exp.Message;
            }
        }
        public void Disconnect()
        {
            _sqlConnection.Close();
            Status = "No connection";
        }
        public void InitBase()
        {
            _sqlConnection = new SqlCeConnection();
            _sqlConnection.ConnectionString = string.Empty;// @"Data Source=C:\Users\Amos\Google_Drive\_Assistant\Assistant\TOBI\DATABASE\DBTOBI.sdf";
            _sda = new SqlCeDataAdapter();
            _ds = new DataSet();
            
            QueryFILE("");
            // DBDroid dbm = new DBDroid(this);
        }

        public void AddTableTmp(string Label, string TParent, List<string> TListAttributes, List<string> TListChilds, List<string> TLinks)
        {
            // create table DEF + Label

            // foreign key on TParent table

            // add columns foreach attributes

            // add optionnals childs

            // link with the other tables with correct verb
        }

        /// <summary>
        /// In this method write code for Inserting data in 
        /// this table MyDemoTable.
        /// </summary>
        public void Add()
        {
            DataSet oldData;
            DataRow dr;
            FillDataInDataset(out oldData, out dr);
            dr = oldData.Tables[0].NewRow();
            
            dr["LABEL"] = "CACTUS";
            dr["NB_CHILD"] = "0";

            oldData.Tables[0].Rows.Add(dr);
            _sda.Update(oldData);
            BindGrid();
        }
        /// <summary>
        /// In this method write code for updating existing data.
        /// </summary>
        /// <param name="id"></param>
        public void Update(string id)
        {
            DataSet oldData;
            DataRow dr;
            FillDataInDataset(out oldData, out dr);
            //Here get record of specified id.
            Console.WriteLine("Get rid of this.");
            //DataRow[] tempdata = oldData.Tables[0].Where(p => p["LABEL"].ToString() == id).ToArray();
            //if (tempdata.Length > 0)
            //{
            //    dr = tempdata[0];
            //    dr["LABEL"] = "FLEUR";
            //    dr["TEMPO"] = "True";
            //    dr["FROM"] = "DEFINITION";
            //}
            //_sda.Update(oldData);
            BindGrid();
        }
        /// <summary>
        ///In this method write code for Deleting existing data. 
        /// </summary>
        /// <param name="id"></param>
        public void Delete(string id)
        {
            DataSet oldData;
            DataRow dr;
            FillDataInDataset(out oldData, out dr);
            //Here get record of specified id.
            Console.WriteLine("Get rid of this.");
            //DataRow[] tempdata = oldData.Tables[0].Where(p => p["LABEL"].ToString() == id).ToArray();
            //if (tempdata.Length > 0)
            //{
            //    dr = tempdata[0];
            //    dr["LABEL"] = "FLEUR";
            //    dr["TEMPO"] = "True";
            //    dr["FROM"] = "DEFINITION";
            //    dr.Delete();
            //}
            //_sda.Update(oldData);
            BindGrid();
        }

        public void Query_SELECT(string query)
        {

        }
        public void Query_UPDATE(string query)
        {

        }
        public void Query_DELETE(string query)
        {
        }
        public void QueryFILE(string filePath)
        {
            ////string sqlConnectionString = "Data Source=(local);Initial Catalog=AdventureWorks;Integrated Security=True";
            //FileInfo file = new FileInfo(@"C:\Users\Amos\Google_Drive\_Assistant\Assistant\TOBI\DATABASE\CreationTable.sql");
            //string script = file.OpenText().ReadToEnd();
            ////SqlConnection conn = new SqlConnection(sqlConnectionString);
            //Server server = new Server(new ServerConnection(_sqlConnection));
            //server.ConnectionContext.ExecuteNonQuery(script);

            try
            {
                var fileContent = File.ReadAllText(CREATION_TABLE_CLEAN);
                if (fileContent != null)
                {
                    var sqlqueries = fileContent.Split(new[] { " GO " }, StringSplitOptions.RemoveEmptyEntries);

                    var cmd = new SqlCeCommand("query", _sqlConnection);

                    if (_sqlConnection.State != ConnectionState.Open) _sqlConnection.Open();
                    foreach (var query in sqlqueries)
                    {
                        cmd.CommandText = query;
                        cmd.ExecuteNonQuery();
                    }
                    _sqlConnection.Close();
                }
            }
            catch (Exception)
            {
            }
            try
            {
                var fileContent = File.ReadAllText(CREATION_TABLE_BUILD);
                if (fileContent != null)
                {
                    var sqlqueries = fileContent.Split(new[] { " GO " }, StringSplitOptions.RemoveEmptyEntries);

                    var cmd = new SqlCeCommand("query", _sqlConnection);

                    if (_sqlConnection.State != ConnectionState.Open) _sqlConnection.Open();
                    foreach (var query in sqlqueries)
                    {
                        cmd.CommandText = query;
                        cmd.ExecuteNonQuery();
                    }
                    _sqlConnection.Close();
                }
            }
            catch (Exception)
            {
            }
        }
        public void Query(string query)
        {
            query = "DROP TABLE TDEF_NON_VIVANT";

            SqlCeCommand cmd = new SqlCeCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = query;
            cmd.Connection = _sqlConnection;

            cmd.ExecuteNonQuery();
        }
        #endregion

        #region Methods protected
        protected void OnStatusChanged()
        {
            if (ConnectionStatusChanged != null) ConnectionStatusChanged();
        }
        protected void OnResultChanged()
        {
            if (ResultChanged != null) ResultChanged();
        }
        #endregion

        #region Methods private
        /// <summary>
        /// Here we have to use SqlCeConnection Class.
        /// </summary>
        private void CurdOperation()
        {
            //SqlCeConnection con = new SqlCeConnection(@"Data Source=C:\Users\Amos\Google_Drive\_Assistant\Assistant\TOBI\DATABASE\DBPARTOBI_DEF.sdf");

            SqlCeCommand cmd = _sqlConnection.CreateCommand();
            cmd.CommandText = "select * from TPARAM_INDEX";
            _sda.SelectCommand = cmd;

            if (_sda != null)
            {
                try
                {
                    SqlCeCommandBuilder cb = new SqlCeCommandBuilder(_sda);
                    _sda.InsertCommand = cb.GetInsertCommand();
                    _sda.UpdateCommand = cb.GetUpdateCommand();
                    _sda.DeleteCommand = cb.GetDeleteCommand();
                }
                catch (Exception)
                {
                }
            }
        }
        /// <summary>
        /// this is common method created for Add,Update,Delete case.
        /// In this method normaly fill dataset with olddata.
        /// </summary>
        /// <param name="oldData"></param>
        /// <param name="dr"></param>
        private void FillDataInDataset(out DataSet oldData, out DataRow dr)
        {
            oldData = new DataSet();
            dr = null;
            _sda.Fill(oldData);
        }
        /// <summary>
        /// In this method get all data from table and 
        /// bind grid with data.
        /// </summary>
        private void BindGrid()
        {
            _ds.Clear();
            _sda.Fill(_ds);

            if (_ds.Tables.Count > 0)
            {
                OnResultChanged();
                // dgvOldData.DataSource = _ds.Tables[0];
            }
        }
        #endregion
    }
}
