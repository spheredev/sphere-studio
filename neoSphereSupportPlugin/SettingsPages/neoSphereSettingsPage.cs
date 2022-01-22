using System;
using System.Windows.Forms;

using SphereStudio.Base;

namespace SphereStudio.SettingsPages
{
    partial class neoSphereSettingsPage : UserControl, ISettingsPage, IStyleAware
    {
        private PluginSettings settings;

        public neoSphereSettingsPage(PluginSettings settings)
        {
            InitializeComponent();
            StyleManager.AutoStyle(this);

            this.settings = settings;
        }

        public SettingsCategory Category => SettingsCategory.Engine;

        public Control Control => this;

        public void ApplyStyle(UIStyle style)
        {
            style.AsUIElement(this);

            style.AsHeading(directoryHeading);
            style.AsAccent(directoryPanel);
            style.AsTextView(enginePathTextBox);
            style.AsAccent(browseDirButton);
            
            style.AsHeading(debugHeading);
            style.AsAccent(debugPanel);
            style.AsAccent(testWithConsoleButton);
            style.AsAccent(testInWindowButton);
            style.AsAccent(showTracesButton);
            style.AsAccent(retroModeCheckBox);
            style.AsTextView(logLevelDropDown);
            
            style.AsTextView(enginePathTextBox);
            style.AsTextView(logLevelDropDown);
            style.AsAccent(browseDirButton);
        }

        public void Populate()
        {
            enginePathTextBox.Text = settings.EnginePath;
            showTracesButton.Checked = settings.ShowTraceLogs;
            testWithConsoleButton.Checked = settings.AlwaysUseConsole;
            testInWindowButton.Checked = settings.TestInWindow;
            logLevelDropDown.SelectedIndex = settings.Verbosity;
            retroModeCheckBox.Checked = settings.TestInRetroMode;
        }

        public void Save()
        {
            settings.EnginePath = enginePathTextBox.Text;
            settings.AlwaysUseConsole = testWithConsoleButton.Checked;
            settings.ShowTraceLogs = showTracesButton.Checked;
            settings.TestInWindow = testInWindowButton.Checked;
            settings.Verbosity = logLevelDropDown.SelectedIndex;
            settings.TestInRetroMode = retroModeCheckBox.Checked;
        }

        public bool Verify()
        {
            return true;
        }

        private void browseDirButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fb = new FolderBrowserDialog();
            fb.Description = "Select the directory where neoSphere is installed.";
            fb.ShowNewFolderButton = false;
            if (fb.ShowDialog(this) == DialogResult.OK)
            {
                enginePathTextBox.Text = fb.SelectedPath;
            }
        }
    }
}
