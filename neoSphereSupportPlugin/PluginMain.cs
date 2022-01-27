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

        private PluginSettings settings;
        private ToolStripMenuItem sphereApiRefCommand;
        private ToolStripMenuItem cellApiRefCommand;
        private ToolStripMenuItem runtimeApiRefCommand;

        public void Initialize(ISettings settings)
        {
            this.settings = new PluginSettings(settings);

            PluginManager.Register(this, new neoSphereStarter(this.settings), "neoSphere");
            PluginManager.Register(this, new neoSphereStarter(this.settings, true), "neoSphere (Retrograde)");
            PluginManager.Register(this, new CellCompiler(this.settings), "Cell");
            PluginManager.Register(this, new neoSphereSettingsPage(this.settings), "neoSphere");
            PluginManager.Register(this, new CellSettingsPage(this.settings), "Cell");

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
            var filePath = Path.Combine(settings.EnginePath, "documentation", "sphere2-core-api.txt");
            PluginManager.Core.OpenFile(filePath);
        }

        private void miniRTApiRefCommand_Click(object sender, EventArgs e)
        {
            var filePath = Path.Combine(settings.EnginePath, "documentation", "sphere2-hl-api.txt");
            PluginManager.Core.OpenFile(filePath);
        }

        private void cellApiRefCommand_Click(object sender, EventArgs e)
        {
            var filePath = Path.Combine(settings.EnginePath, "documentation", "cellscript-api.txt");
            PluginManager.Core.OpenFile(filePath);
        }
    }

    static class Panes
    {
        public static void Initialize(PluginMain plugin)
        {
            PluginManager.Register(plugin, Inspector = new InspectorPane(), "Inspector");
            PluginManager.Register(plugin, Console = new ConsolePane(), "Debug Log");
        }

        public static ConsolePane Console { get; private set; }

        public static InspectorPane Inspector { get; private set; }
    }
}
