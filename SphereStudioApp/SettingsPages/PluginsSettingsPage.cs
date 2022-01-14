using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

using SphereStudio.Base;
using SphereStudio.Core;
using SphereStudio.Forms;
using SphereStudio.IO;

namespace SphereStudio.SettingsPages
{
    public partial class PluginsSettingsPage : UserControl, ISettingsPage, IStyleAware
    {
        private bool updatingHandlers = false;
        private bool updatingPresets = false;
        private bool updatingPlugins = false;

        public PluginsSettingsPage()
        {
            InitializeComponent();
            StyleManager.AutoStyle(this);
        }

        public Control Control => this;
        public SettingsPageType Type => SettingsPageType.TopLevel;

        public void ApplyStyle(UIStyle style)
        {
            style.AsUIElement(this);

            style.AsTextView(presetDropDown);
            style.AsAccent(savePresetButton);
            style.AsAccent(deletePresetButton);

            style.AsHeading(defaultsHeading);
            style.AsAccent(defaultsPanel);
            style.AsTextView(engineComboBox);
            style.AsTextView(typeComboBox);
            style.AsTextView(otherComboBox);
            style.AsTextView(scriptComboBox);
            style.AsTextView(imageDropDown);
            style.AsTextView(presetDropDown);

            style.AsHeading(pluginsHeading);
            style.AsAccent(pluginsPanel);
            style.AsTextView(pluginsListView);
        }

        public void Populate()
        {
            updatePresets();
            updatePlugins();
            updateHandlers();
        }

        public void Save()
        {
            Session.Settings.Preset = presetDropDown.Text;
        }

        public bool Verify()
        {
            bool haveAllPlugins = PluginManager.Get<IStarter>(Session.Settings.Engine) != null
                && PluginManager.Get<ICompiler>(Session.Settings.Compiler) != null
                && PluginManager.Get<IFileOpener>(Session.Settings.FileOpener) != null
                && PluginManager.Get<IEditor<TextView>>(Session.Settings.TextEditor) != null
                && PluginManager.Get<IEditor<ImageView>>(Session.Settings.ImageEditor) != null;
            if (!haveAllPlugins)
            {
                DialogResult result = MessageBox.Show(
                    "You haven't selected plugins for one or more tasks. Continue?",
                    "No Handler Selected",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.No)
                    return false;
            }
            return true;
        }

        private string getPluginName(ComboBox comboBox)
        {
            return comboBox.SelectedIndex > 0 ? comboBox.Text : null;
        }

        private void handleSelectionChanged()
        {
            if (updatingHandlers)
                return;

            Session.Settings.Engine = getPluginName(engineComboBox);
            Session.Settings.Compiler = getPluginName(typeComboBox);
            Session.Settings.FileOpener = getPluginName(otherComboBox);
            Session.Settings.TextEditor = getPluginName(scriptComboBox);
            Session.Settings.ImageEditor = getPluginName(imageDropDown);
            Session.Settings.Apply();
            updatePresets();
            updateHandlers();
        }

        private void populateHandlers<T>(ComboBox comboBox, string currentName)
            where T : IPlugin
        {
            comboBox.Items.Clear();
            comboBox.Items.Add("(no handler selected)");
            foreach (string name in PluginManager.GetNames<T>())
                comboBox.Items.Add(name);
            if (comboBox.Items.Contains(currentName))
                comboBox.Text = currentName;
            else
                comboBox.SelectedIndex = 0;
        }

        private void updateHandlers()
        {
            if (updatingHandlers)
                return;

            updatingHandlers = true;

            populateHandlers<ICompiler>(typeComboBox, Session.Settings.Compiler);
            populateHandlers<IStarter>(engineComboBox, Session.Settings.Engine);
            populateHandlers<IEditor<TextView>>(otherComboBox, Session.Settings.FileOpener);
            populateHandlers<IEditor<TextView>>(scriptComboBox, Session.Settings.TextEditor);
            populateHandlers<IEditor<ImageView>>(imageDropDown, Session.Settings.ImageEditor);

            updatingHandlers = false;
        }

        private void updatePlugins()
        {
            if (updatingPlugins)
                return;

            updatingPlugins = true;

            pluginsListView.CreateGraphics();  // workaround for early ItemCheck event
            pluginsListView.Items.Clear();
            foreach (var pair in Session.Plugins)
            {
                ListViewItem item = new ListViewItem();
                item.Text = pair.Value.Main.Name;
                item.SubItems.Add(pair.Value.Main.Version);
                item.SubItems.Add(pair.Value.Main.Author);
                item.ToolTipText = pair.Value.Main.Description;
                item.Tag = pair.Key;
                item.Checked = !Session.Settings.DisabledPlugins.Contains(pair.Value.Handle);
                pluginsListView.Items.Add(item);
            }

            updatingPlugins = false;
        }

        private void updatePresets()
        {
            if (updatingPresets)
                return;

            updatingPresets = true;

            presetDropDown.Items.Clear();
            string presetPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "Sphere Studio", "pluginPresets");
            if (Directory.Exists(presetPath))
            {
                var presets = from filename in Directory.GetFiles(presetPath, "*.preset")
                              orderby filename ascending
                              select Path.GetFileNameWithoutExtension(filename);
                foreach (string name in presets)
                    presetDropDown.Items.Add(name);
            }

            if (presetDropDown.Items.Contains(Session.Settings.Preset ?? ""))
            {
                presetDropDown.Text = Session.Settings.Preset;
                deletePresetButton.Enabled = true;
            }
            else
            {
                presetDropDown.Items.Insert(0, "Custom Settings");
                presetDropDown.SelectedIndex = 0;
                deletePresetButton.Enabled = false;
            }

            updatingPresets = false;
        }

        private void presetDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (updatingPresets)
                return;

            Session.Settings.Preset = presetDropDown.Text;
            Session.Settings.Apply();
            updatePlugins();
            updateHandlers();
            updatePresets();
        }

        private void deletePresetButton_Click(object sender, EventArgs e)
        {
            string filename = $"{presetDropDown.Text}.preset";
            string path = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "Sphere Studio", "pluginPresets", filename);
            DialogResult result = MessageBox.Show(
                $"Are you sure you want to delete the preset file \"{filename}\"?",
                "Delete Preset", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                File.Delete(path);
                updatePresets();
            }
        }

        private void savePresetButton_Click(object sender, EventArgs e)
        {
            using (var diag = new SavePresetForm())
            {
                if (diag.ShowDialog() != DialogResult.OK)
                    return;
                string fileName = diag.PresetName + ".preset";
                string path = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                    "Sphere Studio", "pluginPresets", fileName);
                Directory.CreateDirectory(Path.GetDirectoryName(path));
                using (var preset = new IniFile(path, false))
                {
                    preset.SetValue("Preset", "compiler", getPluginName(typeComboBox));
                    preset.SetValue("Preset", "engine", getPluginName(engineComboBox));
                    preset.SetValue("Preset", "defaultFileOpener", getPluginName(otherComboBox));
                    preset.SetValue("Preset", "textEditor", getPluginName(scriptComboBox));
                    preset.SetValue("Preset", "imageEditor", getPluginName(imageDropDown));
                    preset.SetValue("Preset", "disabledPlugins", string.Join("|", Session.Settings.DisabledPlugins));
                    preset.Save();
                }
                Session.Settings.Preset = Path.GetFileNameWithoutExtension(fileName);
                Session.Settings.Apply();
                updatePresets();
            }
        }

        private void pluginComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            handleSelectionChanged();
        }

        private void pluginsListView_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (updatingPlugins)
                return;

            Session.Settings.DisabledPlugins = (from ListViewItem item in pluginsListView.Items
                                                where !item.Checked
                                                select item.Tag as string).ToArray();
            Session.Settings.Apply();
            updateHandlers();
            updatePresets();
            handleSelectionChanged();
        }
    }
}
