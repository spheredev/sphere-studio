using System;
using System.IO;
using System.Windows.Forms;

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

        internal PluginSettings Settings { get; private set; }

        private ToolStripMenuItem sphereApiRefCommand;
        private ToolStripMenuItem cellApiRefCommand;
        private ToolStripMenuItem runtimeApiRefCommand;

        public void Initialize(ISettings settings)
        {
            Settings = new PluginSettings(settings);

            PluginManager.Register(this, new neoSphereStarter(this), "neoSphere");
            PluginManager.Register(this, new neoSphereStarter(this, true), "neoSphere R");
            PluginManager.Register(this, new CellCompiler(this), "Cell");
            PluginManager.Register(this, new neoSphereSettingsPage(Settings), "neoSphere");
            PluginManager.Register(this, new CellSettingsPage(Settings), "Cell");

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

            PluginManager.Core.UnloadProject += ide_UnloadProject;
        }

        public void ShutDown()
        {
            PluginManager.Core.UnloadProject -= ide_UnloadProject;
            PluginManager.Core.RemoveMenuItem(sphereApiRefCommand);
            PluginManager.Core.RemoveMenuItem(runtimeApiRefCommand);
            PluginManager.Core.RemoveMenuItem(cellApiRefCommand);
            PluginManager.UnregisterAll(this);
        }

        private void ide_UnloadProject(object sender, EventArgs e)
        {
            Panes.Console.ClearErrors();
            Panes.Console.ClearConsole();
        }

        private void sphereApiRefCommand_Click(object sender, EventArgs e)
        {
            string filePath = Path.Combine(Settings.EnginePath, "documentation", "sphere2-core-api.txt");
            PluginManager.Core.OpenFile(filePath);
        }

        private void miniRTApiRefCommand_Click(object sender, EventArgs e)
        {
            string filePath = Path.Combine(Settings.EnginePath, "documentation", "sphere2-hl-api.txt");
            PluginManager.Core.OpenFile(filePath);
        }

        private void cellApiRefCommand_Click(object sender, EventArgs e)
        {
            string filePath = Path.Combine(Settings.EnginePath, "documentation", "cellscript-api.txt");
            PluginManager.Core.OpenFile(filePath);
        }
    }

    static class Panes
    {
        public static void Initialize(PluginMain main)
        {
            PluginManager.Register(main, Inspector = new InspectorPane(), "Inspector");
            PluginManager.Register(main, Console = new ConsolePane(), "Debug Log");
        }

        public static ConsolePane Console { get; private set; }

        public static InspectorPane Inspector { get; private set; }
    }
}
