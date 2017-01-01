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
    public partial class DBSetting : Form
    {
        #region Attribute
        private DBConnection _dbc;
        #endregion

        #region Properties
        #endregion

        #region Constructor
        public DBSetting(DBConnection dbc)
        {
            _dbc = dbc;
            InitializeComponent();
            treeViewDataBase.DataBaseConnection = _dbc;
            treeViewDataBase.NodeMouseDoubleClick += new TreeNodeMouseClickEventHandler(treeViewDataBase_NodeMouseDoubleClick);

            this.ShowDialog();
        }
        #endregion

        #region Methods
        #endregion

        #region Event
        private void treeViewDataBase_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            textBoxDBName.Text = treeViewDataBase.SelectedNode.Text;
        }
        #endregion
    }
}
