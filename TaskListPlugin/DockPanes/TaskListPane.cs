using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

using SphereStudio.Base;
using SphereStudio.Properties;

using BrightIdeasSoftware;

namespace SphereStudio.UI
{
    partial class TaskListPane : UserControl, IStyleAware, IDockPane
    {
        private ImageList iconImageList = new ImageList();
        private string tasksFilePath;

        public TaskListPane()
        {
            InitializeComponent();
            StyleManager.AutoStyle(this);

            iconImageList.ColorDepth = ColorDepth.Depth32Bit;
            iconImageList.Images.Add("not", Resources.lightbulb);
            iconImageList.Images.Add("done", Resources.lightbulb_off);
            taskListView.SmallImageList = iconImageList;

            olvColumn1.ImageGetter = delegate (object rowObject)
            {
                var entry = (TaskListItem)rowObject;
                return entry.IsFinished ? "done" : "not";
            };

            var taskTypeNames = Enum.GetNames(typeof(TaskType));
            var priorityNames = Enum.GetNames(typeof(TaskPriority));
            foreach (var name in taskTypeNames)
                setTypeMenuItem.DropDownItems.Add(name, Resources.lightbulb, taskTypeMenuItem_Click);
            foreach (var name in priorityNames)
                setPriorityMenuItem.DropDownItems.Add(name, Resources.resultset_none, taskPriorityMenuItem_Click);
        }

        public Control Control => this;

        public DockHint DockHint => DockHint.Left;

        public Bitmap DockIcon => Resources.lightbulb;

        public bool ShowInViewMenu => true;

        public void ApplyStyle(UIStyle style)
        {
            taskListView.AlternateRowBackColor = style.LabelColor;
            taskListView.SelectedBackColor = style.HighlightColor;
            taskListView.SelectedForeColor = style.TextColor;
            taskListView.UseAlternatingBackColors = true;
            style.AsUIElement(toolStrip);
            style.AsTextView(taskListView);
        }

        public void Clear(bool unloading = false)
        {
            taskListView.ClearObjects();
            if (unloading)
                tasksFilePath = null;
        }

        public void LoadTaskList(string projectRoot)
        {
            tasksFilePath = Path.Combine(projectRoot, "sphereStudio.tasks");
            taskListView.ClearObjects();
            if (!File.Exists(tasksFilePath))
                return;
            using (var reader = new BinaryReader(File.OpenRead(tasksFilePath)))
            {
                var taskList = new List<TaskListItem>();
                var taskCount = reader.ReadInt32();
                while (taskCount-- > 0)
                {
                    var taskListItem = new TaskListItem()
                    {
                        IsFinished = reader.ReadBoolean(),
                        Name = reader.ReadString(),
                        Priority = (TaskPriority)reader.ReadInt32(),
                        Type = (TaskType)reader.ReadInt32()
                    };
                    taskList.Add(taskListItem);
                }
                taskListView.SetObjects(taskList);
            }
        }

        public void SaveTaskList()
        {
            if (deleteFileIfEmpty())
                return;
            using (var fileWriter = new BinaryWriter(File.OpenWrite(tasksFilePath)))
            {
                fileWriter.Write(taskListView.GetItemCount());
                foreach (TaskListItem task in taskListView.Objects)
                {
                    fileWriter.Write(task.IsFinished);
                    fileWriter.Write(task.Name);
                    fileWriter.Write((int)task.Priority);
                    fileWriter.Write((int)task.Type);
                }
                fileWriter.Flush();
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            refreshMenuCommands();
            base.OnLoad(e);
        }

        private bool deleteFileIfEmpty()
        {
            if (string.IsNullOrEmpty(tasksFilePath))
                return true;
            if (taskListView.GetItemCount() == 0)
            {
                if (File.Exists(tasksFilePath))
                    File.Delete(tasksFilePath);
                return true;
            }
            else
            {
                return false;
            }
        }

        private void refreshMenuCommands()
        {
            var haveSelection = taskListView.SelectedObjects.Count > 0;
            
            decreasePriorityToolButton.Enabled = haveSelection;
            increasePriorityToolButton.Enabled = haveSelection;
            removeTaskToolButton.Enabled = haveSelection;

            removeTaskMenuItem.Enabled = haveSelection;
            setPriorityMenuItem.Enabled = haveSelection;
            setTypeMenuItem.Enabled = haveSelection;
        }

        private void taskListView_FormatCell(object sender, FormatCellEventArgs e)
        {
            var taskListItem = e.Model as TaskListItem;
            if (e.ColumnIndex == olvColumn1.Index && taskListItem != null)
            {
                var fontStyle = taskListItem.IsFinished ? FontStyle.Strikeout : FontStyle.Regular;
                e.SubItem.Font = new Font(e.SubItem.Font, fontStyle);
            }
        }

        private void taskListView_FormatRow(object sender, FormatRowEventArgs e)
        {
            var taskListItem = (TaskListItem)e.Model;
            switch (taskListItem.Priority)
            {
                case TaskPriority.High:
                    e.Item.BackColor = Color.FromArgb(250, 150, 150);
                    e.Item.ForeColor = Color.Black;
                    break;
                case TaskPriority.Low:
                    e.Item.BackColor = Color.FromArgb(150, 250, 150);
                    e.Item.ForeColor = Color.Black;
                    break;
                case TaskPriority.Medium:
                    e.Item.BackColor = Color.FromArgb(250, 250, 150);
                    e.Item.ForeColor = Color.Black;
                    break;
                case TaskPriority.None:
                    e.Item.BackColor = StyleManager.Style.BackColor;
                    e.Item.ForeColor = StyleManager.Style.TextColor;
                    break;
            }
        }

        private void taskListView_SelectionChanged(object sender, EventArgs e)
        {
            refreshMenuCommands();
        }

        private void decreasePriorityToolButton_Click(object sender, EventArgs e)
        {
            foreach (TaskListItem task in taskListView.SelectedObjects)
                task.DecreasePriority();
            taskListView.RefreshSelectedObjects();
        }

        private void increasePriorityToolButton_Click(object sender, EventArgs e)
        {
            foreach (TaskListItem task in taskListView.SelectedObjects)
                task.IncreasePriority();
            taskListView.RefreshSelectedObjects();
        }

        private void addTaskMenuItem_Click(object sender, EventArgs e)
        {
            taskListView.AddObject(new TaskListItem("New Task"));
        }

        private void removeAllTasksMenuItem_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void removeCompletedTasksMenuItem_Click(object sender, EventArgs e)
        {
            var removedItems = taskListView.Objects
                .Cast<TaskListItem>()
                .Where(entry => entry.IsFinished)
                .ToArray();
            taskListView.RemoveObjects(removedItems);
        }

        private void removeTaskMenuItem_Click(object sender, EventArgs e)
        {
            foreach (TaskListItem task in taskListView.SelectedObjects)
                taskListView.RemoveObject(task);
        }

        private void taskPriorityMenuItem_Click(object sender, EventArgs e)
        {
            foreach (TaskListItem taskListItem in taskListView.SelectedObjects)
            {
                var index = setPriorityMenuItem.DropDownItems.IndexOf((ToolStripItem)sender);
                taskListItem.Priority = (TaskPriority)index;
                taskListView.RefreshObject(taskListItem);
            }
        }

        private void taskTypeMenuItem_Click(object sender, EventArgs e)
        {
            foreach (TaskListItem taskListItem in taskListView.SelectedObjects)
            {
                var index = setTypeMenuItem.DropDownItems.IndexOf((ToolStripItem)sender);
                taskListItem.Type = (TaskType)index;
                taskListView.RefreshObject(taskListItem);
            }
        }
    }
}
