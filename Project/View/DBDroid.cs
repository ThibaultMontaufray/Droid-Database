using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Droid_database
{
    public partial class DBDroid : Panel
    {
        #region Attribute
        private DBConnection _dbc;
        #endregion

        #region Properties
        public DBConnection DroidDBConnection
        {
            get { return _dbc; }
            set
            {
                _dbc = value;
                upadteConnection();
            }
        }
        #endregion

        #region Constructor
        public DBDroid()
        {
            InitializeComponent();
        }

        public DBDroid(DBConnection dbc)
        {
            InitializeComponent();
            
            _dbc = dbc;
            upadteConnection();
        }
        #endregion

        #region Methods private
        private void LaunchQuery()
        {
            //_dbc.Add();
            //_dbc.Query("SELECT * FROM TDEF_VEGETAL");
        }
        private void upadteConnection()
        {
            if (_dbc != null)
            {
                _dbc.ConnectionStatusChanged += new DBConnectionEventHandler(_dbc_ConnectionStatusChanged);
                _dbc.ResultChanged += new DBConnectionEventHandler(_dbc_ResultChanged);
                _dbc.Connect();
                treeViewDataBase.DataBaseConnection = _dbc;
                this.Refresh();
            }
            // TODO : refresh of the treeview
            // TODO : position of the buttons
        }
        #endregion 

        #region Event
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            try
            {
                //Add();
                //Reset();
                //this.BindGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                //if (string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtAge.Text) || string.IsNullOrEmpty(txtQualification.Text)) { MessageBox.Show("Please Input data."); return; }
                //Update(lblId.Text);
                //Reset();
                //this.BindGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void buttonDelete_Click(object sender, EventArgs e)
        {
            try
            {
                //if (string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtAge.Text) || string.IsNullOrEmpty(txtQualification.Text)) { MessageBox.Show("Please Input data."); return; }
                //Delete(lblId.Text);
                //Reset();
                //this.BindGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void buttonRun_Click(object sender, EventArgs e)
        {
            LaunchQuery();
        }
        private DBConnectionEventHandler _dbc_ResultChanged()
        {
            //dataGridViewPreview.DataSource = _dbc.DBDataSet.Tables[0];
            return null;
        }
        private DBConnectionEventHandler _dbc_ConnectionStatusChanged()
        {
            toolStripStatusLabelConnection.Text = _dbc.Status;
            return null;
        }
        private void textBoxQuery_KeyDown(object sender, KeyEventArgs e)
        {
            //LaunchQuery();
        }
        private void buttonSetting_Click(object sender, EventArgs e)
        {
            DBSetting dbs = new DBSetting(_dbc);
        }
        #endregion
    }
}
