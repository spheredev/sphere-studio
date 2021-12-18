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
using SphereStudio.UI;

namespace SphereStudio.Forms
{
    partial class PreferencesDialog : Form, IStyleAware
    {
        private List<ISettingsPage> applyList = new List<ISettingsPage>();
        private Control currentPage = null;

        public PreferencesDialog()
        {
            InitializeComponent();
            StyleManager.AutoStyle(this);
        }

        public void ApplyStyle(UIStyle style)
        {
            style.AsUIElement(this);
            style.AsHeading(header);
            style.AsHeading(footer);
            style.AsAccent(tabControl);
            style.AsAccent(okButton);
            style.AsAccent(cancelButton);
            style.AsAccent(applyButton);
        }

        protected override void OnLoad(EventArgs e)
        {
            var pageNames = PluginManager.GetNames<ISettingsPage>();
            foreach (var name in pageNames)
            {
                var plugin = PluginManager.Get<ISettingsPage>(name);
                var page = new TabPage(name) { Tag = plugin };
                tabControl.TabPages.Add(page);
            }
            loadSettingsPage();
            base.OnLoad(e);
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
            foreach (ISettingsPage page in applyList)
                page.Save();
        }

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadSettingsPage();
        }

        private void loadSettingsPage()
        {
            var plugin = tabControl.SelectedTab.Tag as ISettingsPage;
            plugin.Populate();
            if (!applyList.Contains(plugin))
                applyList.Add(plugin);
            plugin.Control.Dock = DockStyle.Fill;
            tabControl.SelectedTab.Controls.Add(plugin.Control);
            if (currentPage != null)
                currentPage.Hide();
            plugin.Control.Show();
            currentPage = plugin.Control;
        }
    }
}
