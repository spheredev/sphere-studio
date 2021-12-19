using System;
using System.IO;
using System.Windows.Forms;
using Microsoft.Win32;

using SphereStudio.Base;
using SphereStudio.Compilers;
using SphereStudio.DockPanes;
using SphereStudio.Properties;
using SphereStudio.SettingsPages;
using SphereStudio.Starters;

namespace SphereStudio
{
    public class PluginMain : IPluginMain
    {
        public string Name => "neoSphere Support";
        public string Description => "Provides support for the neoSphere platform.";
        public string Version => Versioning.Version;
        public string Author => Versioning.Author;

        internal PluginConf Conf { get; private set; }

        private ToolStripMenuItem sphereApiRefCommand;
        private ToolStripMenuItem cellApiRefCommand;
        private ToolStripMenuItem runtimeApiRefCommand;

        public void Initialize(ISettings conf)
        {
            Conf = new PluginConf(conf);

            PluginManager.Register(this, new neoSphereStarter(this), "neoSphere");
            PluginManager.Register(this, new neoSphereStarter(this, true), "neoSphere R");
            PluginManager.Register(this, new CellCompiler(this), "Cell");
            PluginManager.Register(this, new neoSphereSettingsPage(this), "neoSphere");

            Panes.Initialize(this);

            sphereApiRefCommand = new ToolStripMenuItem("Sphere v2 Core API Reference", Resources.EvalIcon);
            runtimeApiRefCommand = new ToolStripMenuItem("Sphere Runtime API Reference", Resources.EvalIcon);
            cellApiRefCommand = new ToolStripMenuItem("Cellscript API Reference", Resources.EvalIcon);
            sphereApiRefCommand.Click += sphereApiRefCommand_Click;
            runtimeApiRefCommand.Click += miniRTApiRefCommand_Click;
            cellApiRefCommand.Click += cellApiRefCommand_Click;
            PluginManager.Core.AddMenuItem("Help", sphereApiRefCommand);
            PluginManager.Core.AddMenuItem("Help", runtimeApiRefCommand);
            PluginManager.Core.AddMenuItem("Help", cellApiRefCommand);

            PluginManager.Core.UnloadProject += on_UnloadProject;
        }

        public void ShutDown()
        {
            PluginManager.Core.UnloadProject -= on_UnloadProject;
            PluginManager.Core.RemoveMenuItem(sphereApiRefCommand);
            PluginManager.Core.RemoveMenuItem(runtimeApiRefCommand);
            PluginManager.Core.RemoveMenuItem(cellApiRefCommand);
            PluginManager.UnregisterAll(this);
        }

        private void sphereApiRefCommand_Click(object sender, EventArgs e)
        {
            string filePath = Path.Combine(Conf.EnginePath, "documentation", "sphere2-core-api.txt");
            PluginManager.Core.OpenFile(filePath);
        }

        private void miniRTApiRefCommand_Click(object sender, EventArgs e)
        {
            string filePath = Path.Combine(Conf.EnginePath, "documentation", "sphere2-hl-api.txt");
            PluginManager.Core.OpenFile(filePath);
        }

        private void cellApiRefCommand_Click(object sender, EventArgs e)
        {
            string filePath = Path.Combine(Conf.EnginePath, "documentation", "cellscript-api.txt");
            PluginManager.Core.OpenFile(filePath);
        }

        private void on_UnloadProject(object sender, EventArgs e)
        {
            Panes.Console.ClearErrors();
            Panes.Console.ClearConsole();
        }
    }

    class PluginConf
    {
        private ISettings settings;

        public PluginConf(ISettings settings)
        {
            this.settings = settings;
        }

        public string EnginePath
        {
            get
            {
                RegistryKey key = Registry.LocalMachine.OpenSubKey(
                    @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\{10C19C9F-1E29-45D8-A534-8FEF98C7C2FF}_is1");
                if (key != null)
                {
                    // Sphere is installed, get path from registry
                    string defaultPath = (string)key.GetValue("InstallLocation") ?? "";
                    string path = settings.GetString("enginePath", defaultPath);
                    return !string.IsNullOrWhiteSpace(path) ? path : defaultPath;
                }
                else
                {
                    // no installation key, just read from conf
                    return settings.GetString("enginePath", "");
                }
            }
            set
            {
                settings.SetValue("enginePath", value);
            }
        }

        public bool AlwaysUseConsole
        {
            get => settings.GetBoolean("alwaysUseConsole", false);
            set => settings.SetValue("alwaysUseConsole", value);
        }

        public bool MakeDebugPackages
        {
            get => settings.GetBoolean("makeDebugPackages", false);
            set => settings.SetValue("makeDebugPackages", value);
        }

        public bool ShowTraceInfo
        {
            get => settings.GetBoolean("showTraceOutput", false);
            set => settings.SetValue("showTraceOutput", value);
        }

        public bool TestInWindow
        {
            get => settings.GetBoolean("testInWindow", false);
            set => settings.SetValue("testInWindow", value);
        }

        public bool TestInRetroMode
        {
            get => settings.GetBoolean("debugInRetroMode", false);
            set => settings.SetValue("debugInRetroMode", value);
        }
        
        public int Verbosity
        {
            get => Math.Min(Math.Max(settings.GetInteger("verbosity", 0), 0), 4);
            set => settings.SetValue("verbosity", Math.Min(Math.Max(value, 0), 4));
        }
    }

    static class Panes
    {
        public static void Initialize(PluginMain main)
        {
            PluginManager.Register(main, Inspector = new InspectorPane(), "Inspector");
            PluginManager.Register(main, Console = new ConsolePane(main.Conf), "Debug Log");
        }

        public static ConsolePane Console { get; private set; }
        public static InspectorPane Inspector { get; private set; }
    }
}
