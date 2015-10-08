﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

using Sphere.Core.Editor;
using Sphere.Plugins;
using Sphere.Plugins.Interfaces;
using Sphere.Plugins.Views;

namespace SphereStudio.Plugins
{
    public class PluginMain : IPluginMain, INewFileOpener, IEditor<ScriptView>
    {
        public string Name { get { return "Script Editor"; } }
        public string Author { get { return "Spherical"; } }
        public string Description { get { return "Sphere Studio default script editor"; } }
        public string Version { get { return "1.2.0"; } }

        public string FileTypeName { get; private set; }
        public string[] FileExtensions { get; private set; }
        public Bitmap FileIcon { get; private set; }

        internal static List<String> Functions = new List<string>();

        public void Initialize(ISettings conf)
        {
            FileTypeName = "JS Script";
            FileExtensions = new[] { "js", "coffee" };
            FileIcon = Properties.Resources.script_edit;

            PluginManager.Register(this, this, Name);
            PluginManager.IDE.AddMenuItem(_rootMenu, "Tools");

            LoadFunctions();

            // check off the active settings in the menus
            _autoCompleteItem.Checked = PluginManager.IDE.Settings.GetBoolean("script-autocomplete", true);
            _codeFoldItem.Checked = PluginManager.IDE.Settings.GetBoolean("script-fold", true);
            _highlightLineItem.Checked = PluginManager.IDE.Settings.GetBoolean("script-hiline", true);
            _highlightBracesItem.Checked = PluginManager.IDE.Settings.GetBoolean("script-hibraces", true);
            _useTabsItem.Checked = PluginManager.IDE.Settings.GetBoolean("script-tabs", true);

            int spaces = PluginManager.IDE.Settings.GetInteger("script-spaces", 2);
            _twoUnitItem.Checked = spaces == 2;
            _fourUnitItem.Checked = spaces == 4;
            _eightUnitItem.Checked = spaces == 8;
        }

        public void ShutDown()
        {
            PluginManager.UnregisterAll(this);
            Functions.Clear();
        }

        public ScriptView CreateEditView()
        {
            return new ScriptEditView();
        }

        public DocumentView New()
        {
            DocumentView view = new ScriptEditView();
            return view.NewDocument() ? view : null;
        }

        public DocumentView Open(string fileName)
        {
            DocumentView view = new ScriptEditView();
            view.Load(fileName);
            return view;
        }

        private static void LoadFunctions()
        {
            FileInfo file = new FileInfo(Application.StartupPath + "/docs/functions.txt");
            if (!file.Exists) return;

            using (StreamReader reader = file.OpenText())
            {
                while (!reader.EndOfStream)
                    Functions.Add(reader.ReadLine());
            }
        }
        
        #region initialize the script menu
        private static ToolStripMenuItem _rootMenu, _indentMenu;
        private static ToolStripMenuItem _autoCompleteItem, _codeFoldItem;
        private static ToolStripMenuItem _highlightLineItem, _highlightBracesItem;
        private static ToolStripMenuItem _useTabsItem, _changeFontItem;
        private static ToolStripMenuItem _twoUnitItem, _fourUnitItem, _eightUnitItem;
        private static ToolStripItem _separator1, _separator2;

        internal static void ShowMenus(bool show)
        {
            _rootMenu.Visible = show;
        }

        static PluginMain()
        {
            _autoCompleteItem = new ToolStripMenuItem("Auto Complete") { CheckOnClick = true };
            _autoCompleteItem.Click += menuAutoComplete_Click;

            _codeFoldItem = new ToolStripMenuItem("Code Folding") { CheckOnClick = true };
            _codeFoldItem.Click += menuFolding_Click;

            _highlightLineItem = new ToolStripMenuItem("Highlight Current Line") { CheckOnClick = true };
            _highlightLineItem.Click += menuHighlightLine_Click;

            _highlightBracesItem = new ToolStripMenuItem("Highlight Braces") { CheckOnClick = true };
            _highlightBracesItem.Click += menuHighlightBraces_Click;

            _separator1 = new ToolStripSeparator();
            _separator2 = new ToolStripSeparator();

            _useTabsItem = new ToolStripMenuItem("Use Tabs") { CheckOnClick = true };
            _useTabsItem.Click += menuUseTabs_Click;

            _twoUnitItem = new ToolStripMenuItem("2 units");
            _twoUnitItem.Click += menuTabStop2_Click;

            _fourUnitItem = new ToolStripMenuItem("4 units");
            _fourUnitItem.Click += menuTabStop4_Click;

            _eightUnitItem = new ToolStripMenuItem("8 units");
            _eightUnitItem.Click += menuTabStop8_Click;

            _changeFontItem = new ToolStripMenuItem("Change Font...", Properties.Resources.style);
            _changeFontItem.Click += menuFont_Click;
            _changeFontItem.Enabled = false;

            _rootMenu = new ToolStripMenuItem("&Script");
            _rootMenu.DropDownItems.Add(_autoCompleteItem);
            _rootMenu.DropDownItems.Add(_codeFoldItem);
            _rootMenu.DropDownItems.Add(_highlightLineItem);
            _rootMenu.DropDownItems.Add(_highlightBracesItem);
            _rootMenu.DropDownItems.Add(_separator1);

            _indentMenu = new ToolStripMenuItem("Indentation");
            _indentMenu.DropDownItems.Add(_useTabsItem);
            _indentMenu.DropDown.Items.Add(_separator2);
            _indentMenu.DropDown.Items.Add(_twoUnitItem);
            _indentMenu.DropDown.Items.Add(_fourUnitItem);
            _indentMenu.DropDown.Items.Add(_eightUnitItem);

            _rootMenu.DropDownItems.Add(_indentMenu);
            _rootMenu.DropDownItems.Add(_changeFontItem);
            _rootMenu.Visible = false;
        }

        private static void UpdateScriptControls()
        {
            // restyle all script controls that changed.
            PluginManager.IDE.RestyleEditors();
        }

        private static void menuUseTabs_Click(object sender, EventArgs e)
        {
            PluginManager.IDE.Settings.SetValue("script-tabs", _useTabsItem.Checked);
            UpdateScriptControls();
        }

        private static void menuTabStop2_Click(object sender, EventArgs e)
        {
            _twoUnitItem.Checked = true;
            _fourUnitItem.Checked = _eightUnitItem.Checked = false;
            PluginManager.IDE.Settings.SetValue("script-spaces", 2);
            UpdateScriptControls();
        }

        private static void menuTabStop4_Click(object sender, EventArgs e)
        {
            _fourUnitItem.Checked = true;
            _twoUnitItem.Checked = _eightUnitItem.Checked = false;
            PluginManager.IDE.Settings.SetValue("script-spaces", 4);
            UpdateScriptControls();
        }

        private static void menuTabStop8_Click(object sender, EventArgs e)
        {
            _eightUnitItem.Checked = true;
            _twoUnitItem.Checked = _fourUnitItem.Checked = false;
            PluginManager.IDE.Settings.SetValue("script-spaces", 8);
            UpdateScriptControls();
        }

        private static void menuFont_Click(object sender, EventArgs e)
        {
            using (FontDialog diag = new FontDialog())
            {
                TypeConverter converter = TypeDescriptor.GetConverter(typeof(Font));
                string fontstring = PluginManager.IDE.Settings.GetString("script-font", "");

                if (!String.IsNullOrEmpty(fontstring))
                    diag.Font = (Font)converter.ConvertFromString(fontstring);

                try
                {
                    if (diag.ShowDialog() == DialogResult.OK)
                    {
                        fontstring = converter.ConvertToString(diag.Font);
                        PluginManager.IDE.Settings.SetValue("script-font", fontstring);
                        UpdateScriptControls();
                    }
                }
                catch (ArgumentException)
                {
                    MessageBox.Show(@"GDI+ only uses TrueType fonts.", @"Type Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private static void menuAutoComplete_Click(object sender, EventArgs e)
        {
            PluginManager.IDE.Settings.SetValue("script-autocomplete", _autoCompleteItem.Checked);
            UpdateScriptControls();
        }
        
        private static void menuFolding_Click(object sender, EventArgs e)
        {
            PluginManager.IDE.Settings.SetValue("script-fold", _codeFoldItem.Checked);
            UpdateScriptControls();
        }

        private static void menuHighlightBraces_Click(object sender, EventArgs e)
        {
            PluginManager.IDE.Settings.SetValue("script-hibraces", _highlightBracesItem.Checked);
            UpdateScriptControls();
        }
        
        private static void menuHighlightLine_Click(object sender, EventArgs e)
        {
            PluginManager.IDE.Settings.SetValue("script-hiline", _highlightLineItem.Checked);
            UpdateScriptControls();
        }
        #endregion
    }
}