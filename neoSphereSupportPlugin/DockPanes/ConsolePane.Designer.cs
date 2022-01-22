namespace SphereStudio.DockPanes
{
    partial class ConsolePane
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConsolePane));
            this.uiTimer = new System.Windows.Forms.Timer(this.components);
            this.splitBox = new System.Windows.Forms.SplitContainer();
            this.logTextBox = new System.Windows.Forms.TextBox();
            this.errorListView = new System.Windows.Forms.ListView();
            this.columnValue = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnScript = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnLine = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.errorImageList = new System.Windows.Forms.ImageList(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitBox)).BeginInit();
            this.splitBox.Panel1.SuspendLayout();
            this.splitBox.Panel2.SuspendLayout();
            this.splitBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // uiTimer
            // 
            this.uiTimer.Interval = 250;
            this.uiTimer.Tick += new System.EventHandler(this.uiTimer_Tick);
            // 
            // splitBox
            // 
            this.splitBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitBox.Location = new System.Drawing.Point(0, 0);
            this.splitBox.Name = "splitBox";
            // 
            // splitBox.Panel1
            // 
            this.splitBox.Panel1.Controls.Add(this.logTextBox);
            // 
            // splitBox.Panel2
            // 
            this.splitBox.Panel2.Controls.Add(this.errorListView);
            this.splitBox.Size = new System.Drawing.Size(764, 221);
            this.splitBox.SplitterDistance = 385;
            this.splitBox.TabIndex = 1;
            // 
            // logTextBox
            // 
            this.logTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.logTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logTextBox.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.logTextBox.Location = new System.Drawing.Point(0, 0);
            this.logTextBox.Multiline = true;
            this.logTextBox.Name = "logTextBox";
            this.logTextBox.ReadOnly = true;
            this.logTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.logTextBox.Size = new System.Drawing.Size(385, 221);
            this.logTextBox.TabIndex = 1;
            this.logTextBox.WordWrap = false;
            // 
            // errorListView
            // 
            this.errorListView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.errorListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnValue,
            this.columnScript,
            this.columnLine});
            this.errorListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.errorListView.FullRowSelect = true;
            this.errorListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.errorListView.HideSelection = false;
            this.errorListView.Location = new System.Drawing.Point(0, 0);
            this.errorListView.Name = "errorListView";
            this.errorListView.Size = new System.Drawing.Size(375, 221);
            this.errorListView.SmallImageList = this.errorImageList;
            this.errorListView.TabIndex = 1;
            this.errorListView.UseCompatibleStateImageBehavior = false;
            this.errorListView.View = System.Windows.Forms.View.Details;
            this.errorListView.DoubleClick += new System.EventHandler(this.errorListView_DoubleClick);
            // 
            // columnValue
            // 
            this.columnValue.Text = "Exception";
            this.columnValue.Width = 400;
            // 
            // columnScript
            // 
            this.columnScript.Text = "Script";
            this.columnScript.Width = 200;
            // 
            // columnLine
            // 
            this.columnLine.Text = "Line";
            this.columnLine.Width = 50;
            // 
            // errorImageList
            // 
            this.errorImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("errorImageList.ImageStream")));
            this.errorImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.errorImageList.Images.SetKeyName(0, "check.png");
            this.errorImageList.Images.SetKeyName(1, "cross.png");
            // 
            // ConsolePane
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitBox);
            this.DoubleBuffered = true;
            this.Name = "ConsolePane";
            this.Size = new System.Drawing.Size(764, 221);
            this.splitBox.Panel1.ResumeLayout(false);
            this.splitBox.Panel1.PerformLayout();
            this.splitBox.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitBox)).EndInit();
            this.splitBox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer uiTimer;
        private System.Windows.Forms.SplitContainer splitBox;
        private System.Windows.Forms.TextBox logTextBox;
        private System.Windows.Forms.ListView errorListView;
        private System.Windows.Forms.ColumnHeader columnValue;
        private System.Windows.Forms.ColumnHeader columnScript;
        private System.Windows.Forms.ColumnHeader columnLine;
        private System.Windows.Forms.ImageList errorImageList;
    }
}
