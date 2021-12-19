using System;
using System.IO;
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
            portUpDown.Value = settings.GetInteger("serverPort", 8080);
        }

        public void Save()
        {
            settings.SetString("enginePath", enginePathTextBox.Text);
            settings.SetInteger("serverPort", (int)portUpDown.Value);
        }

        public bool Verify()
        {
            var enginePath = enginePathTextBox.Text.Trim();
            var indexPath = Path.Combine(enginePathTextBox.Text, "index.html");
            if (enginePath != string.Empty && !File.Exists(indexPath))
            {
                var result = MessageBox.Show(
                    "The directory you selected doesn't seem to contain an Oozaru distribution.  Are you sure you want to use this directory?",
                    "Missing Oozaru Files",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Exclamation);
                if (result != DialogResult.Yes)
                    return false;
            }
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
