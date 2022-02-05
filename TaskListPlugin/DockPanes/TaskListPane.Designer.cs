namespace SphereStudio.UI
{
    partial class TaskListPane
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
            this.increasePriorityMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.decreasePriorityMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteTaskMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Seperator2 = new System.Windows.Forms.ToolStripSeparator();
            this.setCategoryMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setPriorityMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Seperator1 = new System.Windows.Forms.ToolStripSeparator();
            this.addTaskMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.pruneTasksMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteAllTasksMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.addTaskToolButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.increasePriorityToolButton = new System.Windows.Forms.ToolStripButton();
            this.decreasePriorityToolButton = new System.Windows.Forms.ToolStripButton();
            this.Seperator0 = new System.Windows.Forms.ToolStripSeparator();
            this.removeTaskToolButton = new System.Windows.Forms.ToolStripButton();
            this.taskListView = new BrightIdeasSoftware.ObjectListView();
            this.olvColumn1 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn2 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn3 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.contextMenu.SuspendLayout();
            this.toolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.taskListView)).BeginInit();
            this.SuspendLayout();
            // 
            // contextMenu
            // 
            this.contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.increasePriorityMenuItem,
            this.decreasePriorityMenuItem,
            this.deleteTaskMenuItem,
            this.Seperator2,
            this.setCategoryMenuItem,
            this.setPriorityMenuItem,
            this.Seperator1,
            this.addTaskMenuItem,
            this.toolStripSeparator1,
            this.pruneTasksMenuItem,
            this.deleteAllTasksMenuItem});
            this.contextMenu.Name = "TaskListMenuStrip";
            this.contextMenu.Size = new System.Drawing.Size(198, 198);
            // 
            // increasePriorityMenuItem
            // 
            this.increasePriorityMenuItem.Image = global::SphereStudio.Properties.Resources.resultset_up;
            this.increasePriorityMenuItem.Name = "increasePriorityMenuItem";
            this.increasePriorityMenuItem.Size = new System.Drawing.Size(197, 22);
            this.increasePriorityMenuItem.Text = "&Increase Priority";
            this.increasePriorityMenuItem.Click += new System.EventHandler(this.increasePriorityMenuItem_Click);
            // 
            // decreasePriorityMenuItem
            // 
            this.decreasePriorityMenuItem.Image = global::SphereStudio.Properties.Resources.resultset_down;
            this.decreasePriorityMenuItem.Name = "decreasePriorityMenuItem";
            this.decreasePriorityMenuItem.Size = new System.Drawing.Size(197, 22);
            this.decreasePriorityMenuItem.Text = "&Decrease Priority";
            this.decreasePriorityMenuItem.Click += new System.EventHandler(this.decreasePriorityMenuItem_Click);
            // 
            // deleteTaskMenuItem
            // 
            this.deleteTaskMenuItem.Image = global::SphereStudio.Properties.Resources.lightbulb_delete;
            this.deleteTaskMenuItem.Name = "deleteTaskMenuItem";
            this.deleteTaskMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.deleteTaskMenuItem.Size = new System.Drawing.Size(197, 22);
            this.deleteTaskMenuItem.Text = "De&lete Task";
            this.deleteTaskMenuItem.Click += new System.EventHandler(this.deleteTaskMenuItem_Click);
            // 
            // Seperator2
            // 
            this.Seperator2.Name = "Seperator2";
            this.Seperator2.Size = new System.Drawing.Size(194, 6);
            // 
            // setCategoryMenuItem
            // 
            this.setCategoryMenuItem.Image = global::SphereStudio.Properties.Resources.information;
            this.setCategoryMenuItem.Name = "setCategoryMenuItem";
            this.setCategoryMenuItem.Size = new System.Drawing.Size(197, 22);
            this.setCategoryMenuItem.Text = "Set &Category";
            // 
            // setPriorityMenuItem
            // 
            this.setPriorityMenuItem.Image = global::SphereStudio.Properties.Resources.resultset_none;
            this.setPriorityMenuItem.Name = "setPriorityMenuItem";
            this.setPriorityMenuItem.Size = new System.Drawing.Size(197, 22);
            this.setPriorityMenuItem.Text = "Set P&riority";
            // 
            // Seperator1
            // 
            this.Seperator1.Name = "Seperator1";
            this.Seperator1.Size = new System.Drawing.Size(194, 6);
            // 
            // addTaskMenuItem
            // 
            this.addTaskMenuItem.Image = global::SphereStudio.Properties.Resources.lightbulb_add;
            this.addTaskMenuItem.Name = "addTaskMenuItem";
            this.addTaskMenuItem.Size = new System.Drawing.Size(197, 22);
            this.addTaskMenuItem.Text = "&Add New Task";
            this.addTaskMenuItem.Click += new System.EventHandler(this.addTaskMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(194, 6);
            // 
            // pruneTasksMenuItem
            // 
            this.pruneTasksMenuItem.Name = "pruneTasksMenuItem";
            this.pruneTasksMenuItem.Size = new System.Drawing.Size(197, 22);
            this.pruneTasksMenuItem.Text = "&Prune Completed Tasks";
            this.pruneTasksMenuItem.Click += new System.EventHandler(this.pruneTasksMenuItem_Click);
            // 
            // deleteAllTasksMenuItem
            // 
            this.deleteAllTasksMenuItem.Image = global::SphereStudio.Properties.Resources.cross;
            this.deleteAllTasksMenuItem.Name = "deleteAllTasksMenuItem";
            this.deleteAllTasksMenuItem.Size = new System.Drawing.Size(197, 22);
            this.deleteAllTasksMenuItem.Text = "Delete All Task&s";
            this.deleteAllTasksMenuItem.Click += new System.EventHandler(this.deleteAllTasksMenuItem_Click);
            // 
            // toolStrip
            // 
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addTaskToolButton,
            this.toolStripSeparator2,
            this.increasePriorityToolButton,
            this.decreasePriorityToolButton,
            this.Seperator0,
            this.removeTaskToolButton});
            this.toolStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(358, 23);
            this.toolStrip.TabIndex = 3;
            this.toolStrip.Text = "toolStrip1";
            // 
            // addTaskToolButton
            // 
            this.addTaskToolButton.Image = global::SphereStudio.Properties.Resources.lightbulb_add;
            this.addTaskToolButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.addTaskToolButton.Name = "addTaskToolButton";
            this.addTaskToolButton.Size = new System.Drawing.Size(76, 20);
            this.addTaskToolButton.Text = "New Task";
            this.addTaskToolButton.Click += new System.EventHandler(this.addTaskMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 23);
            // 
            // increasePriorityToolButton
            // 
            this.increasePriorityToolButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.increasePriorityToolButton.Image = global::SphereStudio.Properties.Resources.resultset_up;
            this.increasePriorityToolButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.increasePriorityToolButton.Name = "increasePriorityToolButton";
            this.increasePriorityToolButton.Size = new System.Drawing.Size(23, 20);
            this.increasePriorityToolButton.Text = "Increase Priority";
            this.increasePriorityToolButton.Click += new System.EventHandler(this.increasePriorityMenuItem_Click);
            // 
            // decreasePriorityToolButton
            // 
            this.decreasePriorityToolButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.decreasePriorityToolButton.Image = global::SphereStudio.Properties.Resources.resultset_down;
            this.decreasePriorityToolButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.decreasePriorityToolButton.Name = "decreasePriorityToolButton";
            this.decreasePriorityToolButton.Size = new System.Drawing.Size(23, 20);
            this.decreasePriorityToolButton.Text = "Decrease Priority";
            this.decreasePriorityToolButton.Click += new System.EventHandler(this.decreasePriorityMenuItem_Click);
            // 
            // Seperator0
            // 
            this.Seperator0.Name = "Seperator0";
            this.Seperator0.Size = new System.Drawing.Size(6, 23);
            // 
            // removeTaskToolButton
            // 
            this.removeTaskToolButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.removeTaskToolButton.Image = global::SphereStudio.Properties.Resources.lightbulb_delete;
            this.removeTaskToolButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.removeTaskToolButton.Name = "removeTaskToolButton";
            this.removeTaskToolButton.Size = new System.Drawing.Size(23, 20);
            this.removeTaskToolButton.Text = "Delete";
            this.removeTaskToolButton.Click += new System.EventHandler(this.deleteTaskMenuItem_Click);
            // 
            // taskListView
            // 
            this.taskListView.AllColumns.Add(this.olvColumn1);
            this.taskListView.AllColumns.Add(this.olvColumn2);
            this.taskListView.AllColumns.Add(this.olvColumn3);
            this.taskListView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.taskListView.CellEditActivation = BrightIdeasSoftware.ObjectListView.CellEditActivateMode.DoubleClick;
            this.taskListView.CellEditUseWholeCell = false;
            this.taskListView.CheckBoxes = true;
            this.taskListView.CheckedAspectName = "Finished";
            this.taskListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn1,
            this.olvColumn2,
            this.olvColumn3});
            this.taskListView.ContextMenuStrip = this.contextMenu;
            this.taskListView.Cursor = System.Windows.Forms.Cursors.Default;
            this.taskListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.taskListView.EmptyListMsg = "No Tasks";
            this.taskListView.FullRowSelect = true;
            this.taskListView.GroupWithItemCountFormat = "";
            this.taskListView.HasCollapsibleGroups = false;
            this.taskListView.HideSelection = false;
            this.taskListView.Location = new System.Drawing.Point(0, 23);
            this.taskListView.Name = "taskListView";
            this.taskListView.Size = new System.Drawing.Size(358, 301);
            this.taskListView.TabIndex = 4;
            this.taskListView.UseAlternatingBackColors = true;
            this.taskListView.UseCellFormatEvents = true;
            this.taskListView.UseCompatibleStateImageBehavior = false;
            this.taskListView.View = System.Windows.Forms.View.Details;
            this.taskListView.FormatCell += new System.EventHandler<BrightIdeasSoftware.FormatCellEventArgs>(this.taskListView_FormatCell);
            this.taskListView.FormatRow += new System.EventHandler<BrightIdeasSoftware.FormatRowEventArgs>(this.taskListView_FormatRow);
            this.taskListView.SelectionChanged += new System.EventHandler(this.taskListView_SelectionChanged);
            // 
            // olvColumn1
            // 
            this.olvColumn1.AspectName = "Name";
            this.olvColumn1.Text = "Name";
            this.olvColumn1.UseInitialLetterForGroup = true;
            this.olvColumn1.Width = 200;
            // 
            // olvColumn2
            // 
            this.olvColumn2.AspectName = "Priority";
            this.olvColumn2.Text = "Priority";
            // 
            // olvColumn3
            // 
            this.olvColumn3.AspectName = "Category";
            this.olvColumn3.Text = "Category";
            this.olvColumn3.Width = 90;
            // 
            // TaskListPane
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.taskListView);
            this.Controls.Add(this.toolStrip);
            this.Name = "TaskListPane";
            this.Size = new System.Drawing.Size(358, 324);
            this.contextMenu.ResumeLayout(false);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.taskListView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenu;
        private System.Windows.Forms.ToolStripMenuItem addTaskMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteTaskMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pruneTasksMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteAllTasksMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton increasePriorityToolButton;
        private System.Windows.Forms.ToolStripButton decreasePriorityToolButton;
        private System.Windows.Forms.ToolStripSeparator Seperator0;
        private System.Windows.Forms.ToolStripButton removeTaskToolButton;
        private System.Windows.Forms.ToolStripButton addTaskToolButton;
        private System.Windows.Forms.ToolStripSeparator Seperator1;
        private System.Windows.Forms.ToolStripSeparator Seperator2;
        private System.Windows.Forms.ToolStripMenuItem setCategoryMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setPriorityMenuItem;
        private BrightIdeasSoftware.ObjectListView taskListView;
        private BrightIdeasSoftware.OLVColumn olvColumn1;
        private BrightIdeasSoftware.OLVColumn olvColumn2;
        private BrightIdeasSoftware.OLVColumn olvColumn3;
        private System.Windows.Forms.ToolStripMenuItem increasePriorityMenuItem;
        private System.Windows.Forms.ToolStripMenuItem decreasePriorityMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    }
}
