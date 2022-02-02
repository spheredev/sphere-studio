using System;
using System.Windows.Forms;

using SphereStudio.Base;
using SphereStudio.UI;

namespace SphereStudio
{
    public class PluginMain : IPluginMain
    {
        public string Name => "Task List Sidebar";
        public string Description => "Keep track of game development tasks.";
        public string Version => Versioning.Version;
        public string Author => Versioning.Author;

        private TaskListPane dockPane;

        public void Initialize(ISettings settings)
        {
            dockPane = new TaskListPane();
            
            PluginManager.Register(this, dockPane, "Task List");
            PluginManager.Core.LoadProject += ide_LoadProject;
            PluginManager.Core.UnloadProject += ide_UnloadProject;

            // if a project is already loaded, populate its task list
            var projectRoot = PluginManager.Core.Project?.RootPath;            
            if (projectRoot != null)
                dockPane.LoadTaskList(projectRoot);
        }

        public void ShutDown()
        {
            PluginManager.Core.LoadProject -= ide_LoadProject;
            PluginManager.Core.UnloadProject -= ide_UnloadProject;
            PluginManager.UnregisterAll(this);
        }

        private void ide_LoadProject(object sender, EventArgs e)
        {
            dockPane.LoadTaskList(PluginManager.Core.Project.RootPath);
        }

        private void ide_UnloadProject(object sender, EventArgs e)
        {
            dockPane.SaveTaskList();
            dockPane.Clear(true);
        }
    }
}
