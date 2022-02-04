namespace SphereStudio
{
    enum TaskPriority
    {
        High,
        Medium,
        Low,
        None,
    }
    
    enum TaskType
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
            Priority = TaskPriority.None;
            Type = TaskType.Other;
        }

        public TaskListItem(string name, TaskPriority priority)
            : this(name)
        {
            Priority = priority;
            Type = TaskType.Other;
        }

        public TaskListItem(string name, TaskPriority priority, TaskType type)
            : this(name, priority)
        {
            Type = type;
        }

        public string Name { get; set; }

        public TaskPriority Priority { get; set; }

        public TaskType Type { get; set; }

        public bool IsFinished { get; set; }

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
