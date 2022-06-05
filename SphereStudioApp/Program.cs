using System;
using System.IO;
using System.Windows.Forms;

using SphereStudio.Base;
using SphereStudio.Compilers;
using SphereStudio.Forms;
using SphereStudio.ProjectPages;
using SphereStudio.SettingsPages;
using SphereStudio.StyleProviders;

namespace SphereStudio
{
    static class Defaults
    {
        public static string Compiler => "Sphere Classic";
        public static string Style => "Default: Dark Mode";
    }

    static class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // register built-in components
            PluginManager.Register(null, new DefaultStyleProvider(), "Default");
            PluginManager.Register(null, new SphereCompiler(), Defaults.Compiler);
            PluginManager.Register(null, new EnvironmentSettingsPage(), "Environment");
            PluginManager.Register(null, new PluginsSettingsPage(), "Plugins");
            PluginManager.Register(null, new SphereProjectPage(), "Sphere Game");

            // create the main IDE form and run the application
            var ideWindow = new IdeWindowForm();
            foreach (var fileName in args)
            {
                if (File.Exists(fileName))
                    ideWindow.OpenFile(fileName);
            }
            if (args.Length > 0 && File.Exists(args[args.Length - 1]))
                ideWindow.SetDefaultActiveFile(args[args.Length - 1]);
            Application.Run(ideWindow);
        }
    }
}
