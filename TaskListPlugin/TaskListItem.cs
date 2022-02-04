namespace SphereStudio
{
    enum TaskPriority
    {
        High,
        Medium,
        Low,
        None,
    }
    
    enum TaskCategory
    {
        Addition,
        Art,
        Bug,
        Feature,
        Gameplay,
        Other,
        UI,
    }

    class TaskListItem
    {
        public TaskListItem(string name = "")
        {
            Name = name;
            Category = TaskCategory.Other;
            Priority = TaskPriority.None;
        }

        public TaskListItem(string name, TaskPriority priority)
            : this(name)
        {
            Category = TaskCategory.Other;
            Priority = priority;
        }

        public TaskListItem(string name, TaskPriority priority, TaskCategory type)
            : this(name, priority)
        {
            Category = type;
        }

        public TaskCategory Category { get; set; }

        public bool Finished { get; set; }

        public string Name { get; set; }

        public TaskPriority Priority { get; set; }

        public void IncreasePriority()
        {
            var p = (int)Priority;
            if (p - 1 >= 0)
                p--;
            Priority = (TaskPriority)p;
        }

        public void DecreasePriority()
        {
            var p = (int)Priority;
            if (p + 1 <= 3)
                p++;
            Priority = (TaskPriority)p;
        }
    }
}
