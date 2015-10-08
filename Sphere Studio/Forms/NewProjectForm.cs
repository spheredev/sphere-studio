﻿using System;
using System.IO;
using System.Windows.Forms;

using SphereStudio.IDE;
using SphereStudio.Settings;
using Sphere.Core.Editor;

namespace SphereStudio.Forms
{
    internal partial class NewProjectForm : Form, IStyleable
    {
        public string RootFolder
        {
            get { return FolderBox.Text; }
            set
            {
                FolderBox.Text = value;
                DirectoryBox.Text = value + @"//";
            }
        }

        public NewProjectForm()
        {
            InitializeComponent();
            UpdateStyle();
        }

        private void FillDirectory(object sender, KeyEventArgs e)
        {
            DirectoryBox.Text = FolderBox.Text + @"\" + NameBox.Text;
            CheckForOk();
        }

        private void FolderBrowseButton_Click(object sender, EventArgs e)
        {
            if (FolderFinder.ShowDialog() == DialogResult.OK)
            {
                FolderBox.Text = FolderFinder.SelectedPath;
                DirectoryBox.Text = FolderBox.Text + @"\" + NameBox.Text;
                CheckForOk();
            }
        }
        
        private void TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (!Char.IsDigit(e.KeyChar) && e.KeyChar != (char)8);
        }

        private void ResoComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (ResoComboBox.SelectedIndex)
            {
                case 0: WidthBox.Text = @"320"; HeightBox.Text = @"240";
                    break;
                case 1: WidthBox.Text = @"640"; HeightBox.Text = @"480";
                    break;
                case 2: WidthBox.Text = @"800"; HeightBox.Text = @"600";
                    break;
                case 3: WidthBox.Text = @"1024"; HeightBox.Text = @"768";
                    break;
            }
        }

        private void CheckForOk()
        {
            OKButton.Enabled = true;
            StatusLabel.Text = @"Ready.";
            String text = NameBox.Text;
            String path = DirectoryBox.Text;
            if (text.Length == 0)
            {
                OKButton.Enabled = false;
                StatusLabel.Text = FolderBox.Text.Length < 2 ? "You'll need a name and directory." : "You'll need a name.";
            }
            else if (FolderBox.Text.Length < 2)
            {
                OKButton.Enabled = false;
                StatusLabel.Text = @"The directory must be more than 2 letters.";
            }

            if (path.Contains("|") || text.Contains("\\") || path.Contains("?") || path.Contains("<") ||
                path.Contains("/") || path.Contains("\"") || path.Contains("*") || path.Contains(">") ||
                path.LastIndexOf(':') > 1 || path.Contains("\'"))
            {
                OKButton.Enabled = false;
                StatusLabel.Text = @"Path or name contains invalid characters.";
            }
            else
            {
                System.IO.DirectoryInfo directory = new System.IO.DirectoryInfo(DirectoryBox.Text);
                if (directory.Exists && text.Length > 0)
                {
                    OKButton.Enabled = false;
                    StatusLabel.Text = @"Project Name Already Exists!";
                }
            }
        }

        public void UpdateStyle()
        {
            StyleSettings.ApplyStyle(ButtonPanel);
            StyleSettings.ApplyStyle(OKButton);
            StyleSettings.ApplyStyle(cancelButton);
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            Project project = Project.Create(DirectoryBox.Text, NameBox.Text);
            project.Author = AuthorBox.Text;
            project.Description = DescriptionBox.Text;
            project.ScreenWidth = int.Parse(WidthBox.Text);
            project.ScreenHeight = int.Parse(HeightBox.Text);
            project.MainScript = "main.js";
            project.Save();

            // automatically create the starting script //
            using (StreamWriter startscript = new StreamWriter(File.Open(project.RootPath + "\\scripts\\main.js", FileMode.CreateNew)))
            {
                const string header = "/**\n* Script: main.js\n* Written by: {0}\n* Updated: {1}\n**/\n\nfunction game()\n{{\n\t\n}}";
                startscript.Write(string.Format(header, project.Author, DateTime.Today.ToShortDateString()));
                startscript.Close();
            }

            Global.Project = project;
        }
    }
}
