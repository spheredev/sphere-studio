using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

using SphereStudio.Base;

namespace SphereStudio.SettingsPages
{
    partial class Sphere1xSettingsPage : UserControl, ISettingsPage, IStyleAware
    {
        private ISettings conf;

        public Sphere1xSettingsPage(ISettings conf)
        {
            InitializeComponent();
            StyleManager.AutoStyle(this);

            this.conf = conf;
        }

        public SettingsCategory Category => SettingsCategory.Engine;

        public Control Control => this;

        public void ApplyStyle(UIStyle style)
        {
            style.AsUIElement(this);

            style.AsHeading(directoryHeading);
            style.AsAccent(directoryPanel);
            style.AsTextView(enginePathTextBox);
            style.AsAccent(configEngineButton);
            style.AsAccent(browseDirButton);
        }

        public void Populate()
        {
            enginePathTextBox.Text = conf.GetString("enginePath", "");
        }

        public void Save()
        {
            conf.SetValue("enginePath", enginePathTextBox.Text);
        }

        public bool Verify()
        {
            return true;
        }

        private void BrowseButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fb = new FolderBrowserDialog();
            fb.Description = "Where do you have classic Sphere installed?";
            fb.ShowNewFolderButton = false;
            if (fb.ShowDialog(this) == DialogResult.OK)
            {
                enginePathTextBox.Text = fb.SelectedPath;
            }
        }

        private void ConfigButton_Click(object sender, EventArgs e)
        {
            var configAppPath = Path.Combine(enginePathTextBox.Text, "config.exe");
            if (File.Exists(configAppPath))
            {
                Directory.SetCurrentDirectory(enginePathTextBox.Text);
                Process.Start(configAppPath);
                Directory.SetCurrentDirectory(Application.StartupPath);
            }
        }

        private void enginePathTextBox_TextChanged(object sender, EventArgs e)
        {
            configEngineButton.Enabled = File.Exists(Path.Combine(enginePathTextBox.Text, "config.exe"));
        }
    }
}
