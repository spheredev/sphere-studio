using System;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

using SphereStudio.Base;
using SphereStudio.Core;

namespace SphereStudio.Forms
{
    partial class NewProjectForm : Form, IStyleAware
    {
        private bool autoTitleMode = true;
        private string projectRoot;

        public NewProjectForm(string projectRootPath)
        {
            InitializeComponent();
            StyleManager.AutoStyle(this);

            projectRoot = projectRootPath;

            var baseName = "Sphere Game";
            var counter = 1;
            var nameOK = false;
            while (!nameOK)
            {
                var projectName = $"{baseName} {counter++}";
                var projectDirPath = Path.Combine(projectRoot, projectName);
                var dirInfo = new DirectoryInfo(projectDirPath);
                if (nameOK = !dirInfo.Exists)
                {
                    nameTextBox.Text = projectName;
                }
            }

            typeDropDown.Items.AddRange(PluginManager.GetNames<ICompiler>());
            typeDropDown.Text = Session.Settings.Compiler;

            authorTextBox.Text = Environment.UserName;
            resoDropDown.SelectedIndex = 1;
        }

        public Project Project { get; private set; }

        public void ApplyStyle(UIStyle style)
        {
            style.AsUIElement(this);
            style.AsHeading(header);

            style.AsHeading(projectHeading);
            style.AsAccent(projectPanel);
            style.AsTextView(nameTextBox);
            style.AsTextView(directoryTextBox);
            style.AsTextView(typeDropDown);

            style.AsHeading(gameHeading);
            style.AsAccent(gamePanel);
            style.AsTextView(titleTextBox);
            style.AsTextView(authorTextBox);
            style.AsTextView(summaryTextBox);
            style.AsTextView(resoDropDown);
            style.AsTextView(widthEditBox);
            style.AsTextView(heightEditBox);

            style.AsHeading(footer);
            style.AsUIElement(okButton);
            style.AsUIElement(cancelButton);
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            Project = Project.Create(directoryTextBox.Text, nameTextBox.Text);
            Project.Name = titleTextBox.Text;
            Project.Author = authorTextBox.Text;
            Project.Summary = summaryTextBox.Text;
            Project.Compiler = typeDropDown.Text;
            Project.Settings.SetValue("mainScript", "scripts/main.js");
            Project.Settings.SetSize("resolution",
                new Size((int)widthEditBox.Value, (int)heightEditBox.Value));
        }

        private void resoDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (resoDropDown.SelectedIndex > 0)
            {
                var match = new Regex(@"(\d+)x(\d+)").Match(resoDropDown.Text);
                widthEditBox.Value = int.Parse(match.Groups[1].Value);
                heightEditBox.Value = int.Parse(match.Groups[2].Value);
                widthEditBox.Enabled = false;
                heightEditBox.Enabled = false;
            }
            else
            {
                widthEditBox.Enabled = true;
                heightEditBox.Enabled = true;
                widthEditBox.Focus();
                widthEditBox.Select(0, widthEditBox.Text.Length);
            }
        }

        private void nameTextBox_TextChanged(object sender, EventArgs e)
        {
            if (autoTitleMode)
            {
                titleTextBox.Text = nameTextBox.Text;
                autoTitleMode = true;
            }
            directoryTextBox.Text = Path.Combine(projectRoot, nameTextBox.Text);
            validateForm();
        }

        private void titleTextBox_TextChanged(object sender, EventArgs e)
        {
            autoTitleMode = false;
        }

        private void resoTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && e.KeyChar != '\t' && e.KeyChar != '\b';
        }

        private void resoTextBox_TextChanged(object sender, EventArgs e)
        {
            validateForm();
        }

        private void validateForm()
        {
            okButton.Enabled = true;
            if (nameTextBox.Text.Length == 0)
                okButton.Enabled = false;

            var name = nameTextBox.Text;
            var invalidChars = Path.GetInvalidFileNameChars();
            bool isPathInvalid = false;
            foreach (char ch in invalidChars)
                isPathInvalid |= name.Contains(ch.ToString());
            if (isPathInvalid)
            {
                okButton.Enabled = false;
            }
            else
            {
                var directory = new DirectoryInfo(directoryTextBox.Text);
                if (directory.Exists && nameTextBox.Text.Length > 0)
                {
                    okButton.Enabled = false;
                }
            }
        }
    }
}
