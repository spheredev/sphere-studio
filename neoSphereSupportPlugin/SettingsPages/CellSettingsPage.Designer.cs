namespace SphereStudio.SettingsPages
{
    partial class CellSettingsPage
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
            this.tipPanel = new System.Windows.Forms.Panel();
            this.tipHeading = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.configPanel = new System.Windows.Forms.Panel();
            this.configHeading = new System.Windows.Forms.Label();
            this.debuggableSpkCheckBox = new System.Windows.Forms.CheckBox();
            this.tipPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.configPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // tipPanel
            // 
            this.tipPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tipPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tipPanel.Controls.Add(this.label1);
            this.tipPanel.Controls.Add(this.pictureBox1);
            this.tipPanel.Controls.Add(this.tipHeading);
            this.tipPanel.Location = new System.Drawing.Point(9, 12);
            this.tipPanel.Name = "tipPanel";
            this.tipPanel.Size = new System.Drawing.Size(526, 78);
            this.tipPanel.TabIndex = 6;
            // 
            // tipHeading
            // 
            this.tipHeading.Dock = System.Windows.Forms.DockStyle.Top;
            this.tipHeading.Location = new System.Drawing.Point(0, 0);
            this.tipHeading.Name = "tipHeading";
            this.tipHeading.Size = new System.Drawing.Size(524, 23);
            this.tipHeading.TabIndex = 0;
            this.tipHeading.Text = "if Cell fails to run...";
            this.tipHeading.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::SphereStudio.Properties.Resources.ConsoleIcon;
            this.pictureBox1.Location = new System.Drawing.Point(12, 34);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(16, 16);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Location = new System.Drawing.Point(35, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(478, 33);
            this.label1.TabIndex = 2;
            this.label1.Text = "Cell is part of a neoSphere installation. If the IDE cannot run the compiler, mak" +
    "e sure your neoSphere directory is set correctly under Engines -> neoSphere.";
            // 
            // configPanel
            // 
            this.configPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.configPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.configPanel.Controls.Add(this.debuggableSpkCheckBox);
            this.configPanel.Controls.Add(this.configHeading);
            this.configPanel.Location = new System.Drawing.Point(9, 96);
            this.configPanel.Name = "configPanel";
            this.configPanel.Size = new System.Drawing.Size(526, 61);
            this.configPanel.TabIndex = 7;
            // 
            // configHeading
            // 
            this.configHeading.Dock = System.Windows.Forms.DockStyle.Top;
            this.configHeading.Location = new System.Drawing.Point(0, 0);
            this.configHeading.Name = "configHeading";
            this.configHeading.Size = new System.Drawing.Size(524, 23);
            this.configHeading.TabIndex = 0;
            this.configHeading.Text = "Cell Configuration";
            this.configHeading.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // debuggableSpkCheckBox
            // 
            this.debuggableSpkCheckBox.AutoSize = true;
            this.debuggableSpkCheckBox.Location = new System.Drawing.Point(12, 32);
            this.debuggableSpkCheckBox.Name = "debuggableSpkCheckBox";
            this.debuggableSpkCheckBox.Size = new System.Drawing.Size(405, 17);
            this.debuggableSpkCheckBox.TabIndex = 1;
            this.debuggableSpkCheckBox.Text = "Include debugging information (source maps, etc.) when creating SPK packages";
            this.debuggableSpkCheckBox.UseVisualStyleBackColor = true;
            // 
            // CellSettingsPage
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.Controls.Add(this.configPanel);
            this.Controls.Add(this.tipPanel);
            this.Name = "CellSettingsPage";
            this.Size = new System.Drawing.Size(545, 402);
            this.tipPanel.ResumeLayout(false);
            this.tipPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.configPanel.ResumeLayout(false);
            this.configPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel tipPanel;
        private System.Windows.Forms.Label tipHeading;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel configPanel;
        private System.Windows.Forms.Label configHeading;
        private System.Windows.Forms.CheckBox debuggableSpkCheckBox;
    }
}
