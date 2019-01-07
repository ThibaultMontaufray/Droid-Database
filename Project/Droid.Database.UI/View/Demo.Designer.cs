namespace Droid.Database.UI
{
    partial class Demo
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dbManager1 = new Droid.Database.UI.DBManager();
            this.SuspendLayout();
            // 
            // dbManager1
            // 
            this.dbManager1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dbManager1.Location = new System.Drawing.Point(0, 0);
            this.dbManager1.ManagerDBConnection = null;
            this.dbManager1.Name = "dbManager1";
            this.dbManager1.Size = new System.Drawing.Size(853, 481);
            this.dbManager1.TabIndex = 0;
            this.dbManager1.Text = "DBManager";
            // 
            // Demo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(853, 481);
            this.Controls.Add(this.dbManager1);
            this.Name = "Demo";
            this.Text = "Demo";
            this.ResumeLayout(false);

        }

        #endregion

        private DBManager dbManager1;
    }
}