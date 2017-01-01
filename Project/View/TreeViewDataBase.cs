using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;

namespace Droid_database
{
    public delegate TreeViewDataBaseEventHander TreeViewDataBaseEventHander(string DBNodeVal);
    public class TreeViewDataBase : TreeView
    {
        #region Attribute
        private ImageList imageList;
        private System.ComponentModel.IContainer components;
        private DBConnection _dbc;
        #endregion

        #region Properties
        public DBConnection DataBaseConnection
        {
            get { return _dbc; }
            set 
            { 
                _dbc = value;
                InitData();
            }
        }
        #endregion

        #region Constructor
        public TreeViewDataBase()
        {
            InitializeComponent();
        }
        #endregion

        #region Methods public
        #endregion

        #region Methods protected
        #endregion

        #region Methods private
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TreeViewDataBase));
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "default");
            this.imageList.Images.SetKeyName(1, "root");
            this.imageList.Images.SetKeyName(2, "database");
            this.imageList.Images.SetKeyName(3, "table");
            // 
            // TreeViewDataBase
            // 
            this.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ImageIndex = 0;
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "treeViewDataBase";
            this.SelectedImageIndex = 0;
            this.TabIndex = 0;
            this.LineColor = System.Drawing.Color.Black;
            this.ResumeLayout(false);
            this.ImageList = imageList;

        }
        private void InitData()
        {
            if (_dbc != null)
            {
                TreeNode tn_root = new TreeNode("Connection", 1, 1);
                tn_root.Expand();
                //TreeNode tnDb;

                //tnDb = new TreeNode("DBPARTOBI_DEF", 2, 2);
                //foreach (DataTable dt in _dbc.DBDataSet.Tables)
                //{
                //    tnDb.Nodes.Add(dt.TableName, dt.TableName, 3);
                //}
                //tn_root.Nodes.Add(tnDb);

                //tnDb = new TreeNode("DBPARTOBI_INS", 2, 2);
                //tn_root.Nodes.Add(tnDb);

                //tnDb = new TreeNode("DBPARTOBI_DIA", 2, 2);
                //tn_root.Nodes.Add(tnDb);
    
                //object obj = _dbc.DBDataSet.Tables;

                this.Nodes.Add(tn_root);
            }
        }
        #endregion
    }
}
