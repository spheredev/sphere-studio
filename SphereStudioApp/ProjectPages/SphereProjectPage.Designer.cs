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
            this.levelLabel = new System.Windows.Forms.Label();
            this.levelEditBox = new System.Windows.Forms.NumericUpDown();
            this.apiDropDown = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.apiHeading = new System.Windows.Forms.Label();
            this.startupPanel = new System.Windows.Forms.Panel();
            this.heightEditBox = new System.Windows.Forms.NumericUpDown();
            this.widthEditBox = new System.Windows.Forms.NumericUpDown();
            this.scriptPathEditBox = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.ResoLabel = new System.Windows.Forms.Label();
            this.resoDropDown = new System.Windows.Forms.ComboBox();
            this.startupHeading = new System.Windows.Forms.Label();
            this.apiPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.levelEditBox)).BeginInit();
            this.startupPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.heightEditBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.widthEditBox)).BeginInit();
            this.SuspendLayout();
            // 
            // apiPanel
            // 
            this.apiPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.apiPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.apiPanel.Controls.Add(this.levelLabel);
            this.apiPanel.Controls.Add(this.levelEditBox);
            this.apiPanel.Controls.Add(this.apiDropDown);
            this.apiPanel.Controls.Add(this.label1);
            this.apiPanel.Controls.Add(this.apiHeading);
            this.apiPanel.Location = new System.Drawing.Point(9, 12);
            this.apiPanel.Name = "apiPanel";
            this.apiPanel.Size = new System.Drawing.Size(454, 69);
            this.apiPanel.TabIndex = 0;
            // 
            // levelLabel
            // 
            this.levelLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.levelLabel.AutoSize = true;
            this.levelLabel.Location = new System.Drawing.Point(358, 35);
            this.levelLabel.Name = "levelLabel";
            this.levelLabel.Size = new System.Drawing.Size(33, 13);
            this.levelLabel.TabIndex = 1;
            this.levelLabel.Text = "Level";
            // 
            // levelEditBox
            // 
            this.levelEditBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.levelEditBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.levelEditBox.Location = new System.Drawing.Point(397, 33);
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
            this.levelEditBox.TabIndex = 1;
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
            this.apiDropDown.Location = new System.Drawing.Point(40, 32);
            this.apiDropDown.Name = "apiDropDown";
            this.apiDropDown.Size = new System.Drawing.Size(312, 21);
            this.apiDropDown.TabIndex = 2;
            this.apiDropDown.SelectedIndexChanged += new System.EventHandler(this.apiDropDown_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(24, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "API";
            // 
            // apiHeading
            // 
            this.apiHeading.Dock = System.Windows.Forms.DockStyle.Top;
            this.apiHeading.Location = new System.Drawing.Point(0, 0);
            this.apiHeading.Name = "apiHeading";
            this.apiHeading.Size = new System.Drawing.Size(452, 23);
            this.apiHeading.TabIndex = 0;
            this.apiHeading.Text = "Minimum Requirement";
            this.apiHeading.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // startupPanel
            // 
            this.startupPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.startupPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.startupPanel.Controls.Add(this.heightEditBox);
            this.startupPanel.Controls.Add(this.widthEditBox);
            this.startupPanel.Controls.Add(this.scriptPathEditBox);
            this.startupPanel.Controls.Add(this.label3);
            this.startupPanel.Controls.Add(this.ResoLabel);
            this.startupPanel.Controls.Add(this.resoDropDown);
            this.startupPanel.Controls.Add(this.startupHeading);
            this.startupPanel.Location = new System.Drawing.Point(9, 87);
            this.startupPanel.Name = "startupPanel";
            this.startupPanel.Size = new System.Drawing.Size(454, 94);
            this.startupPanel.TabIndex = 1;
            // 
            // heightEditBox
            // 
            this.heightEditBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.heightEditBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.heightEditBox.Increment = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.heightEditBox.Location = new System.Drawing.Point(395, 59);
            this.heightEditBox.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.heightEditBox.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.heightEditBox.Name = "heightEditBox";
            this.heightEditBox.Size = new System.Drawing.Size(45, 20);
            this.heightEditBox.TabIndex = 18;
            this.heightEditBox.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // widthEditBox
            // 
            this.widthEditBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.widthEditBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.widthEditBox.Increment = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.widthEditBox.Location = new System.Drawing.Point(344, 59);
            this.widthEditBox.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.widthEditBox.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.widthEditBox.Name = "widthEditBox";
            this.widthEditBox.Size = new System.Drawing.Size(45, 20);
            this.widthEditBox.TabIndex = 17;
            this.widthEditBox.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // moduleEditBox
            // 
            this.scriptPathEditBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.scriptPathEditBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.scriptPathEditBox.FormattingEnabled = true;
            this.scriptPathEditBox.Location = new System.Drawing.Point(76, 32);
            this.scriptPathEditBox.Name = "moduleEditBox";
            this.scriptPathEditBox.Size = new System.Drawing.Size(364, 21);
            this.scriptPathEditBox.TabIndex = 16;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 35);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "Main Script";
            // 
            // ResoLabel
            // 
            this.ResoLabel.AutoSize = true;
            this.ResoLabel.Location = new System.Drawing.Point(13, 62);
            this.ResoLabel.Name = "ResoLabel";
            this.ResoLabel.Size = new System.Drawing.Size(57, 13);
            this.ResoLabel.TabIndex = 11;
            this.ResoLabel.Text = "Resolution";
            // 
            // resoDropDown
            // 
            this.resoDropDown.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.resoDropDown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.resoDropDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.resoDropDown.FormattingEnabled = true;
            this.resoDropDown.Items.AddRange(new object[] {
            "Custom Resolution",
            "320x240",
            "640x480",
            "800x600",
            "1024x768",
            "1280x720",
            "1920x1080"});
            this.resoDropDown.Location = new System.Drawing.Point(76, 59);
            this.resoDropDown.Name = "resoDropDown";
            this.resoDropDown.Size = new System.Drawing.Size(262, 21);
            this.resoDropDown.TabIndex = 12;
            this.resoDropDown.SelectedIndexChanged += new System.EventHandler(this.resoDropDown_SelectedIndexChanged);
            // 
            // startupHeading
            // 
            this.startupHeading.Dock = System.Windows.Forms.DockStyle.Top;
            this.startupHeading.Location = new System.Drawing.Point(0, 0);
            this.startupHeading.Name = "startupHeading";
            this.startupHeading.Size = new System.Drawing.Size(452, 23);
            this.startupHeading.TabIndex = 0;
            this.startupHeading.Text = "Startup";
            this.startupHeading.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // SphereProjectPage
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.Controls.Add(this.startupPanel);
            this.Controls.Add(this.apiPanel);
            this.Name = "SphereProjectPage";
            this.Size = new System.Drawing.Size(473, 348);
            this.apiPanel.ResumeLayout(false);
            this.apiPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.levelEditBox)).EndInit();
            this.startupPanel.ResumeLayout(false);
            this.startupPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.heightEditBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.widthEditBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel apiPanel;
        private System.Windows.Forms.Label apiHeading;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox apiDropDown;
        private System.Windows.Forms.Label levelLabel;
        private System.Windows.Forms.NumericUpDown levelEditBox;
        private System.Windows.Forms.Panel startupPanel;
        private System.Windows.Forms.Label startupHeading;
        private System.Windows.Forms.Label ResoLabel;
        private System.Windows.Forms.ComboBox resoDropDown;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox scriptPathEditBox;
        private System.Windows.Forms.NumericUpDown heightEditBox;
        private System.Windows.Forms.NumericUpDown widthEditBox;
    }
}
