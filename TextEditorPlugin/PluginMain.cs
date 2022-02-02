using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

using SphereStudio.Base;
using SphereStudio.Editors;
using SphereStudio.FileOpeners;

namespace SphereStudio
{
    public class PluginMain : IPluginMain
    {
        public string Name => "Sphere Code & Text Editor";
        public string Description => "Code and text editor optimized for Sphere";
        public string Version => Versioning.Version;
        public string Author => Versioning.Author;

        private ToolStripMenuItem autoCompleteMenuItem;
        private ToolStripMenuItem enableCodeFoldingMenuItem;
        private ToolStripMenuItem highlightBracesMenuItem;
        private ToolStripMenuItem highlightCurrentLineMenuItem;
        private ToolStripMenuItem indentBy2MenuItem;
        private ToolStripMenuItem indentBy4MenuItem;
        private ToolStripMenuItem indentBy8MenuItem;
        private ToolStripMenuItem preferTabsMenuItem;
        private ToolStripMenuItem scriptMenu;

        internal List<string> Functions { get; private set; }

        internal PluginSettings Settings { get; private set; }

        public void Initialize(ISettings settings)
        {
            initializeAutoComplete();
            initializeMenuItems();
            Settings = new PluginSettings(settings);

            PluginManager.Register(this, new TextEditor(this), Name);
            PluginManager.Register(this, new TextFileOpener(this), Name);
            PluginManager.Core.AddMenuItem(scriptMenu, "Project");

            var indentWidth = Settings.IndentWidth;
            autoCompleteMenuItem.Checked = Settings.AutoComplete;
            enableCodeFoldingMenuItem.Checked = Settings.EnableCodeFolding;
            highlightBracesMenuItem.Checked = Settings.HighlightBraces;
            highlightCurrentLineMenuItem.Checked = Settings.ShowCurrentLine;
            indentBy2MenuItem.Checked = indentWidth == 2;
            indentBy4MenuItem.Checked = indentWidth == 4;
            indentBy8MenuItem.Checked = indentWidth == 8;
            preferTabsMenuItem.Checked = Settings.PreferTabs;
        }

        public void ShutDown()
        {
            PluginManager.UnregisterAll(this);
            Functions.Clear();
        }

        internal void ShowMenus(bool visible)
        {
            scriptMenu.Visible = visible;
        }

        private void initializeAutoComplete()
        {
            var dictionaryPath = Path.Combine(Application.StartupPath, @"Dictionary\AutoComplete.txt");
            if (File.Exists(dictionaryPath))
            {
                Functions = new List<string>();
                using (var reader = new StreamReader(dictionaryPath, true))
                {
                    while (!reader.EndOfStream)
                        Functions.Add(reader.ReadLine());
                }
            }
        }

        private void initializeMenuItems()
        {
            autoCompleteMenuItem = new ToolStripMenuItem("&Automatic Completion", null, autoCompleteMenuItem_Click) { CheckOnClick = true };
            enableCodeFoldingMenuItem = new ToolStripMenuItem("Enable Code &Folding", null, enableCodeFoldingMenuItem_Click) { CheckOnClick = true };
            highlightBracesMenuItem = new ToolStripMenuItem("Highlight &Braces", null, highlightBracesMenuItem_Click) { CheckOnClick = true };
            highlightCurrentLineMenuItem = new ToolStripMenuItem("Highlight Current &Line", null, highlightCurrentLineMenuItem_Click) { CheckOnClick = true };
            indentBy2MenuItem = new ToolStripMenuItem("Indent &2 Spaces", null, indentBy2MenuItem_Click);
            indentBy4MenuItem = new ToolStripMenuItem("Indent &4 Spaces", null, indentBy4MenuItem_Click);
            indentBy8MenuItem = new ToolStripMenuItem("Indent &8 Spaces", null, indentBy8MenuItem_Click);
            preferTabsMenuItem = new ToolStripMenuItem("Prefer &Tabs over Spaces", null, preferTabsMenuItem_Click) { CheckOnClick = true };
            scriptMenu = new ToolStripMenuItem("&Script") { Visible = false };
            scriptMenu.DropDownItems.Add(autoCompleteMenuItem);
            scriptMenu.DropDownItems.Add(enableCodeFoldingMenuItem);
            scriptMenu.DropDownItems.Add(highlightBracesMenuItem);
            scriptMenu.DropDownItems.Add(highlightCurrentLineMenuItem);
            scriptMenu.DropDownItems.Add(new ToolStripSeparator());
            scriptMenu.DropDownItems.Add(preferTabsMenuItem);
            scriptMenu.DropDown.Items.Add(new ToolStripSeparator());
            scriptMenu.DropDown.Items.Add(indentBy2MenuItem);
            scriptMenu.DropDown.Items.Add(indentBy4MenuItem);
            scriptMenu.DropDown.Items.Add(indentBy8MenuItem);
        }

        private void refreshDocuments()
        {
            StyleManager.Refresh();
        }

        private void autoCompleteMenuItem_Click(object sender, EventArgs e)
        {
            Settings.AutoComplete = autoCompleteMenuItem.Checked;
            refreshDocuments();
        }

        private void enableCodeFoldingMenuItem_Click(object sender, EventArgs e)
        {
            Settings.EnableCodeFolding = enableCodeFoldingMenuItem.Checked;
            refreshDocuments();
        }

        private void highlightBracesMenuItem_Click(object sender, EventArgs e)
        {
            Settings.HighlightBraces = highlightBracesMenuItem.Checked;
            refreshDocuments();
        }

        private void highlightCurrentLineMenuItem_Click(object sender, EventArgs e)
        {
            Settings.ShowCurrentLine = highlightCurrentLineMenuItem.Checked;
            refreshDocuments();
        }

        private void indentBy2MenuItem_Click(object sender, EventArgs e)
        {
            indentBy2MenuItem.Checked = true;
            indentBy4MenuItem.Checked = false;
            indentBy8MenuItem.Checked = false;
            Settings.IndentWidth = 2;
            refreshDocuments();
        }

        private void indentBy4MenuItem_Click(object sender, EventArgs e)
        {
            indentBy4MenuItem.Checked = true;
            indentBy2MenuItem.Checked = false;
            indentBy8MenuItem.Checked = false;
            Settings.IndentWidth = 4;
            refreshDocuments();
        }

        private void indentBy8MenuItem_Click(object sender, EventArgs e)
        {
            indentBy8MenuItem.Checked = true;
            indentBy2MenuItem.Checked = false;
            indentBy4MenuItem.Checked = false;
            Settings.IndentWidth = 8;
            refreshDocuments();
        }

        private void preferTabsMenuItem_Click(object sender, EventArgs e)
        {
            Settings.PreferTabs = preferTabsMenuItem.Checked;
            refreshDocuments();
        }
    }
}
