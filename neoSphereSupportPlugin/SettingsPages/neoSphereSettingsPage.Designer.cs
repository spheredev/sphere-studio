namespace SphereStudio.SettingsPages
{
    partial class neoSphereSettingsPage
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
            this.showTracesButton = new System.Windows.Forms.CheckBox();
            this.testInWindowButton = new System.Windows.Forms.CheckBox();
            this.logLevelDropDown = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.testWithConsoleButton = new System.Windows.Forms.CheckBox();
            this.browseDirButton = new System.Windows.Forms.Button();
            this.enginePathTextBox = new System.Windows.Forms.TextBox();
            this.enginePathLabel = new System.Windows.Forms.Label();
            this.directoryPanel = new System.Windows.Forms.Panel();
            this.findEngineButton = new System.Windows.Forms.Button();
            this.directoryHeading = new System.Windows.Forms.Label();
            this.debugPanel = new System.Windows.Forms.Panel();
            this.retroModeCheckBox = new System.Windows.Forms.CheckBox();
            this.debugHeading = new System.Windows.Forms.Label();
            this.directoryPanel.SuspendLayout();
            this.debugPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // showTracesButton
            // 
            this.showTracesButton.AutoSize = true;
            this.showTracesButton.Location = new System.Drawing.Point(13, 80);
            this.showTracesButton.Name = "showTracesButton";
            this.showTracesButton.Size = new System.Drawing.Size(278, 17);
            this.showTracesButton.TabIndex = 4;
            this.showTracesButton.Text = "Show output produced by \'SSj.trace\' while debugging";
            this.showTracesButton.UseVisualStyleBackColor = true;
            // 
            // testInWindowButton
            // 
            this.testInWindowButton.AutoSize = true;
            this.testInWindowButton.Location = new System.Drawing.Point(13, 57);
            this.testInWindowButton.Name = "testInWindowButton";
            this.testInWindowButton.Size = new System.Drawing.Size(411, 17);
            this.testInWindowButton.TabIndex = 3;
            this.testInWindowButton.Text = "Force the engine to start in windowed mode when using the Test Game command";
            this.testInWindowButton.UseVisualStyleBackColor = true;
            // 
            // logLevelDropDown
            // 
            this.logLevelDropDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.logLevelDropDown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.logLevelDropDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.logLevelDropDown.FormattingEnabled = true;
            this.logLevelDropDown.Items.AddRange(new object[] {
            "V0 - game output only",
            "V1 - basic logging",
            "V2 - high-level logging",
            "V3 - low-level logging",
            "V4 - log everything!"});
            this.logLevelDropDown.Location = new System.Drawing.Point(366, 32);
            this.logLevelDropDown.Name = "logLevelDropDown";
            this.logLevelDropDown.Size = new System.Drawing.Size(148, 21);
            this.logLevelDropDown.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(306, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Log Level";
            // 
            // testWithConsoleButton
            // 
            this.testWithConsoleButton.AutoSize = true;
            this.testWithConsoleButton.Location = new System.Drawing.Point(13, 34);
            this.testWithConsoleButton.Name = "testWithConsoleButton";
            this.testWithConsoleButton.Size = new System.Drawing.Size(262, 17);
            this.testWithConsoleButton.TabIndex = 0;
            this.testWithConsoleButton.Text = "Use SpheRun to handle the Test Game command";
            this.testWithConsoleButton.UseVisualStyleBackColor = true;
            // 
            // browseDirButton
            // 
            this.browseDirButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.browseDirButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.browseDirButton.Image = global::SphereStudio.Properties.Resources.FolderIcon;
            this.browseDirButton.Location = new System.Drawing.Point(434, 58);
            this.browseDirButton.Name = "browseDirButton";
            this.browseDirButton.Size = new System.Drawing.Size(80, 25);
            this.browseDirButton.TabIndex = 4;
            this.browseDirButton.Text = "&Browse...";
            this.browseDirButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.browseDirButton.UseVisualStyleBackColor = true;
            this.browseDirButton.Click += new System.EventHandler(this.browseDirButton_Click);
            // 
            // enginePathTextBox
            // 
            this.enginePathTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.enginePathTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.enginePathTextBox.Location = new System.Drawing.Point(65, 32);
            this.enginePathTextBox.Name = "enginePathTextBox";
            this.enginePathTextBox.Size = new System.Drawing.Size(449, 20);
            this.enginePathTextBox.TabIndex = 2;
            // 
            // enginePathLabel
            // 
            this.enginePathLabel.AutoSize = true;
            this.enginePathLabel.Location = new System.Drawing.Point(10, 34);
            this.enginePathLabel.Name = "enginePathLabel";
            this.enginePathLabel.Size = new System.Drawing.Size(49, 13);
            this.enginePathLabel.TabIndex = 0;
            this.enginePathLabel.Text = "Directory";
            // 
            // directoryPanel
            // 
            this.directoryPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.directoryPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.directoryPanel.Controls.Add(this.findEngineButton);
            this.directoryPanel.Controls.Add(this.browseDirButton);
            this.directoryPanel.Controls.Add(this.directoryHeading);
            this.directoryPanel.Controls.Add(this.enginePathTextBox);
            this.directoryPanel.Controls.Add(this.enginePathLabel);
            this.directoryPanel.Location = new System.Drawing.Point(9, 12);
            this.directoryPanel.Name = "directoryPanel";
            this.directoryPanel.Size = new System.Drawing.Size(526, 94);
            this.directoryPanel.TabIndex = 4;
            // 
            // findEngineButton
            // 
            this.findEngineButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.findEngineButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.findEngineButton.Location = new System.Drawing.Point(328, 58);
            this.findEngineButton.Name = "findEngineButton";
            this.findEngineButton.Size = new System.Drawing.Size(100, 25);
            this.findEngineButton.TabIndex = 5;
            this.findEngineButton.Text = "&Find Engine...";
            this.findEngineButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.findEngineButton.UseVisualStyleBackColor = true;
            this.findEngineButton.Click += new System.EventHandler(this.findEngineButton_Click);
            // 
            // directoryHeading
            // 
            this.directoryHeading.Dock = System.Windows.Forms.DockStyle.Top;
            this.directoryHeading.Location = new System.Drawing.Point(0, 0);
            this.directoryHeading.Name = "directoryHeading";
            this.directoryHeading.Size = new System.Drawing.Size(524, 23);
            this.directoryHeading.TabIndex = 0;
            this.directoryHeading.Text = "neoSphere Installation";
            this.directoryHeading.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // debugPanel
            // 
            this.debugPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.debugPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.debugPanel.Controls.Add(this.retroModeCheckBox);
            this.debugPanel.Controls.Add(this.showTracesButton);
            this.debugPanel.Controls.Add(this.testWithConsoleButton);
            this.debugPanel.Controls.Add(this.testInWindowButton);
            this.debugPanel.Controls.Add(this.logLevelDropDown);
            this.debugPanel.Controls.Add(this.debugHeading);
            this.debugPanel.Controls.Add(this.label2);
            this.debugPanel.Location = new System.Drawing.Point(9, 113);
            this.debugPanel.Name = "debugPanel";
            this.debugPanel.Size = new System.Drawing.Size(526, 132);
            this.debugPanel.TabIndex = 5;
            // 
            // retroModeCheckBox
            // 
            this.retroModeCheckBox.AutoSize = true;
            this.retroModeCheckBox.Location = new System.Drawing.Point(13, 103);
            this.retroModeCheckBox.Name = "retroModeCheckBox";
            this.retroModeCheckBox.Size = new System.Drawing.Size(368, 17);
            this.retroModeCheckBox.TabIndex = 5;
            this.retroModeCheckBox.Text = "Emulate targeted API level when testing for distribution (retrograde mode)";
            this.retroModeCheckBox.UseVisualStyleBackColor = true;
            // 
            // debugHeading
            // 
            this.debugHeading.Dock = System.Windows.Forms.DockStyle.Top;
            this.debugHeading.Location = new System.Drawing.Point(0, 0);
            this.debugHeading.Name = "debugHeading";
            this.debugHeading.Size = new System.Drawing.Size(524, 23);
            this.debugHeading.TabIndex = 0;
            this.debugHeading.Text = "Configuration";
            this.debugHeading.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // neoSphereSettingsPage
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.AutoScroll = true;
            this.Controls.Add(this.debugPanel);
            this.Controls.Add(this.directoryPanel);
            this.Name = "neoSphereSettingsPage";
            this.Size = new System.Drawing.Size(545, 349);
            this.directoryPanel.ResumeLayout(false);
            this.directoryPanel.PerformLayout();
            this.debugPanel.ResumeLayout(false);
            this.debugPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button browseDirButton;
        private System.Windows.Forms.TextBox enginePathTextBox;
        private System.Windows.Forms.Label enginePathLabel;
        private System.Windows.Forms.CheckBox testWithConsoleButton;
        private System.Windows.Forms.CheckBox testInWindowButton;
        private System.Windows.Forms.ComboBox logLevelDropDown;
        private System.Windows.Forms.Label label2;
		private System.Windows.Forms.CheckBox showTracesButton;
        private System.Windows.Forms.Panel directoryPanel;
        private System.Windows.Forms.Label directoryHeading;
        private System.Windows.Forms.Panel debugPanel;
        private System.Windows.Forms.Label debugHeading;
        private System.Windows.Forms.CheckBox retroModeCheckBox;
        private System.Windows.Forms.Button findEngineButton;
    }
}
