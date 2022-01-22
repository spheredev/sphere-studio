using System;
using System.Collections.Generic;
using System.Drawing;
using System.Media;
using System.Windows.Forms;

using SphereStudio.Base;
using SphereStudio.Debuggers;
using SphereStudio.Properties;

namespace SphereStudio.DockPanes
{
    partial class ConsolePane : UserControl, IDockPane, IStyleAware
    {
        private List<string> logLines = new List<string>();

        public ConsolePane()
        {
            InitializeComponent();
            StyleManager.AutoStyle(this);
        }

        public Control Control => this;

        public DockHint DockHint => DockHint.Bottom;

        public Bitmap DockIcon => Resources.ConsoleIcon;

        public bool ShowInViewMenu => true;

        public SsjDebugger Ssj { get; set; }

        public void AddError(string value, bool isFatal, string filename, int line)
        {
            if (errorListView.Items.Count > 0) {
                errorListView.Items[0].BackColor = errorListView.BackColor;
                errorListView.Items[0].ForeColor = errorListView.ForeColor;
            }
            var listViewItem = errorListView.Items.Insert(0, value, isFatal ? 1 : 0);
            listViewItem.SubItems.Add(filename);
            listViewItem.SubItems.Add(line.ToString());
            if (isFatal) {
                listViewItem.BackColor = Color.DarkRed;
                listViewItem.ForeColor = Color.Yellow;
            }
        }

        public void ApplyStyle(UIStyle style)
        {
            style.AsCodeView(logTextBox);
            style.AsTextView(errorListView);
        }

        public void ClearConsole()
        {
            logLines.Clear();
            uiTimer.Start();
        }

        public void ClearErrorHighlight()
        {
            if (errorListView.Items.Count > 0)
            {
                errorListView.Items[0].BackColor = errorListView.BackColor;
                errorListView.Items[0].ForeColor = errorListView.ForeColor;
            }
        }

        public void ClearErrors()
        {
            errorListView.Items.Clear();
        }

        public void HideIfClean()
        {
            ClearErrorHighlight();
            if (errorListView.Items.Count == 0) {
                PluginManager.Core.Docking.Hide(this);
            }
        }

        public void Print(string text)
        {
            logLines.AddRange(text.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None));
            uiTimer.Start();
        }

        private void errorListView_DoubleClick(object sender, EventArgs e)
        {
            if (errorListView.SelectedItems.Count > 0) {
                ListViewItem item = errorListView.SelectedItems[0];
                var filename = Ssj.ResolvePath(item.SubItems[1].Text);
                var lineNumber = int.Parse(item.SubItems[2].Text);
                var textView = PluginManager.Core.OpenFile(filename) as TextView;
                if (textView == null)
                    SystemSounds.Asterisk.Play();
                else
                    textView.GoToLine(lineNumber);
            }
        }

        private void uiTimer_Tick(object sender, EventArgs e)
        {
            uiTimer.Stop();
            logTextBox.Text = $"{string.Join("\r\n", logLines)}\r\n";
            logTextBox.SelectionStart = logTextBox.Text.Length;
            logTextBox.SelectionLength = 0;
            logTextBox.ScrollToCaret();
        }
    }
}
