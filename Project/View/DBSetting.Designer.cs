namespace Droid_database
{
    partial class DBSetting
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.labelDBName = new System.Windows.Forms.Label();
            this.labelConnString = new System.Windows.Forms.Label();
            this.textBoxDBName = new System.Windows.Forms.TextBox();
            this.textBoxConnString = new System.Windows.Forms.TextBox();
            this.buttonTestConnection = new System.Windows.Forms.Button();
            this.treeViewDataBase = new TreeViewDataBase();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeViewDataBase);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.Color.White;
            this.splitContainer1.Panel2.Controls.Add(this.buttonTestConnection);
            this.splitContainer1.Panel2.Controls.Add(this.textBoxConnString);
            this.splitContainer1.Panel2.Controls.Add(this.textBoxDBName);
            this.splitContainer1.Panel2.Controls.Add(this.labelConnString);
            this.splitContainer1.Panel2.Controls.Add(this.labelDBName);
            this.splitContainer1.Size = new System.Drawing.Size(751, 127);
            this.splitContainer1.SplitterDistance = 286;
            this.splitContainer1.TabIndex = 0;
            // 
            // labelDBName
            // 
            this.labelDBName.AutoSize = true;
            this.labelDBName.Location = new System.Drawing.Point(3, 15);
            this.labelDBName.Name = "labelDBName";
            this.labelDBName.Size = new System.Drawing.Size(85, 13);
            this.labelDBName.TabIndex = 0;
            this.labelDBName.Text = "DataBase Name";
            // 
            // labelConnString
            // 
            this.labelConnString.AutoSize = true;
            this.labelConnString.Location = new System.Drawing.Point(3, 41);
            this.labelConnString.Name = "labelConnString";
            this.labelConnString.Size = new System.Drawing.Size(89, 13);
            this.labelConnString.TabIndex = 1;
            this.labelConnString.Text = "Connection string";
            // 
            // textBoxDBName
            // 
            this.textBoxDBName.Location = new System.Drawing.Point(98, 12);
            this.textBoxDBName.Name = "textBoxDBName";
            this.textBoxDBName.Size = new System.Drawing.Size(343, 20);
            this.textBoxDBName.TabIndex = 2;
            // 
            // textBoxConnString
            // 
            this.textBoxConnString.Location = new System.Drawing.Point(98, 38);
            this.textBoxConnString.Name = "textBoxConnString";
            this.textBoxConnString.Size = new System.Drawing.Size(343, 20);
            this.textBoxConnString.TabIndex = 3;
            // 
            // buttonTestConnection
            // 
            this.buttonTestConnection.Enabled = false;
            this.buttonTestConnection.Location = new System.Drawing.Point(334, 92);
            this.buttonTestConnection.Name = "buttonTestConnection";
            this.buttonTestConnection.Size = new System.Drawing.Size(107, 23);
            this.buttonTestConnection.TabIndex = 4;
            this.buttonTestConnection.Text = "Test connection";
            this.buttonTestConnection.UseVisualStyleBackColor = true;
            // 
            // treeViewDataBase
            // 
            this.treeViewDataBase.DataBaseConnection = null;
            this.treeViewDataBase.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewDataBase.ImageIndex = 0;
            this.treeViewDataBase.Location = new System.Drawing.Point(0, 0);
            this.treeViewDataBase.Name = "treeViewDataBase";
            this.treeViewDataBase.SelectedImageIndex = 0;
            this.treeViewDataBase.Size = new System.Drawing.Size(286, 127);
            this.treeViewDataBase.TabIndex = 0;
            // 
            // DBSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(751, 127);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "DBSetting";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "DBSetting";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private TreeViewDataBase treeViewDataBase;
        private System.Windows.Forms.Label labelConnString;
        private System.Windows.Forms.Label labelDBName;
        private System.Windows.Forms.TextBox textBoxDBName;
        private System.Windows.Forms.TextBox textBoxConnString;
        private System.Windows.Forms.Button buttonTestConnection;
    }
}