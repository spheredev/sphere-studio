namespace SphereStudio.ProjectPages
{
    partial class SphereProjectPage
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
            this.apiPanel = new System.Windows.Forms.Panel();
            this.autoJsonCheckBox = new System.Windows.Forms.CheckBox();
            this.levelLabel = new System.Windows.Forms.Label();
            this.levelEditBox = new System.Windows.Forms.NumericUpDown();
            this.apiDropDown = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.apiHeading = new System.Windows.Forms.Label();
            this.runtimePanel = new System.Windows.Forms.Panel();
            this.saveIdTextBox = new System.Windows.Forms.TextBox();
            this.saveIdLabel = new System.Windows.Forms.Label();
            this.heightUpDown = new System.Windows.Forms.NumericUpDown();
            this.widthUpDown = new System.Windows.Forms.NumericUpDown();
            this.scriptPathComboBox = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.ResoLabel = new System.Windows.Forms.Label();
            this.resolutionDropDown = new System.Windows.Forms.ComboBox();
            this.runtimeHeading = new System.Windows.Forms.Label();
            this.apiPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.levelEditBox)).BeginInit();
            this.runtimePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.heightUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.widthUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // apiPanel
            // 
            this.apiPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.apiPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.apiPanel.Controls.Add(this.autoJsonCheckBox);
            this.apiPanel.Controls.Add(this.levelLabel);
            this.apiPanel.Controls.Add(this.levelEditBox);
            this.apiPanel.Controls.Add(this.apiDropDown);
            this.apiPanel.Controls.Add(this.label1);
            this.apiPanel.Controls.Add(this.apiHeading);
            this.apiPanel.Location = new System.Drawing.Point(9, 12);
            this.apiPanel.Name = "apiPanel";
            this.apiPanel.Size = new System.Drawing.Size(454, 93);
            this.apiPanel.TabIndex = 0;
            // 
            // autoJsonCheckBox
            // 
            this.autoJsonCheckBox.AutoSize = true;
            this.autoJsonCheckBox.Location = new System.Drawing.Point(76, 60);
            this.autoJsonCheckBox.Name = "autoJsonCheckBox";
            this.autoJsonCheckBox.Size = new System.Drawing.Size(184, 17);
            this.autoJsonCheckBox.TabIndex = 2;
            this.autoJsonCheckBox.Text = "Automatically manage \'game.json\'";
            this.autoJsonCheckBox.UseVisualStyleBackColor = true;
            // 
            // levelLabel
            // 
            this.levelLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.levelLabel.AutoSize = true;
            this.levelLabel.Location = new System.Drawing.Point(341, 61);
            this.levelLabel.Name = "levelLabel";
            this.levelLabel.Size = new System.Drawing.Size(53, 13);
            this.levelLabel.TabIndex = 3;
            this.levelLabel.Text = "API Level";
            // 
            // levelEditBox
            // 
            this.levelEditBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.levelEditBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.levelEditBox.Location = new System.Drawing.Point(397, 59);
            this.levelEditBox.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.levelEditBox.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.levelEditBox.Name = "levelEditBox";
            this.levelEditBox.Size = new System.Drawing.Size(43, 20);
            this.levelEditBox.TabIndex = 4;
            this.levelEditBox.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // apiDropDown
            // 
            this.apiDropDown.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.apiDropDown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.apiDropDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.apiDropDown.FormattingEnabled = true;
            this.apiDropDown.Location = new System.Drawing.Point(76, 32);
            this.apiDropDown.Name = "apiDropDown";
            this.apiDropDown.Size = new System.Drawing.Size(364, 21);
            this.apiDropDown.TabIndex = 1;
            this.apiDropDown.SelectedIndexChanged += new System.EventHandler(this.apiDropDown_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Platform";
            // 
            // apiHeading
            // 
            this.apiHeading.Dock = System.Windows.Forms.DockStyle.Top;
            this.apiHeading.Location = new System.Drawing.Point(0, 0);
            this.apiHeading.Name = "apiHeading";
            this.apiHeading.Size = new System.Drawing.Size(452, 23);
            this.apiHeading.TabIndex = 0;
            this.apiHeading.Text = "Sphere API";
            this.apiHeading.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // runtimePanel
            // 
            this.runtimePanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.runtimePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.runtimePanel.Controls.Add(this.saveIdTextBox);
            this.runtimePanel.Controls.Add(this.saveIdLabel);
            this.runtimePanel.Controls.Add(this.heightUpDown);
            this.runtimePanel.Controls.Add(this.widthUpDown);
            this.runtimePanel.Controls.Add(this.scriptPathComboBox);
            this.runtimePanel.Controls.Add(this.label3);
            this.runtimePanel.Controls.Add(this.ResoLabel);
            this.runtimePanel.Controls.Add(this.resolutionDropDown);
            this.runtimePanel.Controls.Add(this.runtimeHeading);
            this.runtimePanel.Location = new System.Drawing.Point(9, 111);
            this.runtimePanel.Name = "runtimePanel";
            this.runtimePanel.Size = new System.Drawing.Size(454, 120);
            this.runtimePanel.TabIndex = 1;
            // 
            // saveIdTextBox
            // 
            this.saveIdTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.saveIdTextBox.Location = new System.Drawing.Point(76, 86);
            this.saveIdTextBox.Name = "saveIdTextBox";
            this.saveIdTextBox.Size = new System.Drawing.Size(364, 20);
            this.saveIdTextBox.TabIndex = 7;
            // 
            // saveIdLabel
            // 
            this.saveIdLabel.AutoSize = true;
            this.saveIdLabel.Location = new System.Drawing.Point(24, 89);
            this.saveIdLabel.Name = "saveIdLabel";
            this.saveIdLabel.Size = new System.Drawing.Size(46, 13);
            this.saveIdLabel.TabIndex = 6;
            this.saveIdLabel.Text = "Save ID";
            // 
            // heightUpDown
            // 
            this.heightUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.heightUpDown.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.heightUpDown.Increment = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.heightUpDown.Location = new System.Drawing.Point(395, 59);
            this.heightUpDown.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.heightUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.heightUpDown.Name = "heightUpDown";
            this.heightUpDown.Size = new System.Drawing.Size(45, 20);
            this.heightUpDown.TabIndex = 5;
            this.heightUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // widthUpDown
            // 
            this.widthUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.widthUpDown.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.widthUpDown.Increment = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.widthUpDown.Location = new System.Drawing.Point(344, 59);
            this.widthUpDown.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.widthUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.widthUpDown.Name = "widthUpDown";
            this.widthUpDown.Size = new System.Drawing.Size(45, 20);
            this.widthUpDown.TabIndex = 4;
            this.widthUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // scriptPathComboBox
            // 
            this.scriptPathComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.scriptPathComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.scriptPathComboBox.FormattingEnabled = true;
            this.scriptPathComboBox.Location = new System.Drawing.Point(76, 32);
            this.scriptPathComboBox.Name = "scriptPathComboBox";
            this.scriptPathComboBox.Size = new System.Drawing.Size(364, 21);
            this.scriptPathComboBox.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 35);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Main Script";
            // 
            // ResoLabel
            // 
            this.ResoLabel.AutoSize = true;
            this.ResoLabel.Location = new System.Drawing.Point(13, 62);
            this.ResoLabel.Name = "ResoLabel";
            this.ResoLabel.Size = new System.Drawing.Size(57, 13);
            this.ResoLabel.TabIndex = 2;
            this.ResoLabel.Text = "Resolution";
            // 
            // resolutionDropDown
            // 
            this.resolutionDropDown.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.resolutionDropDown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.resolutionDropDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.resolutionDropDown.FormattingEnabled = true;
            this.resolutionDropDown.Items.AddRange(new object[] {
            "Custom Resolution",
            "320x240",
            "640x480",
            "800x600",
            "1024x768",
            "1280x720",
            "1920x1080"});
            this.resolutionDropDown.Location = new System.Drawing.Point(76, 59);
            this.resolutionDropDown.Name = "resolutionDropDown";
            this.resolutionDropDown.Size = new System.Drawing.Size(262, 21);
            this.resolutionDropDown.TabIndex = 3;
            this.resolutionDropDown.SelectedIndexChanged += new System.EventHandler(this.resoDropDown_SelectedIndexChanged);
            // 
            // runtimeHeading
            // 
            this.runtimeHeading.Dock = System.Windows.Forms.DockStyle.Top;
            this.runtimeHeading.Location = new System.Drawing.Point(0, 0);
            this.runtimeHeading.Name = "runtimeHeading";
            this.runtimeHeading.Size = new System.Drawing.Size(452, 23);
            this.runtimeHeading.TabIndex = 0;
            this.runtimeHeading.Text = "Runtime";
            this.runtimeHeading.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // SphereProjectPage
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.Controls.Add(this.runtimePanel);
            this.Controls.Add(this.apiPanel);
            this.Name = "SphereProjectPage";
            this.Size = new System.Drawing.Size(473, 348);
            this.apiPanel.ResumeLayout(false);
            this.apiPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.levelEditBox)).EndInit();
            this.runtimePanel.ResumeLayout(false);
            this.runtimePanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.heightUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.widthUpDown)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel apiPanel;
        private System.Windows.Forms.Label apiHeading;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox apiDropDown;
        private System.Windows.Forms.Label levelLabel;
        private System.Windows.Forms.NumericUpDown levelEditBox;
        private System.Windows.Forms.Panel runtimePanel;
        private System.Windows.Forms.Label runtimeHeading;
        private System.Windows.Forms.Label ResoLabel;
        private System.Windows.Forms.ComboBox resolutionDropDown;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox scriptPathComboBox;
        private System.Windows.Forms.NumericUpDown heightUpDown;
        private System.Windows.Forms.NumericUpDown widthUpDown;
        private System.Windows.Forms.TextBox saveIdTextBox;
        private System.Windows.Forms.Label saveIdLabel;
        private System.Windows.Forms.CheckBox autoJsonCheckBox;
    }
}
