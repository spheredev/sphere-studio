﻿using System;
using System.Windows.Forms;

using SphereStudio.Base;

namespace SphereStudio.SettingsPages
{
    partial class neoSphereSettingsPage : UserControl, ISettingsPage, IStyleAware
    {
        private PluginMain main;

        public neoSphereSettingsPage(PluginMain main)
        {
            InitializeComponent();
            StyleManager.AutoStyle(this);
            this.main = main;
        }

        public Control Control { get { return this; } }

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
            style.AsAccent(useSourceMapsButton);
            style.AsAccent(testInWindowButton);
            style.AsAccent(showTracesButton);
            style.AsTextView(logLevelDropDown);
            
            style.AsTextView(enginePathTextBox);
            style.AsTextView(logLevelDropDown);
            style.AsAccent(browseDirButton);
        }

        public bool Apply()
        {
            main.Conf.EnginePath = enginePathTextBox.Text;
            main.Conf.MakeDebugPackages = useSourceMapsButton.Checked;
            main.Conf.AlwaysUseConsole = testWithConsoleButton.Checked;
            main.Conf.ShowTraceInfo = showTracesButton.Checked;
            main.Conf.TestInWindow = testInWindowButton.Checked;
            main.Conf.Verbosity = logLevelDropDown.SelectedIndex;
            return true;
        }

        protected override void OnLoad(EventArgs e)
        {
            enginePathTextBox.Text = main.Conf.EnginePath;
            useSourceMapsButton.Checked = main.Conf.MakeDebugPackages;
            showTracesButton.Checked = main.Conf.ShowTraceInfo;
            testWithConsoleButton.Checked = main.Conf.AlwaysUseConsole;
            testInWindowButton.Checked = main.Conf.TestInWindow;
            logLevelDropDown.SelectedIndex = main.Conf.Verbosity;
            base.OnLoad(e);
        }

        private void BrowseButton_Click(object sender, EventArgs e)
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