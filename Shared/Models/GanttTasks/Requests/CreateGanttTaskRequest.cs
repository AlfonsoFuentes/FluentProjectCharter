using Shared.Models.FileResults.Generics.Request;
using Shared.Models.GanttTasks.Responses;

namespace Shared.Models.GanttTasks.Requests
{
    //public class CreateGanttTaskRequest : CreateMessageResponse, IRequest
    //{
    //    public string Name { get; set; } = string.Empty;
    //    public string EndPointName => StaticClass.GanttTasks.EndPoint.Create;
    //    public Guid DeliverableId { get; set; }
    //    public Guid ProjectId { get; set; }
    //    public override string Legend => Name;

    //    public override string ClassName => StaticClass.GanttTasks.ClassName;
    //    public string WBS { get; set; } = string.Empty;
    //    public int Order {  get; set; }
    //    public int LabelOrder { get; set; }
    //    public string DependencyType { get; set; } = string.Empty;
    //    public DateTime? StartDate { get; set; }
    //    public DateTime? EndDate { get; set; }
    //    public string? Duration {  get; set; } = string.Empty;
    //    public string? Lag { get; set; } = string.Empty;
    //    public Guid? ParentGanttTaskId {  get; set; }
    //    public List<GanttTaskResponse> Dependants { get; set; } = new(); // Colección de subtareas
    //}
}
