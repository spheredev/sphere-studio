using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

using SphereStudio.Base;

namespace SphereStudio.Starters
{
    class Sphere1xStarter : IStarter
    {
        private ISettings settings;

        public Sphere1xStarter(ISettings settings)
        {
            this.settings = settings;
        }

        public bool CanConfigure
        {
            get
            {
                var enginePath = settings.GetString("enginePath", "");
                return File.Exists(Path.Combine(enginePath, "config.exe"));
            }
        }

        public void Start(string gamePath, bool isPackage)
        {
            var enginePath = Path.Combine(settings.GetString("enginePath", ""), "engine.exe");
            var options = $@"-game ""{gamePath}""";
            if (File.Exists(enginePath))
                Process.Start(enginePath, options);
            else
            {
                MessageBox.Show(
                    "Sphere 1.x or compatible engine was not found.  Please check your Sphere installation path under Preferences -> Sphere 1.x.",
                    "Unable to Start Engine", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void Configure()
        {
            if (!CanConfigure)
                throw new NotSupportedException("Engine doesn't support configuration.");

            var enginePath = settings.GetString("enginePath", "");
            ProcessStartInfo psi = new ProcessStartInfo()
            {
                FileName = Path.Combine(enginePath, "config.exe"),
                UseShellExecute = false,
                WorkingDirectory = enginePath,
            };
            Process.Start(psi);
        }
    }
}
