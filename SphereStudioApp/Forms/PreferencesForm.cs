using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using SphereStudio.Base;
using SphereStudio.UI;

namespace SphereStudio.Forms
{
    partial class PreferencesForm : Form, IStyleAware
    {
        private List<ISettingsPage> applyList = new List<ISettingsPage>();
        private TreeNode currentNode;
        private ISettingsPage currentPage;
        private ISettingsPage firstPage;

        public PreferencesForm(string pageName = null)
        {
            InitializeComponent();
            StyleManager.AutoStyle(this);

            if (pageName != null)
                firstPage = PluginManager.Get<ISettingsPage>(pageName);
        }

        public void ApplyStyle(UIStyle style)
        {
            style.AsUIElement(this);
            style.AsHeading(header);
            style.AsHeading(footer);
            style.AsTextView(pagesTreeView);
            style.AsAccent(okButton);
            style.AsAccent(cancelButton);
            style.AsAccent(applyButton);
        }

        protected override void OnLoad(EventArgs e)
        {
            var pages = from name in PluginManager.GetNames<ISettingsPage>()
                        let plugin = PluginManager.Get<ISettingsPage>(name)
                        orderby plugin.Category, name
                        select (Name: name, Plugin: plugin);
            pagesTreeView.BeginUpdate();
            var engineNode = new TreeNode("Engines", 1, 1);
            var compilerNode = new TreeNode("Compilers", 1, 1);
            foreach (var page in pages)
            {
                TreeNode node = null;
                switch (page.Plugin.Category)
                {
                    case SettingsCategory.TopLevel:
                        node = new TreeNode(page.Name, 0, 0) { Tag = page.Plugin };
                        pagesTreeView.Nodes.Add(node);
                        break;
                    case SettingsCategory.Engine:
                        node = new TreeNode(page.Name, 2, 2) { Tag = page.Plugin };
                        engineNode.Nodes.Add(node);
                        break;
                    case SettingsCategory.Compiler:
                        node = new TreeNode(page.Name, 3, 3) { Tag = page.Plugin };
                        compilerNode.Nodes.Add(node);
                        break;
                }
                if (page.Plugin == firstPage)
                    currentNode = node;
            }
            if (engineNode.Nodes.Count > 0)
                pagesTreeView.Nodes.Add(engineNode);
            if (compilerNode.Nodes.Count > 0)
                pagesTreeView.Nodes.Add(compilerNode);
            pagesTreeView.ExpandAll();
            pagesTreeView.SelectedNode = currentNode ?? pagesTreeView.Nodes[0];
            pagesTreeView.EndUpdate();
            
            loadSettingsPage();

            base.OnLoad(e);
        }

        private void pagesTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            loadSettingsPage();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            bool canClose = true;
            foreach (ISettingsPage page in applyList)
                canClose &= page.Verify();
            if (canClose)
            {
                foreach (ISettingsPage page in applyList)
                    page.Save();
            }
            else
            {
                DialogResult = DialogResult.None;
            }
        }

        private void applyButton_Click(object sender, EventArgs e)
        {
            bool canSave = true;
            foreach (ISettingsPage page in applyList)
                canSave &= page.Verify();
            if (canSave)
            {
                foreach (ISettingsPage page in applyList)
                    page.Save();
            }
        }

        private void loadSettingsPage()
        {
            var settingsPage = pagesTreeView.SelectedNode.Tag as ISettingsPage;
            if (settingsPage != null && settingsPage != currentPage)
            {
                if (!applyList.Contains(settingsPage))
                {
                    settingsPage.Populate();
                    applyList.Add(settingsPage);
                }
                settingsPage.Control.Dock = DockStyle.Fill;
                pagePanel.Controls.Add(settingsPage.Control);
                if (currentPage != null)
                    currentPage.Control.Hide();
                settingsPage.Control.Show();
                currentPage = settingsPage;
                currentNode = pagesTreeView.SelectedNode;
            }
            else
            {
                if (currentNode.IsVisible)
                    pagesTreeView.SelectedNode = currentNode;
                else
                    pagesTreeView.SelectedNode = pagesTreeView.Nodes[0];
            }
        }
    }
}
