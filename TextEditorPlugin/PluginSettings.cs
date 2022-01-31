using SphereStudio.Base;

namespace SphereStudio
{
    class PluginSettings
    {
        private ISettings settings;

        public PluginSettings(ISettings settings)
        {
            this.settings = settings;
        }

        public bool AutoComplete
        {
            get => settings.GetBoolean("autoComplete", true);
            set => settings.SetValue("autoComplete", value);
        }

        public bool EnableCodeFolding
        {
            get => settings.GetBoolean("enableCodeFolding", true);
            set => settings.SetValue("enableCodeFolding", value);
        }

        public bool HighlightBraces
        {
            get => settings.GetBoolean("highlightBraces", true);
            set => settings.SetValue("highlightBraces", value);
        }

        public int IndentWidth
        {
            get => settings.GetInteger("indentWidth", 4);
            set => settings.SetInteger("indentWidth", value);
        }

        public bool PreferTabs
        {
            get => settings.GetBoolean("preferTabs", true);
            set => settings.SetValue("preferTabs", value);
        }

        public bool ShowCurrentLine
        {
            get => settings.GetBoolean("highlightCurrentLine", true);
            set => settings.SetValue("highlightCurrentLine", value);
        }

    }
}
