namespace Shared.Models.GanttTasks.Responses
{
    public class DeliverableWithGanttTaskResponseList
    {
        public Guid ProjectId { get; set; }
        public List<DeliverableWithGanttTaskResponse> Deliverables { get; set; } = new();
   
    }
}
