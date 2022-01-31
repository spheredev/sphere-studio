using SphereStudio.Base;
using SphereStudio.SettingsPages;
using SphereStudio.Starters;

namespace SphereStudio
{
    public class PluginMain : IPluginMain
    {
        public string Name => "Oozaru Engine Support";
        public string Description => "Provides support for the Oozaru Web-based engine.";
        public string Version => Versioning.Version;
        public string Author => Versioning.Author;

        private OozaruSettingsPage settingsPage;
        private OozaruStarter starter;

        public void Initialize(ISettings settings)
        {
            starter = new OozaruStarter(settings);
            settingsPage = new OozaruSettingsPage(settings);
            PluginManager.Register(this, starter, "Oozaru");
            PluginManager.Register(this, settingsPage, "Oozaru");
        }

        public void ShutDown()
        {
            PluginManager.UnregisterAll(this);
            settingsPage.Dispose();
            starter.Dispose();
            settingsPage = null;
            starter = null;
        }
    }
}
