using SphereStudio.Base;
using SphereStudio.SettingsPages;
using SphereStudio.Starters;

namespace SphereStudio
{
    public class PluginMain : IPluginMain
    {
        public string Name => "Sphere 1.x Support";
        public string Description => "Provides support for the legacy Sphere 1.x engine.";
        public string Version => Versioning.Version;
        public string Author => Versioning.Author;

        public void Initialize(ISettings conf)
        {
            PluginManager.Register(this, new Sphere1xStarter(conf), "Sphere 1.x");
            PluginManager.Register(this, new Sphere1xSettingsPage(conf), "Sphere 1.x");
        }

        public void ShutDown()
        {
            PluginManager.UnregisterAll(this);
        }
    }
}
