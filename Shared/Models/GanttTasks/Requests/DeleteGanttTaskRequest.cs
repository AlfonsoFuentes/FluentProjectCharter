using Shared.Models.FileResults.Generics.Request;

namespace Shared.Models.GanttTasks.Requests
{
    public class DeleteGanttTaskRequest : DeleteMessageResponse, IRequest
    {
        public string Name { get; set; } = string.Empty;
        public override string Legend => Name;

        public override string ClassName => StaticClass.GanttTasks.ClassName;

        public Guid ProjectId { get; set; }
        public Guid Id { get; set; }

        public string EndPointName => StaticClass.GanttTasks.EndPoint.Delete;
    }
}
