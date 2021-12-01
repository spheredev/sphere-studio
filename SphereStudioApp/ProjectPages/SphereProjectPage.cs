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

            apiDropDown.Items.AddRange(new[] { "Sphere 1.x Compatible", "Sphere v2 API" });
        }

        public void ApplyStyle(UIStyle style)
        {
            style.AsUIElement(this);
            style.AsHeading(apiHeading);
            style.AsHeading(startupHeading);
            style.AsAccent(apiPanel);
            style.AsAccent(startupPanel);

            style.AsTextView(apiDropDown);
            style.AsTextView(levelEditBox);
            style.AsTextView(scriptPathEditBox);
            style.AsTextView(resoDropDown);
            style.AsTextView(widthEditBox);
            style.AsTextView(heightEditBox);
        }

        public void Populate(ISettings settings)
        {
            var apiVersion = settings.GetInteger("apiVersion", 1);
            var apiLevel = settings.GetInteger("apiLevel", 1);
            var mainPath = settings.GetString("mainScript", "");
            var resolution = settings.GetSize("resolution", new Size(320, 240));

            apiVersion = Math.Min(Math.Max(apiVersion, 1), 2);
            apiLevel = Math.Min(Math.Max(apiLevel, 1), 999);
            apiDropDown.SelectedIndex = apiVersion - 1;
            levelEditBox.Value = apiLevel;
            scriptPathEditBox.Text = mainPath;

            var resoString = $"{resolution.Width}x{resolution.Height}";
            if (resoDropDown.FindStringExact(resoString) >= 0)
            {
                resoDropDown.Text = resoString;
            }
            else
            {
                resoDropDown.SelectedIndex = 0;
                widthEditBox.Value = resolution.Width;
                heightEditBox.Value = resolution.Height;
            }
        }

        public void Save(ISettings settings)
        {
            settings.SetValue("apiVersion", apiDropDown.SelectedIndex + 1);
            settings.SetValue("apiLevel", (int)levelEditBox.Value);
            settings.SetValue("resolution", $"{widthEditBox.Value}x{heightEditBox.Value}");
            settings.SetValue("mainScript", scriptPathEditBox.Text);
        }

        public bool Verify()
        {
            return true;
        }

        private void apiDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            levelEditBox.Visible = apiDropDown.SelectedIndex > 0;
            levelLabel.Visible = levelEditBox.Visible;
        }

        private void resoDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (resoDropDown.SelectedIndex > 0)
            {
                var match = new Regex(@"(\d+)x(\d+)").Match(resoDropDown.Text);
                widthEditBox.Value = int.Parse(match.Groups[1].Value);
                heightEditBox.Value = int.Parse(match.Groups[2].Value);
                widthEditBox.Enabled = false;
                heightEditBox.Enabled = false;
            }
            else
            {
                widthEditBox.Enabled = true;
                heightEditBox.Enabled = true;
                widthEditBox.Focus();
                widthEditBox.Select(0, widthEditBox.Text.Length);
            }
        }
    }
}
