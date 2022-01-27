using System;
using System.Windows.Forms;
using Microsoft.Win32;

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
            var dialog = new FolderBrowserDialog();
            dialog.Description = "Select the directory where neoSphere is installed.";
            dialog.ShowNewFolderButton = false;
            if (dialog.ShowDialog(this) == DialogResult.OK)
                enginePathTextBox.Text = dialog.SelectedPath;
        }

        private void findEngineButton_Click(object sender, EventArgs e)
        {
            var installInfoKey = Registry.LocalMachine
                .OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\{10C19C9F-1E29-45D8-A534-8FEF98C7C2FF}_is1");
            var enginePath = installInfoKey != null
                ? (string)installInfoKey.GetValue("InstallLocation") ?? string.Empty
                : string.Empty;
            if (!string.IsNullOrEmpty(enginePath))
            {
                var engineName = (string)installInfoKey.GetValue("DisplayName") ?? "neoSphere";
                var version = (string)installInfoKey.GetValue("DisplayVersion") ?? string.Empty;
                var response = MessageBox.Show(
                    $"A system-wide installation of {engineName} {version} was found at {enginePath}. Do you want to use this version?",
                    "Found neoSphere Installation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (response == DialogResult.Yes)
                    enginePathTextBox.Text = enginePath;
            }
            else
            {
                MessageBox.Show(
                    "No neoSphere installation could be located on this machine. Check whether you actually have a copy of the engine installed.",
                    "Couldn't Locate Engine", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
