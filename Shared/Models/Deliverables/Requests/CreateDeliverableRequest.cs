using Shared.Enums.TasksRelationTypeTypes;
using Shared.Models.FileResults.Generics.Request;

namespace Shared.Models.Deliverables.Requests
{
    public class CreateDeliverableRequest : CreateMessageResponse, IRequest
    {
        public Guid? StartId { get; set; }
        public Guid? PlanningId { get; set; }
      
        public string Name { get; set; } = string.Empty;
        public string EndPointName => StaticClass.Deliverables.EndPoint.Create;
        public Guid ProjectId { get; set; }
        public override string Legend => Name;

        public override string ClassName => StaticClass.Deliverables.ClassName;
        public string WBS { get; set; } = string.Empty;
        public int Order {  get; set; }
        public int LabelOrder { get; set; }
        public string DependencyType { get; set; } = string.Empty;
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? Duration {  get; set; } = string.Empty;
        public string? Lag { get; set; } = string.Empty;
    }
}
