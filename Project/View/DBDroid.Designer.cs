namespace Droid_Database
{
    partial class DBDroid
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DBDroid));
            this.dataGridViewPreview = new System.Windows.Forms.DataGridView();
            this.splitContainerQuery = new System.Windows.Forms.SplitContainer();
            this.buttonSetting = new System.Windows.Forms.Button();
            this.buttonRun = new System.Windows.Forms.Button();
            this.textBoxExcute = new System.Windows.Forms.TextBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelConnection = new System.Windows.Forms.ToolStripStatusLabel();
            this.splitContainerNavigation = new System.Windows.Forms.SplitContainer();
            this.treeViewDataBase = new TreeViewDataBase();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPreview)).BeginInit();
            this.splitContainerQuery.Panel1.SuspendLayout();
            this.splitContainerQuery.Panel2.SuspendLayout();
            this.splitContainerQuery.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.splitContainerNavigation.Panel1.SuspendLayout();
            this.splitContainerNavigation.Panel2.SuspendLayout();
            this.splitContainerNavigation.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridViewPreview
            // 
            this.dataGridViewPreview.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewPreview.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewPreview.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewPreview.Name = "dataGridViewPreview";
            this.dataGridViewPreview.Size = new System.Drawing.Size(633, 301);
            this.dataGridViewPreview.TabIndex = 0;
            // 
            // splitContainerQuery
            // 
            this.splitContainerQuery.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerQuery.Location = new System.Drawing.Point(0, 0);
            this.splitContainerQuery.Name = "splitContainerQuery";
            this.splitContainerQuery.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerQuery.Panel1
            // 
            this.splitContainerQuery.Panel1.Controls.Add(this.buttonSetting);
            this.splitContainerQuery.Panel1.Controls.Add(this.buttonRun);
            this.splitContainerQuery.Panel1.Controls.Add(this.textBoxExcute);
            // 
            // splitContainerQuery.Panel2
            // 
            this.splitContainerQuery.Panel2.Controls.Add(this.dataGridViewPreview);
            this.splitContainerQuery.Panel2.Controls.Add(this.statusStrip1);
            this.splitContainerQuery.Size = new System.Drawing.Size(633, 571);
            this.splitContainerQuery.SplitterDistance = 244;
            this.splitContainerQuery.TabIndex = 1;
            // 
            // buttonSetting
            // 
            this.buttonSetting.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonSetting.BackgroundImage")));
            this.buttonSetting.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSetting.ForeColor = System.Drawing.Color.Transparent;
            this.buttonSetting.Location = new System.Drawing.Point(554, 12);
            this.buttonSetting.Name = "buttonSetting";
            this.buttonSetting.Size = new System.Drawing.Size(34, 34);
            this.buttonSetting.TabIndex = 5;
            this.buttonSetting.UseVisualStyleBackColor = true;
            this.buttonSetting.Click += new System.EventHandler(this.buttonSetting_Click);
            // 
            // buttonRun
            // 
            this.buttonRun.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonRun.BackgroundImage")));
            this.buttonRun.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonRun.ForeColor = System.Drawing.Color.Transparent;
            this.buttonRun.Location = new System.Drawing.Point(554, 52);
            this.buttonRun.Name = "buttonRun";
            this.buttonRun.Size = new System.Drawing.Size(34, 34);
            this.buttonRun.TabIndex = 4;
            this.buttonRun.UseVisualStyleBackColor = true;
            this.buttonRun.Click += new System.EventHandler(this.buttonRun_Click);
            // 
            // textBoxExcute
            // 
            this.textBoxExcute.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxExcute.Location = new System.Drawing.Point(0, 0);
            this.textBoxExcute.Multiline = true;
            this.textBoxExcute.Name = "textBoxExcute";
            this.textBoxExcute.Size = new System.Drawing.Size(633, 244);
            this.textBoxExcute.TabIndex = 3;
            this.textBoxExcute.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxQuery_KeyDown);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelConnection});
            this.statusStrip1.Location = new System.Drawing.Point(0, 301);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(633, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabelConnection
            // 
            this.toolStripStatusLabelConnection.Name = "toolStripStatusLabelConnection";
            this.toolStripStatusLabelConnection.Size = new System.Drawing.Size(86, 17);
            this.toolStripStatusLabelConnection.Text = "No connection";
            // 
            // splitContainerNavigation
            // 
            this.splitContainerNavigation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerNavigation.Location = new System.Drawing.Point(0, 0);
            this.splitContainerNavigation.Name = "splitContainerNavigation";
            // 
            // splitContainerNavigation.Panel1
            // 
            this.splitContainerNavigation.Panel1.Controls.Add(this.treeViewDataBase);
            // 
            // splitContainerNavigation.Panel2
            // 
            this.splitContainerNavigation.Panel2.Controls.Add(this.splitContainerQuery);
            this.splitContainerNavigation.Size = new System.Drawing.Size(794, 571);
            this.splitContainerNavigation.SplitterDistance = 157;
            this.splitContainerNavigation.TabIndex = 2;
            // 
            // treeViewDataBase
            // 
            this.treeViewDataBase.DataBaseConnection = null;
            this.treeViewDataBase.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewDataBase.ImageIndex = 0;
            this.treeViewDataBase.Location = new System.Drawing.Point(0, 0);
            this.treeViewDataBase.Name = "treeViewDataBase";
            this.treeViewDataBase.SelectedImageIndex = 0;
            this.treeViewDataBase.Size = new System.Drawing.Size(157, 571);
            this.treeViewDataBase.TabIndex = 0;
            // 
            // DBDroid
            // 
            this.ClientSize = new System.Drawing.Size(794, 571);
            this.Controls.Add(this.splitContainerNavigation);
            this.Name = "DBDroid";
            this.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Text = "DBDroid";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPreview)).EndInit();
            this.splitContainerQuery.Panel1.ResumeLayout(false);
            this.splitContainerQuery.Panel1.PerformLayout();
            this.splitContainerQuery.Panel2.ResumeLayout(false);
            this.splitContainerQuery.Panel2.PerformLayout();
            this.splitContainerQuery.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.splitContainerNavigation.Panel1.ResumeLayout(false);
            this.splitContainerNavigation.Panel2.ResumeLayout(false);
            this.splitContainerNavigation.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewPreview;
        private System.Windows.Forms.SplitContainer splitContainerQuery;
        private System.Windows.Forms.TextBox textBoxExcute;
        private System.Windows.Forms.Button buttonRun;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelConnection;
        private System.Windows.Forms.SplitContainer splitContainerNavigation;
        private System.Windows.Forms.Button buttonSetting;
        private TreeViewDataBase treeViewDataBase;
    }
}