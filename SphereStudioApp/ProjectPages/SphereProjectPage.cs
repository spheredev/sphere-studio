using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

using SphereStudio.Base;

namespace SphereStudio.ProjectPages
{
    public partial class SphereProjectPage : UserControl, IStyleAware, IProjectPage
    {
        public SphereProjectPage()
        {
            InitializeComponent();
            StyleManager.AutoStyle(this);

            apiDropDown.Items.AddRange(new[]
            {
                "Sphere v1 - Sphere 1.x",
                "Sphere v2 - neoSphere, Oozaru",
            });
        }

        public string Compiler => Defaults.Compiler;

        public Control Control => this;

        public void ApplyStyle(UIStyle style)
        {
            style.AsUIElement(this);

            style.AsHeading(apiHeading);
            style.AsAccent(apiPanel);
            style.AsTextView(apiDropDown);
            style.AsTextView(levelEditBox);

            style.AsHeading(runtimeHeading);
            style.AsAccent(runtimePanel);
            style.AsTextView(scriptPathComboBox);
            style.AsTextView(resolutionDropDown);
            style.AsTextView(widthUpDown);
            style.AsTextView(heightUpDown);
            style.AsTextView(saveIdTextBox);

            style.AsHeading(compatHeading);
            style.AsAccent(compatPanel);
        }

        public void Populate(IProject project)
        {
            var apiVersion = project.Settings.GetInteger("apiVersion", 1);
            var apiLevel = project.Settings.GetInteger("apiLevel", 1);
            var mainPath = project.Settings.GetString("mainScript", "");
            var resolution = project.Settings.GetSize("resolution", new Size(320, 240));
            var saveId = project.Settings.GetString("saveID", "");
            var managingJson = project.Settings.GetBoolean("manageGameJson", false);

            apiVersion = Math.Min(Math.Max(apiVersion, 1), 2);
            apiLevel = Math.Min(Math.Max(apiLevel, 1), 999);
            apiDropDown.SelectedIndex = apiVersion - 1;
            levelEditBox.Value = apiLevel;
            scriptPathComboBox.Text = mainPath;
            saveIdTextBox.Text = saveId;

            var resoString = $"{resolution.Width}x{resolution.Height}";
            widthUpDown.Value = resolution.Width;
            heightUpDown.Value = resolution.Height;
            if (resolutionDropDown.FindStringExact(resoString) >= 0)
                resolutionDropDown.Text = resoString;
            else
                resolutionDropDown.SelectedIndex = 0;

            autoJsonCheckBox.Enabled = !project.GameOnly;
            autoJsonCheckBox.Checked = managingJson;
        }

        public void Save(ISettings settings)
        {
            settings.SetInteger("apiVersion", apiDropDown.SelectedIndex + 1);
            settings.SetInteger("apiLevel", (int)levelEditBox.Value);
            settings.SetSize("resolution",
                new Size((int)widthUpDown.Value, (int)heightUpDown.Value)) ;
            settings.SetString("mainScript", scriptPathComboBox.Text);
            settings.SetString("saveID", saveIdTextBox.Text);
            settings.SetValue("manageGameJson", autoJsonCheckBox.Checked);
        }

        public bool Verify()
        {
            return true;
        }

        private void apiDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            var apiVersion = apiDropDown.SelectedIndex + 1;
            levelEditBox.Visible = apiVersion >= 2;
            levelLabel.Visible = levelEditBox.Visible;
        }

        private void resoDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (resolutionDropDown.SelectedIndex > 0)
            {
                var match = new Regex(@"(\d+)x(\d+)").Match(resolutionDropDown.Text);
                widthUpDown.Value = int.Parse(match.Groups[1].Value);
                heightUpDown.Value = int.Parse(match.Groups[2].Value);
                widthUpDown.Enabled = false;
                heightUpDown.Enabled = false;
            }
            else
            {
                widthUpDown.Enabled = true;
                heightUpDown.Enabled = true;
                widthUpDown.Focus();
                widthUpDown.Select(0, widthUpDown.Text.Length);
            }
        }
    }
}
