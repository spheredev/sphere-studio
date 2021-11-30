namespace SphereStudio.Forms
{
    partial class JsObjectViewer
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
            this.closeButton = new System.Windows.Forms.Button();
            this.propListTreeView = new System.Windows.Forms.TreeView();
            this.treeIconImageList = new System.Windows.Forms.ImageList(this.components);
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // closeButton
            // 
            this.closeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.closeButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.closeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.closeButton.Location = new System.Drawing.Point(485, 388);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(80, 25);
            this.closeButton.TabIndex = 2;
            this.closeButton.Text = "&Close";
            this.closeButton.UseVisualStyleBackColor = true;
            // 
            // propListTreeView
            // 
            this.propListTreeView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.propListTreeView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.propListTreeView.HideSelection = false;
            this.propListTreeView.HotTracking = true;
            this.propListTreeView.ImageIndex = 0;
            this.propListTreeView.ImageList = this.treeIconImageList;
            this.propListTreeView.Location = new System.Drawing.Point(12, 41);
            this.propListTreeView.Name = "propListTreeView";
            this.propListTreeView.SelectedImageIndex = 0;
            this.propListTreeView.Size = new System.Drawing.Size(553, 341);
            this.propListTreeView.TabIndex = 0;
            this.propListTreeView.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.PropTree_BeforeExpand);
            this.propListTreeView.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PropTree_MouseMove);
            // 
            // treeIconImageList
            // 
            this.treeIconImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.treeIconImageList.ImageSize = new System.Drawing.Size(16, 16);
            this.treeIconImageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // nameTextBox
            // 
            this.nameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nameTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nameTextBox.Location = new System.Drawing.Point(12, 12);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.ReadOnly = true;
            this.nameTextBox.Size = new System.Drawing.Size(553, 23);
            this.nameTextBox.TabIndex = 3;
            // 
            // ObjectViewer
            // 
            this.AcceptButton = this.closeButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.closeButton;
            this.ClientSize = new System.Drawing.Size(577, 425);
            this.Controls.Add(this.nameTextBox);
            this.Controls.Add(this.propListTreeView);
            this.Controls.Add(this.closeButton);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ObjectViewer";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "JavaScript Object Viewer";
            this.Load += new System.EventHandler(this.this_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.TreeView propListTreeView;
        private System.Windows.Forms.ImageList treeIconImageList;
        private System.Windows.Forms.TextBox nameTextBox;
    }
}