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
        public static string Style => "Default: Blue";
    }

    static class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            PluginManager.Register(null, new DefaultStyleProvider(), "Default");
            PluginManager.Register(null, new ClassicCompiler(), Defaults.Compiler);
            PluginManager.Register(null, new MainSettingsPage(), "Sphere Studio");
            PluginManager.Register(null, new SphereProjectPage(), "Sphere Game");

            Window = new SphereStudioWindow();

            // check for and open files dragged onto the app.
            foreach (var fileName in args)
            {
                if (File.Exists(fileName))
                {
                    Window.OpenFile(fileName);
                }
            }

            if (args.Length > 0 && File.Exists(args[args.Length - 1]))
            {
                Window.SetDefaultActive(args[args.Length - 1]);
            }

            Application.Run(Window);
        }

        public static SphereStudioWindow Window { get; private set; }
    }
}
