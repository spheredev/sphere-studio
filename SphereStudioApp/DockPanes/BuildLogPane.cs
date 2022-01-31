using System;
using System.ComponentModel;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

using SphereStudio.Base;
using SphereStudio.Properties;

namespace SphereStudio.DockPanes
{
    [ToolboxItem(false)]
    partial class BuildLogPane : UserControl, IConsole, IDockPane, IStyleAware
    {
        private string logText = string.Empty;

        public BuildLogPane()
        {
            InitializeComponent();
            StyleManager.AutoStyle(this);
        }

        public Control Control => this;

        public DockHint DockHint => DockHint.Bottom;

        public Bitmap DockIcon => Resources.application_view_list;

        public bool ShowInViewMenu => true;

        public void ApplyStyle(UIStyle style)
        {
            style.AsCodeView(textBox);
        }

        public void Clear()
        {
            logText = string.Empty;
            PluginManager.Core.Invoke(new Action(() =>
            {
                uiTimer.Enabled = true;
            }), null);
        }

        public void Print(string lineText)
        {
            logText += Regex.Replace(lineText, "\r?\n", "\r\n");
            PluginManager.Core.Invoke(new Action(() =>
            {
                uiTimer.Enabled = true;
            }), null);
        }

        private void uiTimer_Tick(object sender, EventArgs e)
        {
            uiTimer.Enabled = false;
            textBox.Text = logText;
            textBox.SelectionStart = textBox.Text.Length;
            textBox.SelectionLength = 0;
            textBox.ScrollToCaret();
        }
    }
}
