using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

using SphereStudio.Base;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SphereStudio.SettingsPages
{
    public partial class OozaruSettingsPage : UserControl, IStyleAware, ISettingsPage
    {
        private ISettings settings;

        public OozaruSettingsPage(ISettings settings)
        {
            InitializeComponent();
            StyleManager.AutoStyle(this);

            this.settings = settings;
        }

        public SettingsCategory Category => SettingsCategory.Engine;

        public Control Control => this;

        public void ApplyStyle(UIStyle style)
        {
            style.AsUIElement(this);

            style.AsHeading(tipHeading);
            style.AsAccent(tipPanel);
            
            style.AsHeading(serverHeading);
            style.AsAccent(serverPanel);
            style.AsTextView(enginePathTextBox);
            style.AsAccent(browseDirButton);
            style.AsTextView(portUpDown);
        }

        public void Populate()
        {
            enginePathTextBox.Text = settings.GetString("enginePath", string.Empty);
            portUpDown.Value = settings.GetInteger("serverPort", 8080);
        }

        public void Save()
        {
            settings.SetString("enginePath", enginePathTextBox.Text);
            settings.SetInteger("serverPort", (int)portUpDown.Value);
        }

        public bool Verify()
        {
            var enginePath = enginePathTextBox.Text.Trim();
            var jsonPath = Path.Combine(enginePathTextBox.Text, "oozaru.json");
            if (enginePath != string.Empty && !File.Exists(jsonPath))
            {
                var result = MessageBox.Show(
                    "The directory you selected doesn't seem to contain an Oozaru distribution.  Are you sure you want to use this directory?",
                    "Missing Oozaru Files",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Exclamation);
                if (result != DialogResult.Yes)
                {
                    enginePathTextBox.Focus();
                    enginePathTextBox.SelectAll();
                    return false;
                }
            }
            return true;
        }

        private void enginePathTextBox_TextChanged(object sender, EventArgs e)
        {
            var enginePath = enginePathTextBox.Text.Trim();
            var jsonPath = Path.Combine(enginePathTextBox.Text, "oozaru.json");
            try
            {
                var jsonText = File.ReadAllText(jsonPath, Encoding.UTF8);
                var jsonData = JsonConvert.DeserializeObject<JObject>(jsonText);
                var engineName = (string)jsonData["name"];
                var author = jsonData.ContainsKey("publisher") ? (string)jsonData["publisher"] : "Unknown";
                var version = jsonData.ContainsKey("version") ? (string)jsonData["version"] : string.Empty;
                engineLabel.Text = $"{engineName} {version}";
                publisherLabel.Text = author;
            }
            catch
            {
                engineLabel.Text = "No Oozaru distribution could be found there.";
                publisherLabel.Text = "N/A";
            }
        }

        private void browseDirButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fb = new FolderBrowserDialog()
            {
                Description = "Select a directory containing a compiled Oozaru engine instance.",
                ShowNewFolderButton = false
            };
            if (fb.ShowDialog(this) == DialogResult.OK)
            {
                enginePathTextBox.Text = fb.SelectedPath;
            }
        }
    }
}
