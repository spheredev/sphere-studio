using System;
using System.Windows.Forms;

using SphereStudio.Base;
using SphereStudio.Plugins.UI;

namespace SphereStudio.Plugins
{
    public class PluginMain : IPluginMain
    {
        public string Name => "Task List";
        public string Description => "Keep track of game development tasks.";
        public string Version => Versioning.Version;
        public string Author => Versioning.Author;

        private TaskListPane dockPane;

        public void Initialize(ISettings conf)
        {
            dockPane = new TaskListPane();
            
            PluginManager.Register(this, dockPane, "Task List");
            PluginManager.Core.LoadProject += on_LoadProject;
            PluginManager.Core.UnloadProject += on_UnloadProject;

            // if a project is already loaded, populate its task list
            var projectRoot = PluginManager.Core.Project?.RootPath;            
            if (projectRoot != null)
                dockPane.LoadTaskList(projectRoot);
        }

        public void ShutDown()
        {
            PluginManager.Core.LoadProject -= on_LoadProject;
            PluginManager.Core.UnloadProject -= on_UnloadProject;
            PluginManager.UnregisterAll(this);
        }

        private void on_LoadProject(object sender, EventArgs e)
        {
            dockPane.LoadTaskList(PluginManager.Core.Project.RootPath);
        }

        private void on_UnloadProject(object sender, EventArgs e)
        {
            dockPane.SaveTaskList();
            dockPane.Clear();
        }
    }
}
