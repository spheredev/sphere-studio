namespace SphereStudio.SettingsPages
{
    partial class OozaruSettingsPage
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
            this.directoryPanel = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.portUpDown = new System.Windows.Forms.NumericUpDown();
            this.browseDirButton = new System.Windows.Forms.Button();
            this.directoryHeading = new System.Windows.Forms.Label();
            this.enginePathTextBox = new System.Windows.Forms.TextBox();
            this.pathLabel = new System.Windows.Forms.Label();
            this.directoryPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.portUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // directoryPanel
            // 
            this.directoryPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.directoryPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.directoryPanel.Controls.Add(this.label1);
            this.directoryPanel.Controls.Add(this.portUpDown);
            this.directoryPanel.Controls.Add(this.browseDirButton);
            this.directoryPanel.Controls.Add(this.directoryHeading);
            this.directoryPanel.Controls.Add(this.enginePathTextBox);
            this.directoryPanel.Controls.Add(this.pathLabel);
            this.directoryPanel.Location = new System.Drawing.Point(9, 12);
            this.directoryPanel.Name = "directoryPanel";
            this.directoryPanel.Size = new System.Drawing.Size(453, 96);
            this.directoryPanel.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(39, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Port";
            // 
            // portUpDown
            // 
            this.portUpDown.Location = new System.Drawing.Point(71, 58);
            this.portUpDown.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.portUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.portUpDown.Name = "portUpDown";
            this.portUpDown.Size = new System.Drawing.Size(60, 20);
            this.portUpDown.TabIndex = 5;
            this.portUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // browseDirButton
            // 
            this.browseDirButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.browseDirButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.browseDirButton.Image = global::SphereStudio.Properties.Resources.FolderIcon;
            this.browseDirButton.Location = new System.Drawing.Point(361, 58);
            this.browseDirButton.Name = "browseDirButton";
            this.browseDirButton.Size = new System.Drawing.Size(80, 25);
            this.browseDirButton.TabIndex = 4;
            this.browseDirButton.Text = "Browse...";
            this.browseDirButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.browseDirButton.UseVisualStyleBackColor = true;
            this.browseDirButton.Click += new System.EventHandler(this.browseDirButton_Click);
            // 
            // directoryHeading
            // 
            this.directoryHeading.Dock = System.Windows.Forms.DockStyle.Top;
            this.directoryHeading.Location = new System.Drawing.Point(0, 0);
            this.directoryHeading.Name = "directoryHeading";
            this.directoryHeading.Size = new System.Drawing.Size(451, 23);
            this.directoryHeading.TabIndex = 0;
            this.directoryHeading.Text = "Oozaru Local Server";
            this.directoryHeading.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // enginePathTextBox
            // 
            this.enginePathTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.enginePathTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.enginePathTextBox.Location = new System.Drawing.Point(71, 32);
            this.enginePathTextBox.Name = "enginePathTextBox";
            this.enginePathTextBox.Size = new System.Drawing.Size(370, 20);
            this.enginePathTextBox.TabIndex = 2;
            // 
            // pathLabel
            // 
            this.pathLabel.AutoSize = true;
            this.pathLabel.Location = new System.Drawing.Point(10, 34);
            this.pathLabel.Name = "pathLabel";
            this.pathLabel.Size = new System.Drawing.Size(55, 13);
            this.pathLabel.TabIndex = 0;
            this.pathLabel.Text = "Host From";
            // 
            // OozaruSettingsPage
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.Controls.Add(this.directoryPanel);
            this.Name = "OozaruSettingsPage";
            this.Size = new System.Drawing.Size(472, 332);
            this.directoryPanel.ResumeLayout(false);
            this.directoryPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.portUpDown)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel directoryPanel;
        private System.Windows.Forms.Button browseDirButton;
        private System.Windows.Forms.Label directoryHeading;
        private System.Windows.Forms.TextBox enginePathTextBox;
        private System.Windows.Forms.Label pathLabel;
        private System.Windows.Forms.NumericUpDown portUpDown;
        private System.Windows.Forms.Label label1;
    }
}
