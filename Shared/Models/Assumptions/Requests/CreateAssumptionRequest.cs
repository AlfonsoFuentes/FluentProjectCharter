using Shared.Models.FileResults.Generics.Request;

namespace Shared.Models.Assumptions.Requests
{
    public class CreateAssumptionRequest : CreateMessageResponse, IRequest
    {
        public Guid? StartId { get; set; }
        public Guid? PlanningId { get; set; }
       
        public Guid ProjectId { get; set; }
        public string Name { get; set; } = string.Empty;
        public Guid? ScopeId { get; set; }
        public string EndPointName => StaticClass.Assumptions.EndPoint.Create;

        public override string Legend => Name;

        public override string ClassName => StaticClass.Assumptions.ClassName;
    }
}
