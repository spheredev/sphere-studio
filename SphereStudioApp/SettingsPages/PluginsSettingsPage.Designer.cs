namespace SphereStudio.SettingsPages
{
    partial class PluginsSettingsPage
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
            this.pluginsPanel = new System.Windows.Forms.Panel();
            this.pluginsHeading = new System.Windows.Forms.Label();
            this.pluginsListView = new System.Windows.Forms.ListView();
            this.NameColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.VersionColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.AuthorColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.defaultsPanel = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.defaultsHeading = new System.Windows.Forms.Label();
            this.imageDropDown = new System.Windows.Forms.ComboBox();
            this.otherComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.scriptComboBox = new System.Windows.Forms.ComboBox();
            this.typeComboBox = new System.Windows.Forms.ComboBox();
            this.engineComboBox = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.deletePresetButton = new System.Windows.Forms.Button();
            this.presetDropDown = new System.Windows.Forms.ComboBox();
            this.savePresetButton = new System.Windows.Forms.Button();
            this.pluginsPanel.SuspendLayout();
            this.defaultsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // pluginsPanel
            // 
            this.pluginsPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pluginsPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pluginsPanel.Controls.Add(this.pluginsHeading);
            this.pluginsPanel.Controls.Add(this.pluginsListView);
            this.pluginsPanel.Location = new System.Drawing.Point(12, 169);
            this.pluginsPanel.Name = "pluginsPanel";
            this.pluginsPanel.Size = new System.Drawing.Size(638, 347);
            this.pluginsPanel.TabIndex = 14;
            // 
            // pluginsHeading
            // 
            this.pluginsHeading.Dock = System.Windows.Forms.DockStyle.Top;
            this.pluginsHeading.Location = new System.Drawing.Point(0, 0);
            this.pluginsHeading.Name = "pluginsHeading";
            this.pluginsHeading.Size = new System.Drawing.Size(636, 23);
            this.pluginsHeading.TabIndex = 0;
            this.pluginsHeading.Text = "Installed Plugins";
            this.pluginsHeading.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pluginsListView
            // 
            this.pluginsListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pluginsListView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pluginsListView.CheckBoxes = true;
            this.pluginsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.NameColumn,
            this.VersionColumn,
            this.AuthorColumn});
            this.pluginsListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.pluginsListView.HideSelection = false;
            this.pluginsListView.Location = new System.Drawing.Point(12, 32);
            this.pluginsListView.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pluginsListView.Name = "pluginsListView";
            this.pluginsListView.ShowItemToolTips = true;
            this.pluginsListView.Size = new System.Drawing.Size(613, 301);
            this.pluginsListView.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.pluginsListView.TabIndex = 3;
            this.pluginsListView.UseCompatibleStateImageBehavior = false;
            this.pluginsListView.View = System.Windows.Forms.View.Details;
            this.pluginsListView.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.pluginsListView_ItemChecked);
            // 
            // NameColumn
            // 
            this.NameColumn.Text = "Name";
            this.NameColumn.Width = 225;
            // 
            // VersionColumn
            // 
            this.VersionColumn.Text = "Version";
            this.VersionColumn.Width = 62;
            // 
            // AuthorColumn
            // 
            this.AuthorColumn.Text = "Publisher";
            this.AuthorColumn.Width = 200;
            // 
            // defaultsPanel
            // 
            this.defaultsPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.defaultsPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.defaultsPanel.Controls.Add(this.label2);
            this.defaultsPanel.Controls.Add(this.defaultsHeading);
            this.defaultsPanel.Controls.Add(this.imageDropDown);
            this.defaultsPanel.Controls.Add(this.otherComboBox);
            this.defaultsPanel.Controls.Add(this.label1);
            this.defaultsPanel.Controls.Add(this.label4);
            this.defaultsPanel.Controls.Add(this.scriptComboBox);
            this.defaultsPanel.Controls.Add(this.typeComboBox);
            this.defaultsPanel.Controls.Add(this.engineComboBox);
            this.defaultsPanel.Controls.Add(this.label3);
            this.defaultsPanel.Controls.Add(this.label5);
            this.defaultsPanel.Location = new System.Drawing.Point(12, 42);
            this.defaultsPanel.Name = "defaultsPanel";
            this.defaultsPanel.Size = new System.Drawing.Size(638, 121);
            this.defaultsPanel.TabIndex = 13;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(342, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Image Editor";
            // 
            // defaultsHeading
            // 
            this.defaultsHeading.Dock = System.Windows.Forms.DockStyle.Top;
            this.defaultsHeading.Location = new System.Drawing.Point(0, 0);
            this.defaultsHeading.Name = "defaultsHeading";
            this.defaultsHeading.Size = new System.Drawing.Size(636, 23);
            this.defaultsHeading.TabIndex = 0;
            this.defaultsHeading.Text = "Default Handlers";
            this.defaultsHeading.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // imageDropDown
            // 
            this.imageDropDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.imageDropDown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.imageDropDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.imageDropDown.FormattingEnabled = true;
            this.imageDropDown.Location = new System.Drawing.Point(420, 59);
            this.imageDropDown.Name = "imageDropDown";
            this.imageDropDown.Size = new System.Drawing.Size(205, 21);
            this.imageDropDown.TabIndex = 9;
            this.imageDropDown.SelectedIndexChanged += new System.EventHandler(this.pluginComboBox_SelectedIndexChanged);
            // 
            // otherComboBox
            // 
            this.otherComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.otherComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.otherComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.otherComboBox.FormattingEnabled = true;
            this.otherComboBox.Location = new System.Drawing.Point(420, 86);
            this.otherComboBox.Name = "otherComboBox";
            this.otherComboBox.Size = new System.Drawing.Size(205, 21);
            this.otherComboBox.TabIndex = 1;
            this.otherComboBox.SelectedIndexChanged += new System.EventHandler(this.pluginComboBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(344, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Script Editor";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 62);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Project Type";
            // 
            // scriptComboBox
            // 
            this.scriptComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.scriptComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.scriptComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.scriptComboBox.FormattingEnabled = true;
            this.scriptComboBox.Location = new System.Drawing.Point(420, 32);
            this.scriptComboBox.Name = "scriptComboBox";
            this.scriptComboBox.Size = new System.Drawing.Size(205, 21);
            this.scriptComboBox.TabIndex = 7;
            this.scriptComboBox.SelectedIndexChanged += new System.EventHandler(this.pluginComboBox_SelectedIndexChanged);
            // 
            // typeComboBox
            // 
            this.typeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.typeComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.typeComboBox.FormattingEnabled = true;
            this.typeComboBox.Location = new System.Drawing.Point(86, 59);
            this.typeComboBox.Name = "typeComboBox";
            this.typeComboBox.Size = new System.Drawing.Size(190, 21);
            this.typeComboBox.TabIndex = 4;
            this.typeComboBox.SelectedIndexChanged += new System.EventHandler(this.pluginComboBox_SelectedIndexChanged);
            // 
            // engineComboBox
            // 
            this.engineComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.engineComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.engineComboBox.FormattingEnabled = true;
            this.engineComboBox.Location = new System.Drawing.Point(86, 32);
            this.engineComboBox.Name = "engineComboBox";
            this.engineComboBox.Size = new System.Drawing.Size(190, 21);
            this.engineComboBox.TabIndex = 6;
            this.engineComboBox.SelectedIndexChanged += new System.EventHandler(this.pluginComboBox_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(325, 89);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Other File Types";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(37, 35);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(40, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "Engine";
            // 
            // deletePresetButton
            // 
            this.deletePresetButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.deletePresetButton.Image = global::SphereStudio.Properties.Resources.cross;
            this.deletePresetButton.Location = new System.Drawing.Point(272, 10);
            this.deletePresetButton.Name = "deletePresetButton";
            this.deletePresetButton.Size = new System.Drawing.Size(36, 26);
            this.deletePresetButton.TabIndex = 11;
            this.deletePresetButton.UseVisualStyleBackColor = true;
            this.deletePresetButton.Click += new System.EventHandler(this.deletePresetButton_Click);
            // 
            // presetDropDown
            // 
            this.presetDropDown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.presetDropDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.presetDropDown.FormattingEnabled = true;
            this.presetDropDown.Location = new System.Drawing.Point(12, 11);
            this.presetDropDown.Name = "presetDropDown";
            this.presetDropDown.Size = new System.Drawing.Size(254, 21);
            this.presetDropDown.TabIndex = 10;
            this.presetDropDown.SelectedIndexChanged += new System.EventHandler(this.presetDropDown_SelectedIndexChanged);
            // 
            // savePresetButton
            // 
            this.savePresetButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.savePresetButton.Image = global::SphereStudio.Properties.Resources.disk;
            this.savePresetButton.Location = new System.Drawing.Point(314, 10);
            this.savePresetButton.Name = "savePresetButton";
            this.savePresetButton.Size = new System.Drawing.Size(36, 26);
            this.savePresetButton.TabIndex = 12;
            this.savePresetButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.savePresetButton.UseVisualStyleBackColor = true;
            this.savePresetButton.Click += new System.EventHandler(this.savePresetButton_Click);
            // 
            // PluginsSettingsPage
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.Controls.Add(this.pluginsPanel);
            this.Controls.Add(this.defaultsPanel);
            this.Controls.Add(this.deletePresetButton);
            this.Controls.Add(this.presetDropDown);
            this.Controls.Add(this.savePresetButton);
            this.Name = "PluginsSettingsPage";
            this.Size = new System.Drawing.Size(663, 530);
            this.pluginsPanel.ResumeLayout(false);
            this.defaultsPanel.ResumeLayout(false);
            this.defaultsPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pluginsPanel;
        private System.Windows.Forms.Label pluginsHeading;
        private System.Windows.Forms.ListView pluginsListView;
        private System.Windows.Forms.ColumnHeader NameColumn;
        private System.Windows.Forms.ColumnHeader AuthorColumn;
        private System.Windows.Forms.ColumnHeader VersionColumn;
        private System.Windows.Forms.Panel defaultsPanel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label defaultsHeading;
        private System.Windows.Forms.ComboBox imageDropDown;
        private System.Windows.Forms.ComboBox otherComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox scriptComboBox;
        private System.Windows.Forms.ComboBox typeComboBox;
        private System.Windows.Forms.ComboBox engineComboBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button deletePresetButton;
        private System.Windows.Forms.ComboBox presetDropDown;
        private System.Windows.Forms.Button savePresetButton;
    }
}
