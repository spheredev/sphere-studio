namespace SphereStudio.Forms
{
    partial class IdeWindow
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
            if (disposing && (components != null)) {
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IdeWindow));
            this.EditorTabContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.CloseTabItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CloseAllTabItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveTabItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainDockPanel = new WeifenLuo.WinFormsUI.Docking.DockPanel();
            this.mainToolStrip = new System.Windows.Forms.ToolStrip();
            this.newToolButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.openToolButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.menuOpenProject2 = new System.Windows.Forms.ToolStripMenuItem();
            this.fileOrDocumentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolButton = new System.Windows.Forms.ToolStripButton();
            this.saveAllToolButton = new System.Windows.Forms.ToolStripButton();
            this.ToolSeperator2 = new System.Windows.Forms.ToolStripSeparator();
            this.cutToolButton = new System.Windows.Forms.ToolStripButton();
            this.copyToolButton = new System.Windows.Forms.ToolStripButton();
            this.pasteToolButton = new System.Windows.Forms.ToolStripButton();
            this.ToolSeperator1 = new System.Windows.Forms.ToolStripSeparator();
            this.testGameToolButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.runGameToolButton = new System.Windows.Forms.ToolStripSplitButton();
            this.buildRunToolCommand = new System.Windows.Forms.ToolStripMenuItem();
            this.rebuildRunToolCommand = new System.Windows.Forms.ToolStripMenuItem();
            this.pauseToolButton = new System.Windows.Forms.ToolStripButton();
            this.stopToolButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.engineToolComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.configureEngineToolButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.projectPropertiesToolButton = new System.Windows.Forms.ToolStripButton();
            this.preferencesToolButton = new System.Windows.Forms.ToolStripButton();
            this.mainStatusStrip = new System.Windows.Forms.StatusStrip();
            this.helpLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.SsResizeMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SsRescaleMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SpriteTilesetMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ExportSsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ImportSsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PropertiesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.recenterMapItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TilesetMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ExportTilesetItem = new System.Windows.Forms.ToolStripMenuItem();
            this.UpdateFromMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ResizeMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RescaleMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.newProjectMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openProjectMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeProjectMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openLastProjectMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.newMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuNewProject2 = new System.Windows.Forms.ToolStripMenuItem();
            this.openMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Seperator2 = new System.Windows.Forms.ToolStripSeparator();
            this.saveMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAllMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Seperator3 = new System.Windows.Forms.ToolStripSeparator();
            this.exitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.undoMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.redoMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Seperator4 = new System.Windows.Forms.ToolStripSeparator();
            this.cutMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Seperator5 = new System.Windows.Forms.ToolStripSeparator();
            this.selectAllMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Separator6 = new System.Windows.Forms.ToolStripSeparator();
            this.zoomInMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zoomOutMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.projectMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.exploreProjectMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshProjectMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Separator7 = new System.Windows.Forms.ToolStripSeparator();
            this.projectPropertiesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.closeDocumentMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.startPageMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.mainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.buildMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.buildMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rebuildMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
            this.packageGameMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.runMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.buildRunMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.breakNowMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopDebuggingMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.rebuildRunMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
            this.stepIntoMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stepOverMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stepOutMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.testGameMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.configureEngineMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.preferencesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EditorTabContextMenu.SuspendLayout();
            this.mainToolStrip.SuspendLayout();
            this.mainStatusStrip.SuspendLayout();
            this.mainMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // EditorTabContextMenu
            // 
            this.EditorTabContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CloseTabItem,
            this.CloseAllTabItem,
            this.SaveTabItem});
            this.EditorTabContextMenu.Name = "EditorTabContextMenu";
            this.EditorTabContextMenu.Size = new System.Drawing.Size(166, 70);
            // 
            // CloseTabItem
            // 
            this.CloseTabItem.Image = global::SphereStudio.Properties.Resources.cross;
            this.CloseTabItem.Name = "CloseTabItem";
            this.CloseTabItem.Size = new System.Drawing.Size(165, 22);
            this.CloseTabItem.Text = "Close Tab";
            // 
            // CloseAllTabItem
            // 
            this.CloseAllTabItem.Name = "CloseAllTabItem";
            this.CloseAllTabItem.Size = new System.Drawing.Size(165, 22);
            this.CloseAllTabItem.Text = "Close All But This";
            // 
            // SaveTabItem
            // 
            this.SaveTabItem.Image = global::SphereStudio.Properties.Resources.disk;
            this.SaveTabItem.Name = "SaveTabItem";
            this.SaveTabItem.Size = new System.Drawing.Size(165, 22);
            this.SaveTabItem.Text = "Save Tab";
            // 
            // mainDockPanel
            // 
            this.mainDockPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainDockPanel.DockBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(54)))), ((int)(((byte)(75)))));
            this.mainDockPanel.DockLeftPortion = 0.2D;
            this.mainDockPanel.DockRightPortion = 0.2D;
            this.mainDockPanel.Location = new System.Drawing.Point(0, 52);
            this.mainDockPanel.Name = "mainDockPanel";
            this.mainDockPanel.ShowDocumentIcon = true;
            this.mainDockPanel.Size = new System.Drawing.Size(787, 479);
            this.mainDockPanel.SupportDeeplyNestedContent = true;
            this.mainDockPanel.TabIndex = 0;
            this.mainDockPanel.ActiveDocumentChanged += new System.EventHandler(this.mainDockPanel_ActiveDocumentChanged);
            // 
            // mainToolStrip
            // 
            this.mainToolStrip.AllowItemReorder = true;
            this.mainToolStrip.AutoSize = false;
            this.mainToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.mainToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolButton,
            this.openToolButton,
            this.saveToolButton,
            this.saveAllToolButton,
            this.ToolSeperator2,
            this.cutToolButton,
            this.copyToolButton,
            this.pasteToolButton,
            this.ToolSeperator1,
            this.testGameToolButton,
            this.toolStripSeparator2,
            this.runGameToolButton,
            this.pauseToolButton,
            this.stopToolButton,
            this.toolStripSeparator1,
            this.engineToolComboBox,
            this.configureEngineToolButton,
            this.toolStripSeparator9,
            this.projectPropertiesToolButton,
            this.preferencesToolButton});
            this.mainToolStrip.Location = new System.Drawing.Point(0, 24);
            this.mainToolStrip.Name = "mainToolStrip";
            this.mainToolStrip.Padding = new System.Windows.Forms.Padding(5, 0, 1, 0);
            this.mainToolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.mainToolStrip.Size = new System.Drawing.Size(787, 28);
            this.mainToolStrip.TabIndex = 2;
            this.mainToolStrip.Text = "Tool Strip";
            // 
            // newToolButton
            // 
            this.newToolButton.Image = global::SphereStudio.Properties.Resources.script_edit;
            this.newToolButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.newToolButton.Name = "newToolButton";
            this.newToolButton.Size = new System.Drawing.Size(60, 25);
            this.newToolButton.Text = "&New";
            this.newToolButton.DropDownClosed += new System.EventHandler(this.newMenuItem_DropDownClosed);
            this.newToolButton.DropDownOpening += new System.EventHandler(this.newMenuItem_DropDownOpening);
            // 
            // openToolButton
            // 
            this.openToolButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.openToolButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuOpenProject2,
            this.fileOrDocumentToolStripMenuItem});
            this.openToolButton.Image = global::SphereStudio.Properties.Resources.open;
            this.openToolButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openToolButton.Name = "openToolButton";
            this.openToolButton.Size = new System.Drawing.Size(29, 25);
            this.openToolButton.Text = "&Open";
            // 
            // menuOpenProject2
            // 
            this.menuOpenProject2.Image = global::SphereStudio.Properties.Resources.SphereEditor;
            this.menuOpenProject2.Name = "menuOpenProject2";
            this.menuOpenProject2.Size = new System.Drawing.Size(196, 22);
            this.menuOpenProject2.Text = "Sphere Studio &Project...";
            this.menuOpenProject2.Click += new System.EventHandler(this.openProjectMenuItem_Click);
            // 
            // fileOrDocumentToolStripMenuItem
            // 
            this.fileOrDocumentToolStripMenuItem.Image = global::SphereStudio.Properties.Resources.page_white_edit;
            this.fileOrDocumentToolStripMenuItem.Name = "fileOrDocumentToolStripMenuItem";
            this.fileOrDocumentToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.fileOrDocumentToolStripMenuItem.Text = "&File/Document...";
            this.fileOrDocumentToolStripMenuItem.Click += new System.EventHandler(this.openMenuItem_Click);
            // 
            // saveToolButton
            // 
            this.saveToolButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.saveToolButton.Enabled = false;
            this.saveToolButton.Image = global::SphereStudio.Properties.Resources.disk;
            this.saveToolButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveToolButton.Name = "saveToolButton";
            this.saveToolButton.Size = new System.Drawing.Size(23, 25);
            this.saveToolButton.Text = "&Save";
            this.saveToolButton.Click += new System.EventHandler(this.saveMenuItem_Click);
            // 
            // saveAllToolButton
            // 
            this.saveAllToolButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.saveAllToolButton.Image = global::SphereStudio.Properties.Resources.disk_multiple;
            this.saveAllToolButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveAllToolButton.Name = "saveAllToolButton";
            this.saveAllToolButton.Size = new System.Drawing.Size(23, 25);
            this.saveAllToolButton.Text = "Save All";
            this.saveAllToolButton.Click += new System.EventHandler(this.saveAllMenuItem_Click);
            // 
            // ToolSeperator2
            // 
            this.ToolSeperator2.Name = "ToolSeperator2";
            this.ToolSeperator2.Size = new System.Drawing.Size(6, 28);
            // 
            // cutToolButton
            // 
            this.cutToolButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.cutToolButton.Enabled = false;
            this.cutToolButton.Image = global::SphereStudio.Properties.Resources.cut;
            this.cutToolButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cutToolButton.Name = "cutToolButton";
            this.cutToolButton.Size = new System.Drawing.Size(23, 25);
            this.cutToolButton.Text = "C&ut";
            this.cutToolButton.Click += new System.EventHandler(this.cutMenuItem_Click);
            // 
            // copyToolButton
            // 
            this.copyToolButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.copyToolButton.Enabled = false;
            this.copyToolButton.Image = global::SphereStudio.Properties.Resources.page_copy;
            this.copyToolButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.copyToolButton.Name = "copyToolButton";
            this.copyToolButton.Size = new System.Drawing.Size(23, 25);
            this.copyToolButton.Text = "&Copy";
            this.copyToolButton.Click += new System.EventHandler(this.copyMenuItem_Click);
            // 
            // pasteToolButton
            // 
            this.pasteToolButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.pasteToolButton.Image = global::SphereStudio.Properties.Resources.paste_plain;
            this.pasteToolButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.pasteToolButton.Name = "pasteToolButton";
            this.pasteToolButton.Size = new System.Drawing.Size(23, 25);
            this.pasteToolButton.Text = "&Paste";
            this.pasteToolButton.Click += new System.EventHandler(this.pasteMenuItem_Click);
            // 
            // ToolSeperator1
            // 
            this.ToolSeperator1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ToolSeperator1.Name = "ToolSeperator1";
            this.ToolSeperator1.Size = new System.Drawing.Size(6, 28);
            // 
            // testGameToolButton
            // 
            this.testGameToolButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.testGameToolButton.Enabled = false;
            this.testGameToolButton.Image = global::SphereStudio.Properties.Resources.lightning;
            this.testGameToolButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.testGameToolButton.Name = "testGameToolButton";
            this.testGameToolButton.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.testGameToolButton.Size = new System.Drawing.Size(23, 25);
            this.testGameToolButton.Text = "Test Game";
            this.testGameToolButton.ToolTipText = "Test without Debugger";
            this.testGameToolButton.Click += new System.EventHandler(this.testGameMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 28);
            // 
            // runGameToolButton
            // 
            this.runGameToolButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.buildRunToolCommand,
            this.rebuildRunToolCommand});
            this.runGameToolButton.Image = global::SphereStudio.Properties.Resources.play;
            this.runGameToolButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.runGameToolButton.Name = "runGameToolButton";
            this.runGameToolButton.Size = new System.Drawing.Size(94, 25);
            this.runGameToolButton.Text = "&Run Game";
            this.runGameToolButton.ToolTipText = "Start Debugging";
            this.runGameToolButton.ButtonClick += new System.EventHandler(this.buildRunMenuItem_Click);
            // 
            // buildRunToolCommand
            // 
            this.buildRunToolCommand.Image = global::SphereStudio.Properties.Resources.play;
            this.buildRunToolCommand.Name = "buildRunToolCommand";
            this.buildRunToolCommand.Size = new System.Drawing.Size(187, 22);
            this.buildRunToolCommand.Text = "Build && &Run (Default)";
            this.buildRunToolCommand.Click += new System.EventHandler(this.buildRunMenuItem_Click);
            // 
            // rebuildRunToolCommand
            // 
            this.rebuildRunToolCommand.Name = "rebuildRunToolCommand";
            this.rebuildRunToolCommand.Size = new System.Drawing.Size(187, 22);
            this.rebuildRunToolCommand.Text = "R&ebuild && Run";
            this.rebuildRunToolCommand.Click += new System.EventHandler(this.rebuildRunMenuItem_Click);
            // 
            // pauseToolButton
            // 
            this.pauseToolButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.pauseToolButton.Image = global::SphereStudio.Properties.Resources.pause;
            this.pauseToolButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.pauseToolButton.Name = "pauseToolButton";
            this.pauseToolButton.Size = new System.Drawing.Size(23, 25);
            this.pauseToolButton.Text = "Pause";
            this.pauseToolButton.ToolTipText = "Break into Debugger";
            this.pauseToolButton.Click += new System.EventHandler(this.breakNowMenuItem_Click);
            // 
            // stopToolButton
            // 
            this.stopToolButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.stopToolButton.Image = global::SphereStudio.Properties.Resources.stop;
            this.stopToolButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.stopToolButton.Name = "stopToolButton";
            this.stopToolButton.Size = new System.Drawing.Size(23, 25);
            this.stopToolButton.Text = "Stop";
            this.stopToolButton.ToolTipText = "Stop Debugging";
            this.stopToolButton.Click += new System.EventHandler(this.stopDebuggingMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 28);
            // 
            // engineToolComboBox
            // 
            this.engineToolComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.engineToolComboBox.DropDownWidth = 150;
            this.engineToolComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.engineToolComboBox.Name = "engineToolComboBox";
            this.engineToolComboBox.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.engineToolComboBox.Size = new System.Drawing.Size(160, 28);
            this.engineToolComboBox.SelectedIndexChanged += new System.EventHandler(this.engineToolComboBox_SelectedIndexChanged);
            // 
            // configureEngineToolButton
            // 
            this.configureEngineToolButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.configureEngineToolButton.Enabled = false;
            this.configureEngineToolButton.Image = global::SphereStudio.Properties.Resources.cog;
            this.configureEngineToolButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.configureEngineToolButton.Name = "configureEngineToolButton";
            this.configureEngineToolButton.Size = new System.Drawing.Size(23, 25);
            this.configureEngineToolButton.Text = "Configure Engine";
            this.configureEngineToolButton.Click += new System.EventHandler(this.configureEngineMenuItem_Click);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(6, 28);
            // 
            // projectPropertiesToolButton
            // 
            this.projectPropertiesToolButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.projectPropertiesToolButton.Enabled = false;
            this.projectPropertiesToolButton.Image = global::SphereStudio.Properties.Resources.SphereEditor;
            this.projectPropertiesToolButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.projectPropertiesToolButton.Name = "projectPropertiesToolButton";
            this.projectPropertiesToolButton.Size = new System.Drawing.Size(23, 25);
            this.projectPropertiesToolButton.Text = "Project Properties";
            this.projectPropertiesToolButton.Click += new System.EventHandler(this.projectPropertiesMenuItem_Click);
            // 
            // preferencesToolButton
            // 
            this.preferencesToolButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.preferencesToolButton.Image = global::SphereStudio.Properties.Resources.application_view_list;
            this.preferencesToolButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.preferencesToolButton.Name = "preferencesToolButton";
            this.preferencesToolButton.Size = new System.Drawing.Size(23, 25);
            this.preferencesToolButton.Text = "Preferences";
            this.preferencesToolButton.Click += new System.EventHandler(this.preferencesMenuItem_Click);
            // 
            // mainStatusStrip
            // 
            this.mainStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helpLabel});
            this.mainStatusStrip.Location = new System.Drawing.Point(0, 531);
            this.mainStatusStrip.Name = "mainStatusStrip";
            this.mainStatusStrip.Size = new System.Drawing.Size(787, 22);
            this.mainStatusStrip.TabIndex = 3;
            this.mainStatusStrip.Text = "Status";
            // 
            // helpLabel
            // 
            this.helpLabel.BackColor = System.Drawing.Color.Transparent;
            this.helpLabel.Margin = new System.Windows.Forms.Padding(2, 3, 0, 2);
            this.helpLabel.Name = "helpLabel";
            this.helpLabel.Size = new System.Drawing.Size(192, 17);
            this.helpLabel.Text = "Welcome to the new Sphere Editor!";
            // 
            // SsResizeMenuItem
            // 
            this.SsResizeMenuItem.Name = "SsResizeMenuItem";
            this.SsResizeMenuItem.Size = new System.Drawing.Size(32, 19);
            // 
            // SsRescaleMenuItem
            // 
            this.SsRescaleMenuItem.Name = "SsRescaleMenuItem";
            this.SsRescaleMenuItem.Size = new System.Drawing.Size(32, 19);
            // 
            // SpriteTilesetMenuItem
            // 
            this.SpriteTilesetMenuItem.Name = "SpriteTilesetMenuItem";
            this.SpriteTilesetMenuItem.Size = new System.Drawing.Size(32, 19);
            // 
            // ExportSsMenuItem
            // 
            this.ExportSsMenuItem.Name = "ExportSsMenuItem";
            this.ExportSsMenuItem.Size = new System.Drawing.Size(32, 19);
            // 
            // ImportSsMenuItem
            // 
            this.ImportSsMenuItem.Name = "ImportSsMenuItem";
            this.ImportSsMenuItem.Size = new System.Drawing.Size(32, 19);
            // 
            // PropertiesMenuItem
            // 
            this.PropertiesMenuItem.Name = "PropertiesMenuItem";
            this.PropertiesMenuItem.Size = new System.Drawing.Size(32, 19);
            // 
            // recenterMapItem
            // 
            this.recenterMapItem.Name = "recenterMapItem";
            this.recenterMapItem.Size = new System.Drawing.Size(32, 19);
            // 
            // TilesetMenuItem
            // 
            this.TilesetMenuItem.Name = "TilesetMenuItem";
            this.TilesetMenuItem.Size = new System.Drawing.Size(32, 19);
            // 
            // ExportTilesetItem
            // 
            this.ExportTilesetItem.Name = "ExportTilesetItem";
            this.ExportTilesetItem.Size = new System.Drawing.Size(32, 19);
            // 
            // UpdateFromMenuItem
            // 
            this.UpdateFromMenuItem.Name = "UpdateFromMenuItem";
            this.UpdateFromMenuItem.Size = new System.Drawing.Size(32, 19);
            // 
            // ResizeMenuItem
            // 
            this.ResizeMenuItem.Name = "ResizeMenuItem";
            this.ResizeMenuItem.Size = new System.Drawing.Size(32, 19);
            // 
            // RescaleMenuItem
            // 
            this.RescaleMenuItem.Name = "RescaleMenuItem";
            this.RescaleMenuItem.Size = new System.Drawing.Size(32, 19);
            // 
            // fileMenu
            // 
            this.fileMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newProjectMenuItem,
            this.openProjectMenuItem,
            this.closeProjectMenuItem,
            this.openLastProjectMenuItem,
            this.toolStripSeparator7,
            this.newMenuItem,
            this.openMenuItem,
            this.Seperator2,
            this.saveMenuItem,
            this.saveAsMenuItem,
            this.saveAllMenuItem,
            this.Seperator3,
            this.exitMenuItem});
            this.fileMenu.Name = "fileMenu";
            this.fileMenu.Size = new System.Drawing.Size(37, 20);
            this.fileMenu.Text = "&File";
            this.fileMenu.DropDownClosed += new System.EventHandler(this.topMenu_DropDownClosed);
            this.fileMenu.DropDownOpening += new System.EventHandler(this.menuFile_DropDownOpening);
            // 
            // newProjectMenuItem
            // 
            this.newProjectMenuItem.Image = global::SphereStudio.Properties.Resources.new_item;
            this.newProjectMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.newProjectMenuItem.Name = "newProjectMenuItem";
            this.newProjectMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.newProjectMenuItem.Size = new System.Drawing.Size(195, 22);
            this.newProjectMenuItem.Text = "New &Project...";
            this.newProjectMenuItem.Click += new System.EventHandler(this.newProjectMenuItem_Click);
            // 
            // openProjectMenuItem
            // 
            this.openProjectMenuItem.Image = global::SphereStudio.Properties.Resources.folder;
            this.openProjectMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openProjectMenuItem.Name = "openProjectMenuItem";
            this.openProjectMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openProjectMenuItem.Size = new System.Drawing.Size(195, 22);
            this.openProjectMenuItem.Text = "&Open Project...";
            this.openProjectMenuItem.Click += new System.EventHandler(this.openProjectMenuItem_Click);
            // 
            // closeProjectMenuItem
            // 
            this.closeProjectMenuItem.Enabled = false;
            this.closeProjectMenuItem.Image = global::SphereStudio.Properties.Resources.cross;
            this.closeProjectMenuItem.Name = "closeProjectMenuItem";
            this.closeProjectMenuItem.Size = new System.Drawing.Size(195, 22);
            this.closeProjectMenuItem.Text = "&Close Project";
            this.closeProjectMenuItem.Click += new System.EventHandler(this.closeProjectMenuItem_Click);
            // 
            // openLastProjectMenuItem
            // 
            this.openLastProjectMenuItem.Name = "openLastProjectMenuItem";
            this.openLastProjectMenuItem.Size = new System.Drawing.Size(195, 22);
            this.openLastProjectMenuItem.Text = "Open &Last Project";
            this.openLastProjectMenuItem.Click += new System.EventHandler(this.openLastProjectMenuItem_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(192, 6);
            // 
            // newMenuItem
            // 
            this.newMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuNewProject2});
            this.newMenuItem.Image = global::SphereStudio.Properties.Resources.page_white_edit;
            this.newMenuItem.Name = "newMenuItem";
            this.newMenuItem.Size = new System.Drawing.Size(195, 22);
            this.newMenuItem.Text = "&New";
            this.newMenuItem.DropDownClosed += new System.EventHandler(this.newMenuItem_DropDownClosed);
            this.newMenuItem.DropDownOpening += new System.EventHandler(this.newMenuItem_DropDownOpening);
            // 
            // menuNewProject2
            // 
            this.menuNewProject2.Image = global::SphereStudio.Properties.Resources.SphereEditor;
            this.menuNewProject2.Name = "menuNewProject2";
            this.menuNewProject2.Size = new System.Drawing.Size(196, 22);
            this.menuNewProject2.Text = "Sphere Studio &Project...";
            this.menuNewProject2.Click += new System.EventHandler(this.newProjectMenuItem_Click);
            // 
            // openMenuItem
            // 
            this.openMenuItem.Image = global::SphereStudio.Properties.Resources.open;
            this.openMenuItem.Name = "openMenuItem";
            this.openMenuItem.Size = new System.Drawing.Size(195, 22);
            this.openMenuItem.Text = "&Open...";
            this.openMenuItem.Click += new System.EventHandler(this.openMenuItem_Click);
            // 
            // Seperator2
            // 
            this.Seperator2.Name = "Seperator2";
            this.Seperator2.Size = new System.Drawing.Size(192, 6);
            // 
            // saveMenuItem
            // 
            this.saveMenuItem.Image = global::SphereStudio.Properties.Resources.disk;
            this.saveMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveMenuItem.Name = "saveMenuItem";
            this.saveMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveMenuItem.Size = new System.Drawing.Size(195, 22);
            this.saveMenuItem.Text = "&Save";
            this.saveMenuItem.Click += new System.EventHandler(this.saveMenuItem_Click);
            // 
            // saveAsMenuItem
            // 
            this.saveAsMenuItem.Name = "saveAsMenuItem";
            this.saveAsMenuItem.Size = new System.Drawing.Size(195, 22);
            this.saveAsMenuItem.Text = "Save &As...";
            this.saveAsMenuItem.Click += new System.EventHandler(this.saveAsMenuItem_Click);
            // 
            // saveAllMenuItem
            // 
            this.saveAllMenuItem.Image = global::SphereStudio.Properties.Resources.disk_multiple;
            this.saveAllMenuItem.Name = "saveAllMenuItem";
            this.saveAllMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.S)));
            this.saveAllMenuItem.Size = new System.Drawing.Size(195, 22);
            this.saveAllMenuItem.Text = "Save &All";
            this.saveAllMenuItem.Click += new System.EventHandler(this.saveAllMenuItem_Click);
            // 
            // Seperator3
            // 
            this.Seperator3.Name = "Seperator3";
            this.Seperator3.Size = new System.Drawing.Size(192, 6);
            // 
            // exitMenuItem
            // 
            this.exitMenuItem.Image = global::SphereStudio.Properties.Resources.door_in;
            this.exitMenuItem.Name = "exitMenuItem";
            this.exitMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.exitMenuItem.Size = new System.Drawing.Size(195, 22);
            this.exitMenuItem.Text = "E&xit";
            this.exitMenuItem.Click += new System.EventHandler(this.exitMenuItem_Click);
            // 
            // editMenu
            // 
            this.editMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.undoMenuItem,
            this.redoMenuItem,
            this.Seperator4,
            this.cutMenuItem,
            this.copyMenuItem,
            this.pasteMenuItem,
            this.Seperator5,
            this.selectAllMenuItem,
            this.Separator6,
            this.zoomInMenuItem,
            this.zoomOutMenuItem});
            this.editMenu.Name = "editMenu";
            this.editMenu.Size = new System.Drawing.Size(39, 20);
            this.editMenu.Text = "&Edit";
            this.editMenu.DropDownClosed += new System.EventHandler(this.topMenu_DropDownClosed);
            this.editMenu.DropDownOpening += new System.EventHandler(this.editMenu_DropDownOpening);
            // 
            // undoMenuItem
            // 
            this.undoMenuItem.Image = global::SphereStudio.Properties.Resources.arrow_undo;
            this.undoMenuItem.Name = "undoMenuItem";
            this.undoMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.undoMenuItem.Size = new System.Drawing.Size(196, 22);
            this.undoMenuItem.Text = "&Undo";
            this.undoMenuItem.Click += new System.EventHandler(this.undoMenuItem_Click);
            // 
            // redoMenuItem
            // 
            this.redoMenuItem.Image = global::SphereStudio.Properties.Resources.arrow_redo;
            this.redoMenuItem.Name = "redoMenuItem";
            this.redoMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
            this.redoMenuItem.Size = new System.Drawing.Size(196, 22);
            this.redoMenuItem.Text = "&Redo";
            this.redoMenuItem.Click += new System.EventHandler(this.redoMenuItem_Click);
            // 
            // Seperator4
            // 
            this.Seperator4.Name = "Seperator4";
            this.Seperator4.Size = new System.Drawing.Size(193, 6);
            // 
            // cutMenuItem
            // 
            this.cutMenuItem.Image = global::SphereStudio.Properties.Resources.cut;
            this.cutMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cutMenuItem.Name = "cutMenuItem";
            this.cutMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.cutMenuItem.Size = new System.Drawing.Size(196, 22);
            this.cutMenuItem.Text = "Cu&t";
            this.cutMenuItem.Click += new System.EventHandler(this.cutMenuItem_Click);
            // 
            // copyMenuItem
            // 
            this.copyMenuItem.Image = global::SphereStudio.Properties.Resources.page_copy;
            this.copyMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.copyMenuItem.Name = "copyMenuItem";
            this.copyMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.copyMenuItem.Size = new System.Drawing.Size(196, 22);
            this.copyMenuItem.Text = "&Copy";
            this.copyMenuItem.Click += new System.EventHandler(this.copyMenuItem_Click);
            // 
            // pasteMenuItem
            // 
            this.pasteMenuItem.Image = global::SphereStudio.Properties.Resources.paste_plain;
            this.pasteMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.pasteMenuItem.Name = "pasteMenuItem";
            this.pasteMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.pasteMenuItem.Size = new System.Drawing.Size(196, 22);
            this.pasteMenuItem.Text = "&Paste";
            this.pasteMenuItem.Click += new System.EventHandler(this.pasteMenuItem_Click);
            // 
            // Seperator5
            // 
            this.Seperator5.Name = "Seperator5";
            this.Seperator5.Size = new System.Drawing.Size(193, 6);
            // 
            // selectAllMenuItem
            // 
            this.selectAllMenuItem.Name = "selectAllMenuItem";
            this.selectAllMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.selectAllMenuItem.Size = new System.Drawing.Size(196, 22);
            this.selectAllMenuItem.Text = "Select &All";
            this.selectAllMenuItem.Click += new System.EventHandler(this.selectAllMenuItem_Click);
            // 
            // Separator6
            // 
            this.Separator6.Name = "Separator6";
            this.Separator6.Size = new System.Drawing.Size(193, 6);
            // 
            // zoomInMenuItem
            // 
            this.zoomInMenuItem.Image = global::SphereStudio.Properties.Resources.magnifier_zoom_in;
            this.zoomInMenuItem.Name = "zoomInMenuItem";
            this.zoomInMenuItem.ShortcutKeyDisplayString = "Ctrl+Plus";
            this.zoomInMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Oemplus)));
            this.zoomInMenuItem.Size = new System.Drawing.Size(196, 22);
            this.zoomInMenuItem.Text = "Zoom &In";
            this.zoomInMenuItem.Click += new System.EventHandler(this.zoomInMenuItem_Click);
            // 
            // zoomOutMenuItem
            // 
            this.zoomOutMenuItem.Image = global::SphereStudio.Properties.Resources.magnifier_zoom_out;
            this.zoomOutMenuItem.Name = "zoomOutMenuItem";
            this.zoomOutMenuItem.ShortcutKeyDisplayString = "Ctrl+Minus";
            this.zoomOutMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.OemMinus)));
            this.zoomOutMenuItem.Size = new System.Drawing.Size(196, 22);
            this.zoomOutMenuItem.Text = "Zoom &Out";
            this.zoomOutMenuItem.Click += new System.EventHandler(this.zoomOutMenuItem_Click);
            // 
            // projectMenu
            // 
            this.projectMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exploreProjectMenuItem,
            this.refreshProjectMenuItem,
            this.Separator7,
            this.projectPropertiesMenuItem});
            this.projectMenu.Name = "projectMenu";
            this.projectMenu.Size = new System.Drawing.Size(56, 20);
            this.projectMenu.Text = "&Project";
            this.projectMenu.DropDownClosed += new System.EventHandler(this.topMenu_DropDownClosed);
            this.projectMenu.DropDownOpening += new System.EventHandler(this.topMenu_DropDownOpening);
            // 
            // exploreProjectMenuItem
            // 
            this.exploreProjectMenuItem.Enabled = false;
            this.exploreProjectMenuItem.Image = global::SphereStudio.Properties.Resources.folder_closed;
            this.exploreProjectMenuItem.Name = "exploreProjectMenuItem";
            this.exploreProjectMenuItem.Size = new System.Drawing.Size(214, 22);
            this.exploreProjectMenuItem.Text = "Show in Windows &Explorer";
            this.exploreProjectMenuItem.Click += new System.EventHandler(this.exploreProjectMenuItem_Click);
            // 
            // refreshProjectMenuItem
            // 
            this.refreshProjectMenuItem.Enabled = false;
            this.refreshProjectMenuItem.Image = global::SphereStudio.Properties.Resources.arrow_refresh;
            this.refreshProjectMenuItem.Name = "refreshProjectMenuItem";
            this.refreshProjectMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F5)));
            this.refreshProjectMenuItem.Size = new System.Drawing.Size(214, 22);
            this.refreshProjectMenuItem.Text = "&Refresh File List";
            this.refreshProjectMenuItem.Click += new System.EventHandler(this.refreshProjectMenuItem_Click);
            // 
            // Separator7
            // 
            this.Separator7.Name = "Separator7";
            this.Separator7.Size = new System.Drawing.Size(211, 6);
            // 
            // projectPropertiesMenuItem
            // 
            this.projectPropertiesMenuItem.Enabled = false;
            this.projectPropertiesMenuItem.Image = global::SphereStudio.Properties.Resources.SphereEditor;
            this.projectPropertiesMenuItem.Name = "projectPropertiesMenuItem";
            this.projectPropertiesMenuItem.Size = new System.Drawing.Size(214, 22);
            this.projectPropertiesMenuItem.Text = "Project P&roperties...";
            this.projectPropertiesMenuItem.Click += new System.EventHandler(this.projectPropertiesMenuItem_Click);
            // 
            // viewMenu
            // 
            this.viewMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.closeDocumentMenuItem,
            this.toolStripSeparator3,
            this.startPageMenuItem});
            this.viewMenu.Name = "viewMenu";
            this.viewMenu.Size = new System.Drawing.Size(44, 20);
            this.viewMenu.Text = "&View";
            this.viewMenu.DropDownClosed += new System.EventHandler(this.viewMenu_DropDownClosed);
            this.viewMenu.DropDownOpening += new System.EventHandler(this.viewMenu_DropDownOpening);
            // 
            // closeDocumentMenuItem
            // 
            this.closeDocumentMenuItem.Name = "closeDocumentMenuItem";
            this.closeDocumentMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.W)));
            this.closeDocumentMenuItem.Size = new System.Drawing.Size(213, 22);
            this.closeDocumentMenuItem.Text = "Close Active Pane";
            this.closeDocumentMenuItem.Click += new System.EventHandler(this.closeDocumentMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(210, 6);
            // 
            // startPageMenuItem
            // 
            this.startPageMenuItem.Image = global::SphereStudio.Properties.Resources.SphereEditor;
            this.startPageMenuItem.Name = "startPageMenuItem";
            this.startPageMenuItem.Size = new System.Drawing.Size(213, 22);
            this.startPageMenuItem.Text = "&Start Page";
            this.startPageMenuItem.Click += new System.EventHandler(this.startPageMenuItem_Click);
            // 
            // helpMenu
            // 
            this.helpMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutMenuItem,
            this.toolStripSeparator8});
            this.helpMenu.Name = "helpMenu";
            this.helpMenu.Size = new System.Drawing.Size(44, 20);
            this.helpMenu.Text = "&Help";
            this.helpMenu.DropDownClosed += new System.EventHandler(this.topMenu_DropDownClosed);
            this.helpMenu.DropDownOpening += new System.EventHandler(this.topMenu_DropDownOpening);
            // 
            // aboutMenuItem
            // 
            this.aboutMenuItem.Image = global::SphereStudio.Properties.Resources.information;
            this.aboutMenuItem.Name = "aboutMenuItem";
            this.aboutMenuItem.Size = new System.Drawing.Size(192, 22);
            this.aboutMenuItem.Text = "&About Sphere Studio...";
            this.aboutMenuItem.Click += new System.EventHandler(this.aboutMenuItem_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(189, 6);
            // 
            // mainMenuStrip
            // 
            this.mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileMenu,
            this.editMenu,
            this.viewMenu,
            this.projectMenu,
            this.buildMenu,
            this.runMenu,
            this.settingsMenu,
            this.helpMenu});
            this.mainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.mainMenuStrip.Name = "mainMenuStrip";
            this.mainMenuStrip.Size = new System.Drawing.Size(787, 24);
            this.mainMenuStrip.TabIndex = 1;
            this.mainMenuStrip.Text = "Menu";
            // 
            // buildMenu
            // 
            this.buildMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.buildMenuItem,
            this.rebuildMenuItem,
            this.toolStripSeparator10,
            this.packageGameMenuItem});
            this.buildMenu.Name = "buildMenu";
            this.buildMenu.Size = new System.Drawing.Size(46, 20);
            this.buildMenu.Text = "&Build";
            this.buildMenu.DropDownClosed += new System.EventHandler(this.topMenu_DropDownClosed);
            this.buildMenu.DropDownOpening += new System.EventHandler(this.topMenu_DropDownOpening);
            // 
            // buildMenuItem
            // 
            this.buildMenuItem.Name = "buildMenuItem";
            this.buildMenuItem.Size = new System.Drawing.Size(161, 22);
            this.buildMenuItem.Text = "&Build Project";
            this.buildMenuItem.Click += new System.EventHandler(this.buildMenuItem_Click);
            // 
            // rebuildMenuItem
            // 
            this.rebuildMenuItem.Name = "rebuildMenuItem";
            this.rebuildMenuItem.Size = new System.Drawing.Size(161, 22);
            this.rebuildMenuItem.Text = "&Rebuild Project";
            this.rebuildMenuItem.Click += new System.EventHandler(this.rebuildMenuItem_Click);
            // 
            // toolStripSeparator10
            // 
            this.toolStripSeparator10.Name = "toolStripSeparator10";
            this.toolStripSeparator10.Size = new System.Drawing.Size(158, 6);
            // 
            // packageGameMenuItem
            // 
            this.packageGameMenuItem.Name = "packageGameMenuItem";
            this.packageGameMenuItem.Size = new System.Drawing.Size(161, 22);
            this.packageGameMenuItem.Text = "&Package Game...";
            this.packageGameMenuItem.Click += new System.EventHandler(this.packageGameMenuItem_Click);
            // 
            // runMenu
            // 
            this.runMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.buildRunMenuItem,
            this.breakNowMenuItem,
            this.stopDebuggingMenuItem,
            this.toolStripSeparator4,
            this.rebuildRunMenuItem,
            this.toolStripSeparator11,
            this.stepIntoMenuItem,
            this.stepOverMenuItem,
            this.stepOutMenuItem,
            this.toolStripSeparator6,
            this.testGameMenuItem});
            this.runMenu.Name = "runMenu";
            this.runMenu.Size = new System.Drawing.Size(40, 20);
            this.runMenu.Text = "&Run";
            this.runMenu.DropDownClosed += new System.EventHandler(this.topMenu_DropDownClosed);
            this.runMenu.DropDownOpening += new System.EventHandler(this.topMenu_DropDownOpening);
            // 
            // buildRunMenuItem
            // 
            this.buildRunMenuItem.Image = global::SphereStudio.Properties.Resources.play;
            this.buildRunMenuItem.Name = "buildRunMenuItem";
            this.buildRunMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.buildRunMenuItem.Size = new System.Drawing.Size(239, 22);
            this.buildRunMenuItem.Text = "Build && &Run";
            this.buildRunMenuItem.Click += new System.EventHandler(this.buildRunMenuItem_Click);
            // 
            // breakNowMenuItem
            // 
            this.breakNowMenuItem.Image = global::SphereStudio.Properties.Resources.pause;
            this.breakNowMenuItem.Name = "breakNowMenuItem";
            this.breakNowMenuItem.Size = new System.Drawing.Size(239, 22);
            this.breakNowMenuItem.Text = "&Break into Debugger";
            this.breakNowMenuItem.Click += new System.EventHandler(this.breakNowMenuItem_Click);
            // 
            // stopDebuggingMenuItem
            // 
            this.stopDebuggingMenuItem.Enabled = false;
            this.stopDebuggingMenuItem.Image = global::SphereStudio.Properties.Resources.stop;
            this.stopDebuggingMenuItem.Name = "stopDebuggingMenuItem";
            this.stopDebuggingMenuItem.Size = new System.Drawing.Size(239, 22);
            this.stopDebuggingMenuItem.Text = "S&top Debugging";
            this.stopDebuggingMenuItem.Click += new System.EventHandler(this.stopDebuggingMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(236, 6);
            // 
            // rebuildRunMenuItem
            // 
            this.rebuildRunMenuItem.Name = "rebuildRunMenuItem";
            this.rebuildRunMenuItem.Size = new System.Drawing.Size(239, 22);
            this.rebuildRunMenuItem.Text = "R&ebuild && Run";
            this.rebuildRunMenuItem.Click += new System.EventHandler(this.rebuildRunMenuItem_Click);
            // 
            // toolStripSeparator11
            // 
            this.toolStripSeparator11.Name = "toolStripSeparator11";
            this.toolStripSeparator11.Size = new System.Drawing.Size(236, 6);
            // 
            // stepIntoMenuItem
            // 
            this.stepIntoMenuItem.Enabled = false;
            this.stepIntoMenuItem.Name = "stepIntoMenuItem";
            this.stepIntoMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F11;
            this.stepIntoMenuItem.Size = new System.Drawing.Size(239, 22);
            this.stepIntoMenuItem.Text = "Step &Into";
            this.stepIntoMenuItem.Click += new System.EventHandler(this.stepIntoMenuItem_Click);
            // 
            // stepOverMenuItem
            // 
            this.stepOverMenuItem.Enabled = false;
            this.stepOverMenuItem.Name = "stepOverMenuItem";
            this.stepOverMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F10;
            this.stepOverMenuItem.Size = new System.Drawing.Size(239, 22);
            this.stepOverMenuItem.Text = "&Step Over";
            this.stepOverMenuItem.Click += new System.EventHandler(this.stepOverMenuItem_Click);
            // 
            // stepOutMenuItem
            // 
            this.stepOutMenuItem.Enabled = false;
            this.stepOutMenuItem.Name = "stepOutMenuItem";
            this.stepOutMenuItem.Size = new System.Drawing.Size(239, 22);
            this.stepOutMenuItem.Text = "Step &Out";
            this.stepOutMenuItem.Click += new System.EventHandler(this.stepOutMenuItem_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(236, 6);
            // 
            // testGameMenuItem
            // 
            this.testGameMenuItem.Enabled = false;
            this.testGameMenuItem.Image = global::SphereStudio.Properties.Resources.lightning;
            this.testGameMenuItem.Name = "testGameMenuItem";
            this.testGameMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F5)));
            this.testGameMenuItem.Size = new System.Drawing.Size(239, 22);
            this.testGameMenuItem.Text = "&Test without Debugger";
            this.testGameMenuItem.Click += new System.EventHandler(this.testGameMenuItem_Click);
            // 
            // settingsMenu
            // 
            this.settingsMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.configureEngineMenuItem,
            this.toolStripSeparator5,
            this.preferencesMenuItem});
            this.settingsMenu.Name = "settingsMenu";
            this.settingsMenu.Size = new System.Drawing.Size(61, 20);
            this.settingsMenu.Text = "&Settings";
            this.settingsMenu.DropDownClosed += new System.EventHandler(this.topMenu_DropDownClosed);
            this.settingsMenu.DropDownOpening += new System.EventHandler(this.topMenu_DropDownOpening);
            // 
            // configureEngineMenuItem
            // 
            this.configureEngineMenuItem.Enabled = false;
            this.configureEngineMenuItem.Image = global::SphereStudio.Properties.Resources.cog;
            this.configureEngineMenuItem.Name = "configureEngineMenuItem";
            this.configureEngineMenuItem.Size = new System.Drawing.Size(186, 22);
            this.configureEngineMenuItem.Text = "Configure the &Engine";
            this.configureEngineMenuItem.Click += new System.EventHandler(this.configureEngineMenuItem_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(183, 6);
            // 
            // preferencesMenuItem
            // 
            this.preferencesMenuItem.Image = global::SphereStudio.Properties.Resources.application_view_list;
            this.preferencesMenuItem.Name = "preferencesMenuItem";
            this.preferencesMenuItem.Size = new System.Drawing.Size(186, 22);
            this.preferencesMenuItem.Text = "P&references...";
            this.preferencesMenuItem.Click += new System.EventHandler(this.preferencesMenuItem_Click);
            // 
            // SphereStudioWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(787, 553);
            this.Controls.Add(this.mainDockPanel);
            this.Controls.Add(this.mainToolStrip);
            this.Controls.Add(this.mainStatusStrip);
            this.Controls.Add(this.mainMenuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.KeyPreview = true;
            this.MainMenuStrip = this.mainMenuStrip;
            this.MinimumSize = new System.Drawing.Size(480, 360);
            this.Name = "SphereStudioWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sphere Studio";
            this.EditorTabContextMenu.ResumeLayout(false);
            this.mainToolStrip.ResumeLayout(false);
            this.mainToolStrip.PerformLayout();
            this.mainStatusStrip.ResumeLayout(false);
            this.mainStatusStrip.PerformLayout();
            this.mainMenuStrip.ResumeLayout(false);
            this.mainMenuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip mainStatusStrip;
        private System.Windows.Forms.ToolStrip mainToolStrip;
        private System.Windows.Forms.ToolStripButton saveToolButton;
        private System.Windows.Forms.ToolStripSeparator ToolSeperator1;
        private System.Windows.Forms.ToolStripButton configureEngineToolButton;
        private System.Windows.Forms.ToolStripSeparator ToolSeperator2;
        private System.Windows.Forms.ToolStripButton cutToolButton;
        private System.Windows.Forms.ToolStripButton copyToolButton;
        private System.Windows.Forms.ToolStripButton pasteToolButton;
        private System.Windows.Forms.ToolStripButton projectPropertiesToolButton;
        private System.Windows.Forms.ContextMenuStrip EditorTabContextMenu;
        private System.Windows.Forms.ToolStripMenuItem CloseTabItem;
        private System.Windows.Forms.ToolStripMenuItem CloseAllTabItem;
        private System.Windows.Forms.ToolStripMenuItem SaveTabItem;
        private System.Windows.Forms.ToolStripMenuItem ResizeMenuItem;
        private System.Windows.Forms.ToolStripMenuItem PropertiesMenuItem;
        private System.Windows.Forms.ToolStripMenuItem RescaleMenuItem;
        private System.Windows.Forms.ToolStripMenuItem TilesetMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ExportTilesetItem;
        private System.Windows.Forms.ToolStripMenuItem UpdateFromMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel helpLabel;
        private System.Windows.Forms.ToolStripButton preferencesToolButton;
        private System.Windows.Forms.ToolStripMenuItem SsResizeMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SsRescaleMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SpriteTilesetMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ExportSsMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ImportSsMenuItem;
        private System.Windows.Forms.ToolStripMenuItem recenterMapItem;
        private System.Windows.Forms.ToolStripDropDownButton newToolButton;
        private System.Windows.Forms.ToolStripMenuItem fileMenu;
        private System.Windows.Forms.ToolStripMenuItem newProjectMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openProjectMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeProjectMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openLastProjectMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openMenuItem;
        private System.Windows.Forms.ToolStripSeparator Seperator2;
        private System.Windows.Forms.ToolStripMenuItem saveMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAllMenuItem;
        private System.Windows.Forms.ToolStripSeparator Seperator3;
        private System.Windows.Forms.ToolStripMenuItem exitMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editMenu;
        private System.Windows.Forms.ToolStripMenuItem undoMenuItem;
        private System.Windows.Forms.ToolStripMenuItem redoMenuItem;
        private System.Windows.Forms.ToolStripSeparator Seperator4;
        private System.Windows.Forms.ToolStripMenuItem cutMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteMenuItem;
        private System.Windows.Forms.ToolStripSeparator Seperator5;
        private System.Windows.Forms.ToolStripMenuItem selectAllMenuItem;
        private System.Windows.Forms.ToolStripSeparator Separator6;
        private System.Windows.Forms.ToolStripMenuItem zoomInMenuItem;
        private System.Windows.Forms.ToolStripMenuItem zoomOutMenuItem;
        private System.Windows.Forms.ToolStripMenuItem projectMenu;
        private System.Windows.Forms.ToolStripMenuItem projectPropertiesMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exploreProjectMenuItem;
        private System.Windows.Forms.ToolStripSeparator Separator7;
        private System.Windows.Forms.ToolStripMenuItem refreshProjectMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewMenu;
        private System.Windows.Forms.ToolStripMenuItem closeDocumentMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem startPageMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpMenu;
        private System.Windows.Forms.ToolStripMenuItem aboutMenuItem;
        private System.Windows.Forms.MenuStrip mainMenuStrip;
        private System.Windows.Forms.ToolStripComboBox engineToolComboBox;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem menuNewProject2;
        private System.Windows.Forms.ToolStripMenuItem settingsMenu;
        private System.Windows.Forms.ToolStripMenuItem configureEngineMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem preferencesMenuItem;
        internal WeifenLuo.WinFormsUI.Docking.DockPanel mainDockPanel;
        private System.Windows.Forms.ToolStripMenuItem runMenu;
        private System.Windows.Forms.ToolStripMenuItem testGameMenuItem;
        private System.Windows.Forms.ToolStripMenuItem buildRunMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem stepIntoMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stepOverMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stepOutMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem stopDebuggingMenuItem;
        private System.Windows.Forms.ToolStripMenuItem breakNowMenuItem;
        private System.Windows.Forms.ToolStripButton testGameToolButton;
        private System.Windows.Forms.ToolStripMenuItem buildMenu;
        private System.Windows.Forms.ToolStripMenuItem packageGameMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem newMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripDropDownButton openToolButton;
        private System.Windows.Forms.ToolStripMenuItem menuOpenProject2;
        private System.Windows.Forms.ToolStripMenuItem fileOrDocumentToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton stopToolButton;
        private System.Windows.Forms.ToolStripButton pauseToolButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.ToolStripButton saveAllToolButton;
        private System.Windows.Forms.ToolStripSplitButton runGameToolButton;
        private System.Windows.Forms.ToolStripMenuItem rebuildRunToolCommand;
        private System.Windows.Forms.ToolStripMenuItem buildMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rebuildMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
        private System.Windows.Forms.ToolStripMenuItem rebuildRunMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator11;
        private System.Windows.Forms.ToolStripMenuItem buildRunToolCommand;
    }
}

