namespace SphereStudio.Forms
{
    partial class PreferencesForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PreferencesForm));
            this.footer = new System.Windows.Forms.Panel();
            this.applyButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this.header = new System.Windows.Forms.Label();
            this.pagesTreeView = new System.Windows.Forms.TreeView();
            this.treeImageList = new System.Windows.Forms.ImageList(this.components);
            this.pagePanel = new System.Windows.Forms.Panel();
            this.footer.SuspendLayout();
            this.SuspendLayout();
            // 
            // footer
            // 
            this.footer.Controls.Add(this.applyButton);
            this.footer.Controls.Add(this.cancelButton);
            this.footer.Controls.Add(this.okButton);
            this.footer.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.footer.Location = new System.Drawing.Point(0, 494);
            this.footer.Name = "footer";
            this.footer.Size = new System.Drawing.Size(716, 50);
            this.footer.TabIndex = 3;
            // 
            // applyButton
            // 
            this.applyButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.applyButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.applyButton.Location = new System.Drawing.Point(624, 13);
            this.applyButton.Name = "applyButton";
            this.applyButton.Size = new System.Drawing.Size(80, 25);
            this.applyButton.TabIndex = 2;
            this.applyButton.Text = "&Apply";
            this.applyButton.UseVisualStyleBackColor = true;
            this.applyButton.Click += new System.EventHandler(this.applyButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cancelButton.Location = new System.Drawing.Point(538, 13);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(80, 25);
            this.cancelButton.TabIndex = 1;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.okButton.Location = new System.Drawing.Point(452, 13);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(80, 25);
            this.okButton.TabIndex = 0;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // header
            // 
            this.header.Dock = System.Windows.Forms.DockStyle.Top;
            this.header.Location = new System.Drawing.Point(0, 0);
            this.header.Name = "header";
            this.header.Size = new System.Drawing.Size(716, 23);
            this.header.TabIndex = 5;
            this.header.Text = "configure the Sphere Studio integrated development environment";
            this.header.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pagesTreeView
            // 
            this.pagesTreeView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.pagesTreeView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pagesTreeView.HideSelection = false;
            this.pagesTreeView.HotTracking = true;
            this.pagesTreeView.ImageIndex = 0;
            this.pagesTreeView.ImageList = this.treeImageList;
            this.pagesTreeView.Location = new System.Drawing.Point(12, 36);
            this.pagesTreeView.Name = "pagesTreeView";
            this.pagesTreeView.SelectedImageIndex = 0;
            this.pagesTreeView.Size = new System.Drawing.Size(146, 450);
            this.pagesTreeView.TabIndex = 6;
            this.pagesTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.pagesTreeView_AfterSelect);
            // 
            // treeImageList
            // 
            this.treeImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("treeImageList.ImageStream")));
            this.treeImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.treeImageList.Images.SetKeyName(0, "SettingsIcon.png");
            this.treeImageList.Images.SetKeyName(1, "OpenFolderIcon.png");
            this.treeImageList.Images.SetKeyName(2, "SphereIcon16.png");
            this.treeImageList.Images.SetKeyName(3, "CompilerIcon16.png");
            // 
            // pagePanel
            // 
            this.pagePanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pagePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pagePanel.Location = new System.Drawing.Point(164, 36);
            this.pagePanel.Name = "pagePanel";
            this.pagePanel.Size = new System.Drawing.Size(540, 450);
            this.pagePanel.TabIndex = 7;
            // 
            // PreferencesDialog
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(716, 544);
            this.Controls.Add(this.pagePanel);
            this.Controls.Add(this.pagesTreeView);
            this.Controls.Add(this.header);
            this.Controls.Add(this.footer);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PreferencesDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Preferences";
            this.footer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel footer;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button applyButton;
        private System.Windows.Forms.Label header;
        private System.Windows.Forms.TreeView pagesTreeView;
        private System.Windows.Forms.ImageList treeImageList;
        private System.Windows.Forms.Panel pagePanel;
    }
}