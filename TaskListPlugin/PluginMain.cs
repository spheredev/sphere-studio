using System;

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

        private TaskListPane taskListPane;

        public void Initialize(ISettings settings)
        {
            taskListPane = new TaskListPane();
            
            PluginManager.Register(this, taskListPane, "Task List");
            PluginManager.Core.LoadProject += ide_LoadProject;
            PluginManager.Core.UnloadProject += ide_UnloadProject;

            // if a project is already loaded, populate its task list
            var projectRoot = PluginManager.Core.Project?.RootPath;            
            if (projectRoot != null)
                taskListPane.LoadTaskList(projectRoot);
        }

        public void ShutDown()
        {
            taskListPane.SaveTaskList();
            PluginManager.Core.LoadProject -= ide_LoadProject;
            PluginManager.Core.UnloadProject -= ide_UnloadProject;
            PluginManager.UnregisterAll(this);
        }

        private void ide_LoadProject(object sender, EventArgs e)
        {
            taskListPane.LoadTaskList(PluginManager.Core.Project.RootPath);
        }

        private void ide_UnloadProject(object sender, EventArgs e)
        {
            taskListPane.SaveTaskList();
            taskListPane.Clear(true);
        }
    }
}
