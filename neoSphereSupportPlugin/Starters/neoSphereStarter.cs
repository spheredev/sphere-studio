using System;
using System.Diagnostics;
using System.IO;

using SphereStudio.Base;
using SphereStudio.Debuggers;

namespace SphereStudio.Starters
{
    class neoSphereStarter : IDebugStarter
    {
        private PluginMain main;
        private bool useRetroMode;

        public neoSphereStarter(PluginMain main, bool useRetroMode = false)
        {
            this.main = main;
            this.useRetroMode = useRetroMode;
        }

        public bool CanConfigure => false;

        public void Start(string gamePath, bool isPackage)
        {
            var gdkPath = main.Conf.EnginePath;
            var wantConsole = main.Conf.AlwaysUseConsole;
            var wantWindow = main.Conf.TestInWindow || wantConsole;
            var enginePath = Path.Combine(gdkPath, wantConsole ? "spherun.exe" : "neoSphere.exe");
            var options = string.Format(@"{0} --verbose {1} {2} {3} ""{4}""",
                wantWindow ? "--windowed" : "",
                main.Conf.Verbosity,
                wantConsole ? "--profile" : "",
                useRetroMode || main.Conf.TestInRetroMode ? "--retro" : "",
                gamePath);
            Process.Start(enginePath, options);
        }

        public void Configure()
        {
            throw new NotSupportedException("neoSphere doesn't support engine configuration.");
        }

        public IDebugger Debug(string gamePath, bool isPackage, IProject project)
        {
            string gdkPath = main.Conf.EnginePath;

            PluginManager.Core.Docking.Activate(Panes.Console);
            Panes.Console.ClearConsole();
            PluginManager.Core.Docking.Show(Panes.Inspector);
            var enginePath = Path.Combine(gdkPath, "spherun.exe");
            var options = string.Format(@"--verbose {0} --debug {1} ""{2}""",
                main.Conf.Verbosity,
                useRetroMode ? "--retro" : "",
                gamePath);
            Process engineProcess = Process.Start(enginePath, options);
            return new SsjDebugger(main, gamePath, enginePath, engineProcess, project);
        }
    }
}
