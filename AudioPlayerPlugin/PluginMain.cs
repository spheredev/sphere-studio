using System;

using SphereStudio.Base;
using SphereStudio.Plugins.UI;

namespace SphereStudio.Plugins
{
    public class PluginMain : IPluginMain
    {
        public string Name => "Audio Player";
        public string Description => "Listen to sounds from your game while you work!";
        public string Version => Versioning.Version;
        public string Author => Versioning.Author;

        private AudioPlayerPane dockPane;

        public void Initialize(ISettings conf)
        {
            dockPane = new AudioPlayerPane(this);
            dockPane.WatchProject(PluginManager.Core.Project);
            dockPane.Refresh();

            PluginManager.Register(this, dockPane, "Audio Player");

            PluginManager.Core.LoadProject += on_LoadProject;
            PluginManager.Core.UnloadProject += on_UnloadProject;
            PluginManager.Core.TestGame += on_TestGame;
        }

        public void ShutDown()
        {
            PluginManager.UnregisterAll(this);
            dockPane.WatchProject(null);
            dockPane.stopPlayback();
            PluginManager.Core.LoadProject -= on_LoadProject;
            PluginManager.Core.UnloadProject -= on_UnloadProject;
            PluginManager.Core.TestGame -= on_TestGame;
        }

        private void on_LoadProject(object sender, EventArgs e)
        {
            dockPane.WatchProject(PluginManager.Core.Project);
        }

        private void on_UnloadProject(object sender, EventArgs e)
        {
            dockPane.WatchProject(null);
        }

        private void on_TestGame(object sender, EventArgs e)
        {
            dockPane.ForcePause();
        }
    }
}
