using Shared.Models.FileResults.Generics.Request;

namespace Shared.Models.GanttTasks.Validators
{
   
    public class ValidateGanttTaskRequest : ValidateMessageResponse, IRequest
    {
        public Guid? Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Guid DeliverableId { get; set; }

        public string EndPointName => StaticClass.GanttTasks.EndPoint.Validate;

        public override string Legend => Name;

        public override string ClassName => StaticClass.GanttTasks.ClassName;
    }
}
