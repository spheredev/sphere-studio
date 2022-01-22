using System;
using System.Diagnostics;
using System.IO;

using SphereStudio.Base;
using SphereStudio.Debuggers;

namespace SphereStudio.Starters
{
    class neoSphereStarter : IDebugStarter
    {
        private PluginMain plugin;
        private bool useRetroMode;

        public neoSphereStarter(PluginMain plugin, bool useRetroMode = false)
        {
            this.plugin = plugin;
            this.useRetroMode = useRetroMode;
        }

        public bool CanConfigure => false;

        public void Configure()
        {
            throw new NotSupportedException("neoSphere doesn't support engine configuration.");
        }

        public IDebugger Debug(string gamePath, bool isPackage, IProject project)
        {
            PluginManager.Core.Docking.Activate(Panes.Console);
            Panes.Console.ClearConsole();
            PluginManager.Core.Docking.Show(Panes.Inspector);
            var enginePath = Path.Combine(plugin.Settings.EnginePath, "spherun.exe");
            var options = string.Format(@"--verbose {0} --debug {1} ""{2}""",
                plugin.Settings.Verbosity,
                useRetroMode ? "--retro" : "",
                gamePath);
            var engineProcess = Process.Start(enginePath, options);
            return new SsjDebugger(plugin, enginePath, engineProcess, project);
        }

        public void Start(string gamePath, bool isPackage)
        {
            var wantConsole = plugin.Settings.AlwaysUseConsole;
            var wantWindow = plugin.Settings.TestInWindow || wantConsole;
            var enginePath = Path.Combine(plugin.Settings.EnginePath,
                wantConsole ? "spherun.exe" : "neoSphere.exe");
            var options = string.Format(@"{0} --verbose {1} {2} {3} ""{4}""",
                wantWindow ? "--windowed" : string.Empty,
                plugin.Settings.Verbosity,
                wantConsole ? "--profile" : string.Empty,
                useRetroMode || plugin.Settings.TestInRetroMode ? "--retro" : string.Empty,
                gamePath);
            Process.Start(enginePath, options);
        }
    }
}
