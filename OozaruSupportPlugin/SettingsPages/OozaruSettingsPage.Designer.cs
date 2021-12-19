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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OozaruSettingsPage));
            this.directoryPanel = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.portUpDown = new System.Windows.Forms.NumericUpDown();
            this.browseDirButton = new System.Windows.Forms.Button();
            this.directoryHeading = new System.Windows.Forms.Label();
            this.enginePathTextBox = new System.Windows.Forms.TextBox();
            this.pathLabel = new System.Windows.Forms.Label();
            this.infoHeading = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.infoPanel = new System.Windows.Forms.Panel();
            this.directoryPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.portUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.infoPanel.SuspendLayout();
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
            this.directoryPanel.Location = new System.Drawing.Point(9, 111);
            this.directoryPanel.Name = "directoryPanel";
            this.directoryPanel.Size = new System.Drawing.Size(453, 95);
            this.directoryPanel.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(37, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Port";
            // 
            // portUpDown
            // 
            this.portUpDown.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.portUpDown.Location = new System.Drawing.Point(69, 58);
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
            this.portUpDown.Size = new System.Drawing.Size(55, 20);
            this.portUpDown.TabIndex = 5;
            this.portUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
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
            this.directoryHeading.Text = "Server Configuration";
            this.directoryHeading.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // enginePathTextBox
            // 
            this.enginePathTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.enginePathTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.enginePathTextBox.Location = new System.Drawing.Point(69, 32);
            this.enginePathTextBox.Name = "enginePathTextBox";
            this.enginePathTextBox.Size = new System.Drawing.Size(372, 20);
            this.enginePathTextBox.TabIndex = 2;
            // 
            // pathLabel
            // 
            this.pathLabel.AutoSize = true;
            this.pathLabel.Location = new System.Drawing.Point(10, 34);
            this.pathLabel.Name = "pathLabel";
            this.pathLabel.Size = new System.Drawing.Size(53, 13);
            this.pathLabel.TabIndex = 0;
            this.pathLabel.Text = "Run From";
            // 
            // infoHeading
            // 
            this.infoHeading.Dock = System.Windows.Forms.DockStyle.Top;
            this.infoHeading.Location = new System.Drawing.Point(0, 0);
            this.infoHeading.Name = "infoHeading";
            this.infoHeading.Size = new System.Drawing.Size(451, 23);
            this.infoHeading.TabIndex = 0;
            this.infoHeading.Text = "Oozaru Support";
            this.infoHeading.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoEllipsis = true;
            this.label2.Location = new System.Drawing.Point(35, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(406, 45);
            this.label2.TabIndex = 7;
            this.label2.Text = resources.GetString("label2.Text");
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::SphereStudio.Properties.Resources.InfoIcon;
            this.pictureBox1.Location = new System.Drawing.Point(13, 34);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(16, 16);
            this.pictureBox1.TabIndex = 8;
            this.pictureBox1.TabStop = false;
            // 
            // infoPanel
            // 
            this.infoPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.infoPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.infoPanel.Controls.Add(this.pictureBox1);
            this.infoPanel.Controls.Add(this.label2);
            this.infoPanel.Controls.Add(this.infoHeading);
            this.infoPanel.Location = new System.Drawing.Point(9, 12);
            this.infoPanel.Name = "infoPanel";
            this.infoPanel.Size = new System.Drawing.Size(453, 93);
            this.infoPanel.TabIndex = 8;
            // 
            // OozaruSettingsPage
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.Controls.Add(this.infoPanel);
            this.Controls.Add(this.directoryPanel);
            this.Name = "OozaruSettingsPage";
            this.Size = new System.Drawing.Size(472, 332);
            this.directoryPanel.ResumeLayout(false);
            this.directoryPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.portUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.infoPanel.ResumeLayout(false);
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
        private System.Windows.Forms.Label infoHeading;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel infoPanel;
    }
}
