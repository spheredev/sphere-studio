using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SphereStudio.Base;
using SphereStudio.SettingsPages;
using SphereStudio.Starters;

namespace SphereStudio
{
    public class PluginMain : IPluginMain
    {
        public string Name => "Oozaru Support";
        public string Description => "Provides support for Oozaru (Web-based Sphere).";
        public string Version => Versioning.Version;
        public string Author => Versioning.Author;

        public void Initialize(ISettings settings)
        {
            PluginManager.Register(this, new OozaruStarter(settings), "Oozaru");
            PluginManager.Register(this, new OozaruSettingsPage(settings), "Oozaru");
        }

        public void ShutDown()
        {
            PluginManager.UnregisterAll(this);
        }
    }
}
