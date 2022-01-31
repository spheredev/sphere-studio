namespace SphereStudio.DockPanes
{
    partial class FileListPane
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
            this.fileTreeView = new System.Windows.Forms.TreeView();
            this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.newFolderMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.renameMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyPathMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exploreMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.projectPropertiesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fsWatcher = new SphereStudio.Utility.DeferredFileSystemWatcher();
            this.header = new System.Windows.Forms.Label();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.contextMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fsWatcher)).BeginInit();
            this.SuspendLayout();
            // 
            // fileTreeView
            // 
            this.fileTreeView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fileTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fileTreeView.HideSelection = false;
            this.fileTreeView.HotTracking = true;
            this.fileTreeView.ItemHeight = 19;
            this.fileTreeView.LabelEdit = true;
            this.fileTreeView.Location = new System.Drawing.Point(0, 23);
            this.fileTreeView.Margin = new System.Windows.Forms.Padding(2);
            this.fileTreeView.Name = "fileTreeView";
            this.fileTreeView.Size = new System.Drawing.Size(191, 365);
            this.fileTreeView.TabIndex = 3;
            this.fileTreeView.BeforeLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.fileTreeView_BeforeLabelEdit);
            this.fileTreeView.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.fileTreeView_AfterLabelEdit);
            this.fileTreeView.AfterCollapse += new System.Windows.Forms.TreeViewEventHandler(this.fileTreeView_AfterCollapse);
            this.fileTreeView.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.fileTreeView_AfterExpand);
            this.fileTreeView.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.fileTreeView_NodeMouseClick);
            this.fileTreeView.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.fileTreeView_NodeMouseDoubleClick);
            this.fileTreeView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.fileTreeView_KeyDown);
            this.fileTreeView.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.fileTreeView_KeyPress);
            // 
            // contextMenu
            // 
            this.contextMenu.BackColor = System.Drawing.Color.Lavender;
            this.contextMenu.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newFolderMenuItem,
            this.toolStripSeparator3,
            this.newMenuItem,
            this.importMenuItem,
            this.openMenuItem,
            this.deleteMenuItem,
            this.renameMenuItem,
            this.toolStripSeparator2,
            this.exploreMenuItem,
            this.copyPathMenuItem,
            this.toolStripSeparator1,
            this.projectPropertiesMenuItem});
            this.contextMenu.Name = "ProjectFileContextMenu";
            this.contextMenu.Size = new System.Drawing.Size(199, 242);
            // 
            // newFolderMenuItem
            // 
            this.newFolderMenuItem.Image = global::SphereStudio.Properties.Resources.folder_closed;
            this.newFolderMenuItem.Name = "newFolderMenuItem";
            this.newFolderMenuItem.Size = new System.Drawing.Size(198, 22);
            this.newFolderMenuItem.Text = "New &Folder...";
            this.newFolderMenuItem.Click += new System.EventHandler(this.newFolderMenuItem_Click);
            // 
            // newMenuItem
            // 
            this.newMenuItem.Image = global::SphereStudio.Properties.Resources.page_white_edit;
            this.newMenuItem.Name = "newMenuItem";
            this.newMenuItem.Size = new System.Drawing.Size(198, 22);
            this.newMenuItem.Text = "&New";
            // 
            // importMenuItem
            // 
            this.importMenuItem.Name = "importMenuItem";
            this.importMenuItem.Size = new System.Drawing.Size(198, 22);
            this.importMenuItem.Text = "&Import Files...";
            this.importMenuItem.Click += new System.EventHandler(this.importMenuItem_Click);
            // 
            // openMenuItem
            // 
            this.openMenuItem.Image = global::SphereStudio.Properties.Resources.folder;
            this.openMenuItem.Name = "openMenuItem";
            this.openMenuItem.Size = new System.Drawing.Size(198, 22);
            this.openMenuItem.Text = "&Open";
            this.openMenuItem.Click += new System.EventHandler(this.openMenuItem_Click);
            // 
            // deleteMenuItem
            // 
            this.deleteMenuItem.Image = global::SphereStudio.Properties.Resources.cross;
            this.deleteMenuItem.Name = "deleteMenuItem";
            this.deleteMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.deleteMenuItem.Size = new System.Drawing.Size(198, 22);
            this.deleteMenuItem.Text = "&Delete";
            this.deleteMenuItem.Click += new System.EventHandler(this.deleteMenuItem_Click);
            // 
            // renameMenuItem
            // 
            this.renameMenuItem.Image = global::SphereStudio.Properties.Resources.pencil;
            this.renameMenuItem.Name = "renameMenuItem";
            this.renameMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.renameMenuItem.Size = new System.Drawing.Size(198, 22);
            this.renameMenuItem.Text = "&Rename";
            this.renameMenuItem.Click += new System.EventHandler(this.renameMenuItem_Click);
            // 
            // copyPathMenuItem
            // 
            this.copyPathMenuItem.Image = global::SphereStudio.Properties.Resources.page_copy;
            this.copyPathMenuItem.Name = "copyPathMenuItem";
            this.copyPathMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.copyPathMenuItem.Size = new System.Drawing.Size(198, 22);
            this.copyPathMenuItem.Text = "&Copy Full Path";
            this.copyPathMenuItem.Click += new System.EventHandler(this.copyPathMenuItem_Click);
            // 
            // exploreMenuItem
            // 
            this.exploreMenuItem.Image = global::SphereStudio.Properties.Resources.open;
            this.exploreMenuItem.Name = "exploreMenuItem";
            this.exploreMenuItem.Size = new System.Drawing.Size(198, 22);
            this.exploreMenuItem.Text = "Show in E&xplorer";
            this.exploreMenuItem.Click += new System.EventHandler(this.exploreMenuItem_Click);
            // 
            // projectPropertiesMenuItem
            // 
            this.projectPropertiesMenuItem.Image = global::SphereStudio.Properties.Resources.SphereEditor;
            this.projectPropertiesMenuItem.Name = "projectPropertiesMenuItem";
            this.projectPropertiesMenuItem.Size = new System.Drawing.Size(198, 22);
            this.projectPropertiesMenuItem.Text = "Project &Properties...";
            this.projectPropertiesMenuItem.Click += new System.EventHandler(this.projectPropertiesMenuItem_Click);
            // 
            // fsWatcher
            // 
            this.fsWatcher.Delay = 1000D;
            this.fsWatcher.EnableRaisingEvents = true;
            this.fsWatcher.IncludeSubdirectories = true;
            this.fsWatcher.SynchronizingObject = this;
            this.fsWatcher.Created += new SphereStudio.Utility.BatchEventHandler<System.IO.FileSystemEventArgs>(this.fsWatcher_Created);
            this.fsWatcher.Deleted += new SphereStudio.Utility.BatchEventHandler<System.IO.FileSystemEventArgs>(this.fsWatcher_Deleted);
            this.fsWatcher.Renamed += new SphereStudio.Utility.BatchEventHandler<System.IO.RenamedEventArgs>(this.fsWatcher_Renamed);
            // 
            // header
            // 
            this.header.Dock = System.Windows.Forms.DockStyle.Top;
            this.header.Location = new System.Drawing.Point(0, 0);
            this.header.Name = "header";
            this.header.Size = new System.Drawing.Size(191, 23);
            this.header.TabIndex = 4;
            this.header.Text = "explore files in this project";
            this.header.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(195, 6);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(195, 6);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(195, 6);
            // 
            // FileListPane
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.fileTreeView);
            this.Controls.Add(this.header);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "FileListPane";
            this.Size = new System.Drawing.Size(191, 388);
            this.contextMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fsWatcher)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.TreeView fileTreeView;
        private System.Windows.Forms.ContextMenuStrip contextMenu;
        private System.Windows.Forms.ToolStripMenuItem newMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openMenuItem;
        private System.Windows.Forms.ToolStripMenuItem renameMenuItem;
        private System.Windows.Forms.ToolStripMenuItem projectPropertiesMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newFolderMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyPathMenuItem;
        private SphereStudio.Utility.DeferredFileSystemWatcher fsWatcher;
        private System.Windows.Forms.ToolStripMenuItem exploreMenuItem;
        private System.Windows.Forms.Label header;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    }
}
