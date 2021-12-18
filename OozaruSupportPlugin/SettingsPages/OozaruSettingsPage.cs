using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using SphereStudio.Base;

namespace SphereStudio.SettingsPages
{
    public partial class OozaruSettingsPage : UserControl, IStyleAware, ISettingsPage
    {
        private ISettings settings;
        
        public OozaruSettingsPage(ISettings settings)
        {
            InitializeComponent();
            StyleManager.AutoStyle(this);

            this.settings = settings;
        }

        public Control Control => this;

        public void ApplyStyle(UIStyle style)
        {
            style.AsUIElement(this);

            style.AsHeading(directoryHeading);
            style.AsAccent(directoryPanel);
            style.AsTextView(enginePathTextBox);
            style.AsAccent(browseDirButton);
        }

        public void Populate()
        {
            enginePathTextBox.Text = settings.GetString("enginePath", string.Empty);
        }

        public void Save()
        {
            settings.SetString("enginePath", enginePathTextBox.Text);
        }
        
        public bool Verify()
        {
            return true;
        }

        private void browseDirButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fb = new FolderBrowserDialog()
            {
                Description = "Select a directory containing a compiled Oozaru distribution.",
                ShowNewFolderButton = false
            };
            if (fb.ShowDialog(this) == DialogResult.OK)
            {
                enginePathTextBox.Text = fb.SelectedPath;
            }
        }
    }
}
