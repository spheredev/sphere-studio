namespace SphereStudio.DocumentViews
{
    partial class StartPageView
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.testGameMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exploreMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setIconMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.viewMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tilesViewMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listViewMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.smallIconsViewMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.largeIconsViewMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.detailsViewMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.refreshMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.header = new System.Windows.Forms.Label();
            this.projectListView = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenu
            // 
            this.contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.testGameMenuItem,
            this.openMenuItem,
            this.exploreMenuItem,
            this.setIconMenuItem,
            this.toolStripSeparator2,
            this.viewMenuItem,
            this.toolStripSeparator1,
            this.refreshMenuItem});
            this.contextMenu.Name = "ItemContextStrip";
            this.contextMenu.Size = new System.Drawing.Size(163, 148);
            this.contextMenu.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenu_Opening);
            // 
            // testGameMenuItem
            // 
            this.testGameMenuItem.Image = global::SphereStudio.Properties.Resources.lightning;
            this.testGameMenuItem.Name = "testGameMenuItem";
            this.testGameMenuItem.Size = new System.Drawing.Size(162, 22);
            this.testGameMenuItem.Text = "&Test Game";
            this.testGameMenuItem.Click += new System.EventHandler(this.testGameMenuItem_Click);
            // 
            // openMenuItem
            // 
            this.openMenuItem.Image = global::SphereStudio.Properties.Resources.script_edit;
            this.openMenuItem.Name = "openMenuItem";
            this.openMenuItem.Size = new System.Drawing.Size(162, 22);
            this.openMenuItem.Text = "&Open Project";
            this.openMenuItem.Click += new System.EventHandler(this.openMenuItem_Click);
            // 
            // exploreMenuItem
            // 
            this.exploreMenuItem.Image = global::SphereStudio.Properties.Resources.folder;
            this.exploreMenuItem.Name = "exploreMenuItem";
            this.exploreMenuItem.Size = new System.Drawing.Size(162, 22);
            this.exploreMenuItem.Text = "Show in Explorer";
            this.exploreMenuItem.Click += new System.EventHandler(this.exploreMenuItem_Click);
            // 
            // setIconMenuItem
            // 
            this.setIconMenuItem.Image = global::SphereStudio.Properties.Resources.palette;
            this.setIconMenuItem.Name = "setIconMenuItem";
            this.setIconMenuItem.Size = new System.Drawing.Size(162, 22);
            this.setIconMenuItem.Text = "&Set Icon...";
            this.setIconMenuItem.Click += new System.EventHandler(this.setIconMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(159, 6);
            // 
            // viewMenuItem
            // 
            this.viewMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tilesViewMenuItem,
            this.listViewMenuItem,
            this.smallIconsViewMenuItem,
            this.largeIconsViewMenuItem,
            this.detailsViewMenuItem});
            this.viewMenuItem.Name = "viewMenuItem";
            this.viewMenuItem.Size = new System.Drawing.Size(162, 22);
            this.viewMenuItem.Text = "&View";
            // 
            // tilesViewMenuItem
            // 
            this.tilesViewMenuItem.Name = "tilesViewMenuItem";
            this.tilesViewMenuItem.Size = new System.Drawing.Size(180, 22);
            this.tilesViewMenuItem.Text = "&Tiles";
            this.tilesViewMenuItem.Click += new System.EventHandler(this.tilesViewMenuItem_Click);
            // 
            // listViewMenuItem
            // 
            this.listViewMenuItem.Name = "listViewMenuItem";
            this.listViewMenuItem.Size = new System.Drawing.Size(180, 22);
            this.listViewMenuItem.Text = "&List";
            this.listViewMenuItem.Click += new System.EventHandler(this.listViewMenuItem_Click);
            // 
            // smallIconsViewMenuItem
            // 
            this.smallIconsViewMenuItem.Name = "smallIconsViewMenuItem";
            this.smallIconsViewMenuItem.Size = new System.Drawing.Size(180, 22);
            this.smallIconsViewMenuItem.Text = "&Small Icons";
            this.smallIconsViewMenuItem.Click += new System.EventHandler(this.smallIconsViewMenuItem_Click);
            // 
            // largeIconsViewMenuItem
            // 
            this.largeIconsViewMenuItem.Name = "largeIconsViewMenuItem";
            this.largeIconsViewMenuItem.Size = new System.Drawing.Size(180, 22);
            this.largeIconsViewMenuItem.Text = "&Large Icons";
            this.largeIconsViewMenuItem.Click += new System.EventHandler(this.largeIconsViewMenuItem_Click);
            // 
            // detailsViewMenuItem
            // 
            this.detailsViewMenuItem.Name = "detailsViewMenuItem";
            this.detailsViewMenuItem.Size = new System.Drawing.Size(180, 22);
            this.detailsViewMenuItem.Text = "&Details";
            this.detailsViewMenuItem.Click += new System.EventHandler(this.detailsViewMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(159, 6);
            // 
            // refreshMenuItem
            // 
            this.refreshMenuItem.Image = global::SphereStudio.Properties.Resources.arrow_refresh;
            this.refreshMenuItem.Name = "refreshMenuItem";
            this.refreshMenuItem.Size = new System.Drawing.Size(162, 22);
            this.refreshMenuItem.Text = "Re&fresh";
            this.refreshMenuItem.Click += new System.EventHandler(this.refreshMenuItem_Click);
            // 
            // header
            // 
            this.header.Dock = System.Windows.Forms.DockStyle.Top;
            this.header.Location = new System.Drawing.Point(0, 0);
            this.header.Name = "header";
            this.header.Size = new System.Drawing.Size(568, 23);
            this.header.TabIndex = 12;
            this.header.Text = "welcome to the Sphere Studio integrated development environment";
            this.header.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // projectListView
            // 
            this.projectListView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.projectListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader4,
            this.columnHeader2,
            this.columnHeader3});
            this.projectListView.ContextMenuStrip = this.contextMenu;
            this.projectListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.projectListView.FullRowSelect = true;
            this.projectListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.projectListView.HideSelection = false;
            this.projectListView.Location = new System.Drawing.Point(0, 23);
            this.projectListView.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.projectListView.MultiSelect = false;
            this.projectListView.Name = "projectListView";
            this.projectListView.ShowItemToolTips = true;
            this.projectListView.Size = new System.Drawing.Size(568, 371);
            this.projectListView.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.projectListView.TabIndex = 11;
            this.projectListView.TileSize = new System.Drawing.Size(256, 48);
            this.projectListView.UseCompatibleStateImageBehavior = false;
            this.projectListView.ItemActivate += new System.EventHandler(this.projectListView_ItemActivate);
            this.projectListView.MouseClick += new System.Windows.Forms.MouseEventHandler(this.projectListView_MouseClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Name";
            this.columnHeader1.Width = 250;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Type";
            this.columnHeader4.Width = 125;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Author";
            this.columnHeader2.Width = 150;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Path";
            this.columnHeader3.Width = 600;
            // 
            // StartPageView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.projectListView);
            this.Controls.Add(this.header);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "StartPageView";
            this.Size = new System.Drawing.Size(568, 394);
            this.contextMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ContextMenuStrip contextMenu;
        private System.Windows.Forms.ToolStripMenuItem testGameMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setIconMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewMenuItem;
        private System.Windows.Forms.ToolStripMenuItem listViewMenuItem;
        private System.Windows.Forms.ToolStripMenuItem smallIconsViewMenuItem;
        private System.Windows.Forms.ToolStripMenuItem largeIconsViewMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tilesViewMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exploreMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem refreshMenuItem;
        private System.Windows.Forms.ToolStripMenuItem detailsViewMenuItem;
        private System.Windows.Forms.Label header;
        private System.Windows.Forms.ListView projectListView;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    }
}
