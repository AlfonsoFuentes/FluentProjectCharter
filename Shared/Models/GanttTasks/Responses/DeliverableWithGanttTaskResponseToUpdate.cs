namespace Shared.Models.GanttTasks.Responses
{
    public class DeliverableWithGanttTaskResponseToUpdate
    {
        public Guid ProjectId { get; set; }
        public Guid DeliverableId { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<GanttTaskResponse> FlatOrderedItems { get; set; } = new();
        public List<GanttTaskResponse> Items { get; set; } = new();
        public int LabelOrder { get; set; }
        public string WBS { get; set; } = string.Empty;
        public bool IsExpanded { get; set; }
        public bool ShowBudgetItems { get; set; }
    }
}
