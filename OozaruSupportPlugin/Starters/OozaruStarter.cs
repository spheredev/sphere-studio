using System;
using System.IO;
using System.Windows.Forms;

using SphereStudio.Base;

using EmbedIO;

namespace SphereStudio.Starters
{
    class OozaruStarter : IStarter
    {
        private WebServer oozaruServer = null;
        private ISettings settings;

        public OozaruStarter(ISettings settings)
        {
            this.settings = settings;
        }
        
        public bool CanConfigure => false;

        public void Configure()
        {
            throw new Exception("Oozaru doesn't support engine configuration.");
        }

        public void Start(string gamePath, bool isPackage)
        {
            if (isPackage)
                throw new Exception("Oozaru doesn't support running games from an SPK package.");
            var enginePath = settings.GetString("enginePath", string.Empty);
            if (!File.Exists(Path.Combine(enginePath, "index.html")))
            {
                MessageBox.Show(
                    "Unable to launch a local Oozaru server.  Please select a valid Oozaru distribution in Preferences.",
                    "Unable to Start Oozaru",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (oozaruServer != null)
                oozaruServer.Dispose();
            oozaruServer = new WebServer(8080)
                .WithStaticFolder("/dist", gamePath, true)
                .WithStaticFolder("/", enginePath, false);
            oozaruServer.RunAsync();
            var browser = new System.Diagnostics.Process()
            {
                StartInfo = new System.Diagnostics.ProcessStartInfo("http://localhost:8080/") { UseShellExecute = true }
            };
            browser.Start();
        }
    }
}
