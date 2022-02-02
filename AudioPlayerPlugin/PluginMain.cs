using System;

using SphereStudio.Base;
using SphereStudio.FileOpeners;
using SphereStudio.UI;

namespace SphereStudio
{
    public class PluginMain : IPluginMain
    {
        public string Name => "Audio Player Sidebar";
        public string Description => "Listen to sounds from your game while you work!";
        public string Version => Versioning.Version;
        public string Author => Versioning.Author;

        private AudioPlayerPane dockPane;

        public void Initialize(ISettings settings)
        {
            dockPane = new AudioPlayerPane(this);
            dockPane.WatchProject(PluginManager.Core.Project);
            dockPane.Refresh();

            PluginManager.Register(this, dockPane, "Audio Player");
            PluginManager.Register(this, new AudioFileOpener(dockPane), "Audio Player Sidebar");

            PluginManager.Core.LoadProject += ide_LoadProject;
            PluginManager.Core.UnloadProject += ide_UnloadProject;
            PluginManager.Core.TestGame += ide_TestGame;
        }

        public void ShutDown()
        {
            PluginManager.UnregisterAll(this);
            dockPane.WatchProject(null);
            dockPane.stopPlayback();
            PluginManager.Core.LoadProject -= ide_LoadProject;
            PluginManager.Core.UnloadProject -= ide_UnloadProject;
            PluginManager.Core.TestGame -= ide_TestGame;
        }

        private void ide_LoadProject(object sender, EventArgs e)
        {
            dockPane.WatchProject(PluginManager.Core.Project);
        }

        private void ide_UnloadProject(object sender, EventArgs e)
        {
            dockPane.WatchProject(null);
        }

        private void ide_TestGame(object sender, EventArgs e)
        {
            dockPane.ForcePause();
        }
    }
}
