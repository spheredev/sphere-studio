using System;
using System.IO;
using System.Windows.Forms;
using System.Linq;

using SphereStudio.Base;
using SphereStudio.IO;

namespace SphereStudio.Core
{
    class CoreSettings : IniSettings, ICoreSettings
    {
        public CoreSettings(IniFile iniFile) :
            base(iniFile, "Sphere Studio")
        {
            Preset = GetString("preset", "");
        }

        public string[] AutoHidePanes
        {
            get => GetStringArray("autoHidePanes", new string[0]);
            set => SetValue("autoHidePanes", value);
        }

        public bool AutoOpenLastProject
        {
            get => GetBoolean("autoOpenProject", false);
            set => SetValue("autoOpenProject", value);
        }

        public string Compiler
        {
            get
            {
                return GetString("defaultCompiler", "");
            }
            set
            {
                SetValue("defaultCompiler", value);
                Preset = null;
            }
        }

        public string[] DisabledPlugins
        {
            get
            {
                return GetStringArray("disabledPlugins", new string[0]);
            }
            set
            {
                SetValue("disabledPlugins", value);
                Preset = "";
            }
        }

        public string Engine
        {
            get
            {
                return GetString("defaultEngine", string.Empty);
            }
            set
            {
                SetValue("defaultEngine", value);
                Preset = null;
            }
        }

        public string FileOpener
        {
            get
            {
                return GetString("defaultFileOpener", "");
            }
            set
            {
                SetValue("defaultFileOpener", value);
                Preset = null;
            }
        }

        public string[] HiddenPanes
        {
            get => GetStringArray("hiddenPanes", new string[0]);
            set => SetValue("hiddenPanes", value);
        }

        public string ImageEditor
        {
            get
            {
                return GetString("imageEditor", "");
            }
            set
            {
                SetValue("imageEditor", value);
                Preset = null;
            }
        }

        public string LastProjectFileName
        {
            get => GetString("lastProject", "");
            set => SetValue("lastProject", value);
        }

        public string Preset
        {
            get
            {
                var value = GetString("preset", string.Empty);
                return string.IsNullOrWhiteSpace(value) ? null : value;
            }
            set
            {
                var path = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                    "Sphere Studio", "pluginPresets", $"{value}.preset");
                if (!string.IsNullOrWhiteSpace(value) && File.Exists(path)) {
                    using (var presetFile = new IniFile(path, false)) {
                        Compiler = presetFile.GetValue("Preset", "compiler", string.Empty);
                        Engine = presetFile.GetValue("Preset", "engine", string.Empty);
                        FileOpener = presetFile.GetValue("Preset", "defaultFileOpener", string.Empty);
                        ImageEditor = presetFile.GetValue("Preset", "imageEditor", string.Empty);
                        TextEditor = presetFile.GetValue("Preset", "textEditor", string.Empty);
                        DisabledPlugins = presetFile.GetValue("Preset", "disabledPlugins", string.Empty)
                            .Split('|');
                    }
                    SetString("preset", value);
                }
                else {
                    SetString("preset", string.Empty);
                }
            }
        }

        public string[] ProjectPaths
        {
            get => GetStringArray("gamePaths", new string[0]);
            set => SetValue("gamePaths", value);
        }

        public View StartPageView
        {
            get
            {
                var value = GetString("startPageView", "Details");
                return (View)Enum.Parse(typeof(View), value);
            }
            set
            {
                SetValue("startPageView", value);
            }
        }

        public string StyleName
        {
            get => GetString("uiStyle", Defaults.Style);
            set => SetString("uiStyle", value);
        }

        public string TextEditor
        {
            get
            {
                return GetString("textEditor", string.Empty);
            }
            set
            {
                SetString("textEditor", value);
                Preset = null;
            }
        }

        public UIStyle UIStyle
        {
            get
            {
                var styles = from name in PluginManager.GetNames<IStyleProvider>()
                             let plugin = PluginManager.Get<IStyleProvider>(name)
                             from style in plugin.Styles
                             select new {
                                 Name = $"{name}: {style.Name}",
                                 Style = style
                             };
                var uiStyle = styles
                    .Where(it => it.Name == StyleName)
                    .Select(it => it.Style)
                    .FirstOrDefault();
                if (uiStyle == null)
                {
                    uiStyle = styles
                        .Where(it => it.Name == Defaults.Style)
                        .Select(it => it.Style)
                        .FirstOrDefault();
                }
                return uiStyle;
            }
        }

        public bool UseStartPage
        {
            get => GetBoolean("autoStartPage", true);
            set => SetValue("autoStartPage", value);
        }

        public void Apply()
        {
            foreach (var entry in Session.Plugins)
                entry.Value.Enabled = !DisabledPlugins.Contains(entry.Key);
            PluginManager.Core.Docking.Refresh();
            if (UIStyle != null && StyleManager.Style != UIStyle)
                StyleManager.Style = UIStyle;
        }
    }
}
