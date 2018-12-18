using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Droid.Database
{
    public partial class Demo : Form
    {
        #region Attributes
        private DBConnection _databaseConnection;
        #endregion

        #region Properties
        public DBConnection DatabaseConnection
        {
            get { return _databaseConnection; }
            set { _databaseConnection = value; }
        }
        #endregion

        #region Constructor
        public Demo()
        {
            InitializeComponent();
            Init();
        }
        #endregion

        #region Methods public
        #endregion

        #region Methods private
        private void Init()
        {
            _databaseConnection = new DBConnection();
        }
        #endregion

        #region event
        #endregion
    }
}
