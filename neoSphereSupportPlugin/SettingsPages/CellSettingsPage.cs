using System.Windows.Forms;

using SphereStudio.Base;

namespace SphereStudio.SettingsPages
{
    partial class CellSettingsPage : UserControl, ISettingsPage, IStyleAware
    {
        private PluginSettings settings;

        public CellSettingsPage(PluginSettings settings)
        {
            InitializeComponent();
            StyleManager.AutoStyle(this);

            this.settings = settings;
        }

        public SettingsCategory Category => SettingsCategory.Compiler;

        public Control Control => this;

        public void ApplyStyle(UIStyle style)
        {
            style.AsUIElement(this);
            
            style.AsHeading(tipHeading);
            style.AsAccent(tipPanel);

            style.AsHeading(configHeading);
            style.AsAccent(configPanel);
            style.AsAccent(debuggableSpkCheckBox);
        }

        public void Populate()
        {
            debuggableSpkCheckBox.Checked = settings.MakeDebugPackages;
        }

        public void Save()
        {
            settings.MakeDebugPackages = debuggableSpkCheckBox.Checked;
        }

        public bool Verify()
        {
            return true;
        }
    }
}
