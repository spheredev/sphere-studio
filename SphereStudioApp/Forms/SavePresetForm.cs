using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

using SphereStudio.Base;

namespace SphereStudio.Forms
{
    public partial class SavePresetForm : Form, IStyleAware
    {
        public SavePresetForm()
        {
            InitializeComponent();
            StyleManager.AutoStyle(this);
            
            refreshPresetBox();
            presetComboBox.SelectedIndex = 0;
        }

        public string PresetName { get; private set; }

        public void ApplyStyle(UIStyle style)
        {
            style.AsUIElement(this);
            style.AsHeading(header);
            style.AsHeading(footer);
            style.AsAccent(okButton);
            style.AsAccent(cancelButton);

            style.AsHeading(nameHeading);
            style.AsAccent(namePanel);
            style.AsTextView(presetComboBox);
            style.AsTextView(nameTextBox);
        }

        private void enterDefaultName()
        {
            var presetDirPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "Sphere Studio", "pluginPresets");
            var defaultName = "Untitled Preset";
            var ordinal = 1;
            var name = $"{defaultName} {ordinal}";
            while (File.Exists(Path.Combine(presetDirPath, $"{name}.preset")))
                name = $"{defaultName} {++ordinal}";
            nameTextBox.Text = name;
        }
        
        private void refreshPresetBox()
        {
            presetComboBox.Items.Clear();
            presetComboBox.Items.Add("new preset (enter name below)");
            var presetDirPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "Sphere Studio", "pluginPresets");
            if (Directory.Exists(presetDirPath))
            {
                var presetNames = from filename in Directory.GetFiles(presetDirPath, "*.preset")
                                  orderby filename ascending
                                  select Path.GetFileNameWithoutExtension(filename);
                foreach (var presetName in presetNames)
                    presetComboBox.Items.Add(presetName);
            }
        }

        private void nameTextBox_TextChanged(object sender, EventArgs e)
        {
            var regex = new Regex($"[{Regex.Escape(new string(Path.GetInvalidFileNameChars()))}]");
            okButton.Enabled = !regex.IsMatch(nameTextBox.Text);
        }

        private void presetComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (presetComboBox.SelectedIndex == 0)
            {
                enterDefaultName();
                nameTextBox.Enabled = true;
                nameTextBox.SelectAll();
                nameTextBox.Select();
            }
            else
            {
                nameTextBox.Enabled = false;
                nameTextBox.Text = presetComboBox.Text;
            }
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            var presetFilePath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "Sphere Studio", "pluginPresets", $"{nameTextBox.Text}.preset");
            var isSaveAllowed = true;
            if (File.Exists(presetFilePath))
            {
                var answer = MessageBox.Show(
                    $@"A configuration preset named ""{nameTextBox.Text}"" already exists. Do you want to overwrite the existing preset?",
                    "Preset Already Exists", MessageBoxButtons.YesNo);
                isSaveAllowed = answer == DialogResult.Yes;
            }
            if (isSaveAllowed)
            {
                PresetName = nameTextBox.Text;
                DialogResult = DialogResult.OK;
            }
        }
    }
}
