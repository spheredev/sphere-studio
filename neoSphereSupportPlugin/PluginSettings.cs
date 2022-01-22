using System;
using Microsoft.Win32;

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

        public bool AlwaysUseConsole
        {
            get => settings.GetBoolean("alwaysUseConsole", false);
            set => settings.SetValue("alwaysUseConsole", value);
        }

        public string EnginePath
        {
            get
            {
                var installInfoKey = Registry.LocalMachine
                    .OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\{10C19C9F-1E29-45D8-A534-8FEF98C7C2FF}_is1");
                var defaultPath = installInfoKey != null
                    ? (string)installInfoKey.GetValue("InstallLocation") ?? ""
                    : string.Empty;
                return settings.GetString("enginePath", defaultPath);
            }
            set
            {
                settings.SetString("enginePath", value);
            }
        }

        public bool MakeDebugPackages
        {
            get => settings.GetBoolean("makeDebugPackages", false);
            set => settings.SetValue("makeDebugPackages", value);
        }

        public bool ShowTraceLogs
        {
            get => settings.GetBoolean("showTraceOutput", false);
            set => settings.SetValue("showTraceOutput", value);
        }

        public bool TestInRetroMode
        {
            get => settings.GetBoolean("debugInRetroMode", false);
            set => settings.SetValue("debugInRetroMode", value);
        }

        public bool TestInWindow
        {
            get => settings.GetBoolean("testInWindow", false);
            set => settings.SetValue("testInWindow", value);
        }

        public int Verbosity
        {
            get => Math.Min(Math.Max(settings.GetInteger("verbosity", 0), 0), 4);
            set => settings.SetInteger("verbosity", Math.Min(Math.Max(value, 0), 4));
        }
    }
}
