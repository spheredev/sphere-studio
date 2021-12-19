using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

using SphereStudio.Base;

using EmbedIO;
using EmbedIO.Files;

namespace SphereStudio.Starters
{
    class OozaruStarter : IStarter, IDisposable
    {
        private WebServer gameServer;
        private ISettings settings;

        public OozaruStarter(ISettings settings)
        {
            this.settings = settings;
        }

        public void Dispose()
        {
            if (gameServer != null)
                gameServer.Dispose();
            gameServer = null;
            settings = null;
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
            if (gameServer != null)
                gameServer.Dispose();
            var enginePort = settings.GetInteger("serverPort", 8080);
            gameServer = new WebServer($"http://localhost:{enginePort}")
                .WithStaticFolder("/dist", gamePath, true, m => m
                    .WithoutContentCaching())
                .WithStaticFolder("/", enginePath, false);
            gameServer.RunAsync();
            Process.Start($"http://localhost:{enginePort}/");
        }
    }
}
