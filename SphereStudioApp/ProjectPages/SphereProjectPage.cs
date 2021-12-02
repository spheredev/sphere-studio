using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

using SphereStudio.Base;

namespace SphereStudio.ProjectPages
{
    public partial class SphereProjectPage : UserControl, IStyleAware, IProjectPage
    {
        public Control Control => this;
        public string Compiler => Defaults.Compiler;

        public SphereProjectPage()
        {
            InitializeComponent();
            StyleManager.AutoStyle(this);

            apiDropDown.Items.AddRange(new[]
            {
                "Sphere v1 (original Sphere 1.x API)",
                "Sphere v2 (neoSphere/Oozaru)",
            });
        }

        public void ApplyStyle(UIStyle style)
        {
            style.AsUIElement(this);
            style.AsHeading(apiHeading);
            style.AsHeading(runtimeHeading);
            style.AsAccent(apiPanel);
            style.AsAccent(runtimePanel);

            style.AsTextView(apiDropDown);
            style.AsTextView(levelEditBox);
            style.AsTextView(scriptPathComboBox);
            style.AsTextView(resolutionDropDown);
            style.AsTextView(widthUpDown);
            style.AsTextView(heightUpDown);
            style.AsTextView(saveIdTextBox);
        }

        public void Populate(ISettings settings)
        {
            var apiVersion = settings.GetInteger("apiVersion", 1);
            var apiLevel = settings.GetInteger("apiLevel", 1);
            var mainPath = settings.GetString("mainScript", "");
            var resolution = settings.GetSize("resolution", new Size(320, 240));
            var saveId = settings.GetString("saveID", "");

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
        }

        public void Save(ISettings settings)
        {
            settings.SetInteger("apiVersion", apiDropDown.SelectedIndex + 1);
            settings.SetInteger("apiLevel", (int)levelEditBox.Value);
            settings.SetSize("resolution",
                new Size((int)widthUpDown.Value, (int)heightUpDown.Value)) ;
            settings.SetString("mainScript", scriptPathComboBox.Text);
            settings.SetString("saveID", saveIdTextBox.Text);
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
