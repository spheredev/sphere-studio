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
            this.serverPanel = new System.Windows.Forms.Panel();
            this.publisherLabel = new System.Windows.Forms.Label();
            this.engineLabel = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.portUpDown = new System.Windows.Forms.NumericUpDown();
            this.browseDirButton = new System.Windows.Forms.Button();
            this.serverHeading = new System.Windows.Forms.Label();
            this.enginePathTextBox = new System.Windows.Forms.TextBox();
            this.pathLabel = new System.Windows.Forms.Label();
            this.tipHeading = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tipPanel = new System.Windows.Forms.Panel();
            this.serverPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.portUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tipPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // serverPanel
            // 
            this.serverPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.serverPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.serverPanel.Controls.Add(this.publisherLabel);
            this.serverPanel.Controls.Add(this.engineLabel);
            this.serverPanel.Controls.Add(this.label4);
            this.serverPanel.Controls.Add(this.label3);
            this.serverPanel.Controls.Add(this.label1);
            this.serverPanel.Controls.Add(this.portUpDown);
            this.serverPanel.Controls.Add(this.browseDirButton);
            this.serverPanel.Controls.Add(this.serverHeading);
            this.serverPanel.Controls.Add(this.enginePathTextBox);
            this.serverPanel.Controls.Add(this.pathLabel);
            this.serverPanel.Location = new System.Drawing.Point(9, 111);
            this.serverPanel.Name = "serverPanel";
            this.serverPanel.Size = new System.Drawing.Size(453, 148);
            this.serverPanel.TabIndex = 5;
            // 
            // publisherLabel
            // 
            this.publisherLabel.AutoSize = true;
            this.publisherLabel.Location = new System.Drawing.Point(69, 117);
            this.publisherLabel.Name = "publisherLabel";
            this.publisherLabel.Size = new System.Drawing.Size(53, 13);
            this.publisherLabel.TabIndex = 10;
            this.publisherLabel.Text = "Unknown";
            // 
            // engineLabel
            // 
            this.engineLabel.AutoSize = true;
            this.engineLabel.Location = new System.Drawing.Point(69, 94);
            this.engineLabel.Name = "engineLabel";
            this.engineLabel.Size = new System.Drawing.Size(68, 13);
            this.engineLabel.TabIndex = 9;
            this.engineLabel.Text = "Oozaru 0.0.0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 117);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Publisher";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 94);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Version";
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
            // serverHeading
            // 
            this.serverHeading.Dock = System.Windows.Forms.DockStyle.Top;
            this.serverHeading.Location = new System.Drawing.Point(0, 0);
            this.serverHeading.Name = "serverHeading";
            this.serverHeading.Size = new System.Drawing.Size(451, 23);
            this.serverHeading.TabIndex = 0;
            this.serverHeading.Text = "Oozaru Server";
            this.serverHeading.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
            this.enginePathTextBox.TextChanged += new System.EventHandler(this.enginePathTextBox_TextChanged);
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
            // tipHeading
            // 
            this.tipHeading.Dock = System.Windows.Forms.DockStyle.Top;
            this.tipHeading.Location = new System.Drawing.Point(0, 0);
            this.tipHeading.Name = "tipHeading";
            this.tipHeading.Size = new System.Drawing.Size(451, 23);
            this.tipHeading.TabIndex = 0;
            this.tipHeading.Text = "note about Oozaru support";
            this.tipHeading.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
            // tipPanel
            // 
            this.tipPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tipPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tipPanel.Controls.Add(this.pictureBox1);
            this.tipPanel.Controls.Add(this.label2);
            this.tipPanel.Controls.Add(this.tipHeading);
            this.tipPanel.Location = new System.Drawing.Point(9, 12);
            this.tipPanel.Name = "tipPanel";
            this.tipPanel.Size = new System.Drawing.Size(453, 93);
            this.tipPanel.TabIndex = 8;
            // 
            // OozaruSettingsPage
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.Controls.Add(this.tipPanel);
            this.Controls.Add(this.serverPanel);
            this.Name = "OozaruSettingsPage";
            this.Size = new System.Drawing.Size(472, 332);
            this.serverPanel.ResumeLayout(false);
            this.serverPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.portUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tipPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel serverPanel;
        private System.Windows.Forms.Button browseDirButton;
        private System.Windows.Forms.Label serverHeading;
        private System.Windows.Forms.TextBox enginePathTextBox;
        private System.Windows.Forms.Label pathLabel;
        private System.Windows.Forms.NumericUpDown portUpDown;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label tipHeading;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel tipPanel;
        private System.Windows.Forms.Label publisherLabel;
        private System.Windows.Forms.Label engineLabel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
    }
}
