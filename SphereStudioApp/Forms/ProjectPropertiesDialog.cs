using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

using SphereStudio.Base;
using SphereStudio.Core;

namespace SphereStudio.Forms
{
    partial class ProjectPropertiesDialog : Form, IStyleAware
    {
        private Dictionary<IProjectPage, TabPage> pageMap = new Dictionary<IProjectPage, TabPage>();
        private Project project;

        public ProjectPropertiesDialog(Project project)
        {
            InitializeComponent();
            StyleManager.AutoStyle(this);

            var pageNames = PluginManager.GetNames<IProjectPage>();
            foreach (string name in pageNames)
            {
                var plugin = PluginManager.Get<IProjectPage>(name);
                plugin.Populate(project.Settings);
                var page = new TabPage(name) { Tag = plugin };
                page.Controls.Add(plugin.Control);
                plugin.Control.Dock = DockStyle.Fill;
                pageMap.Add(plugin, page);
            }

            this.project = project;
        }

        public void ApplyStyle(UIStyle style)
        {
            style.AsUIElement(this);
            style.AsUIElement(tabPage1);
            style.AsHeading(header);
            style.AsHeading(footer);
            style.AsAccent(okButton);
            style.AsAccent(cancelButton);
            style.AsAccent(upgradeButton);

            style.AsHeading(projectHeading);
            style.AsAccent(projectPanel);
            style.AsTextView(pathTextBox);
            style.AsTextView(typeDropDown);

            style.AsHeading(gameHeader);
            style.AsAccent(gamePanel);
            style.AsTextView(titleTextBox);
            style.AsTextView(authorTextBox);
            style.AsTextView(summaryTextBox);
        }

        protected override void OnLoad(EventArgs e)
        {
            typeDropDown.Items.AddRange(PluginManager.GetNames<ICompiler>());
            if (!typeDropDown.Items.Contains(project.Compiler))
                typeDropDown.Items.Insert(0, project.Compiler);

            pathTextBox.Text = Path.GetDirectoryName(project.FileName);
            titleTextBox.Text = project.Name;
            authorTextBox.Text = project.Author;
            summaryTextBox.Text = project.Summary;
            typeDropDown.Text = project.Compiler;

            repopulateTabs();

            typeDropDown.Enabled = !project.IsGameOnly;
            upgradeButton.Visible = project.IsGameOnly;

            ActiveControl = titleTextBox;

            base.OnLoad(e);
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            if (typeDropDown.Text != project.Compiler)
            {
                var answer = MessageBox.Show(
                    "You've changed the compiler for this project.  This may prevent Sphere Studio from building the project.  Are you sure you want to continue?",
                    "Changing Compiler", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (answer == DialogResult.No)
                {
                    DialogResult = DialogResult.None;
                    typeDropDown.Text = project.Compiler;
                    return;
                }
            }

            var compiler = typeDropDown.Text;
            var plugins = from name in PluginManager.GetNames<IProjectPage>()
                          let plugin = PluginManager.Get<IProjectPage>(name)
                          where compiler == plugin.Compiler || plugin.Compiler == null
                          select plugin;
            
            bool isValid = true;
            foreach (var plugin in plugins)
                isValid &= plugin.Verify();
            if (!isValid)
            {
                DialogResult = DialogResult.None;
                return;
            }
            
            foreach (var plugin in plugins)
                plugin.Save(project.Settings);

            project.Name = titleTextBox.Text;
            project.Author = authorTextBox.Text;
            project.Summary = summaryTextBox.Text;
            project.Compiler = typeDropDown.Text;
            project.Save();
        }

        private void upgradeButton_Click(object sender, EventArgs e)
        {
            var answer = MessageBox.Show(
                "This project was synthesized from a Sphere game manifest and has no associated project file.  To enable all Sphere Studio features, you can upgrade it to a full Sphere Studio project.  Do you want to upgrade now?",
                "Upgrade to Full Project", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (answer == DialogResult.Yes)
            {
                project.Upgrade();
                pathTextBox.Text = project.FileName;
                typeDropDown.Enabled = true;
                typeDropDown.Text = project.Compiler;
                upgradeButton.Visible = false;
            }
        }

        private void typeDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            repopulateTabs();
        }

        private void repopulateTabs()
        {
            var firstPage = tabControl.TabPages[0];
            var compiler = typeDropDown.Text;
            tabControl.TabPages.Clear();
            tabControl.TabPages.Add(firstPage);
            foreach (var entry in pageMap)
            {
                var plugin = entry.Key;
                var tabPage = entry.Value;
                if (compiler == plugin.Compiler || plugin.Compiler == null)
                    tabControl.TabPages.Add(tabPage);
            }
        }
    }
}
