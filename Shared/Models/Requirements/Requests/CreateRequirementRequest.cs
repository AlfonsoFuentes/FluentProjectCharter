using Shared.Models.FileResults.Generics.Request;

namespace Shared.Models.Requirements.Requests
{
    public class CreateRequirementRequest : CreateMessageResponse, IRequest
    {
        public Guid? StartId { get; set; }
        public Guid? PlanningId { get; set; }
        public Guid ProjectId { get; set; }
        public string Name { set; get; } = string.Empty;
        public string EndPointName => StaticClass.Requirements.EndPoint.Create;
    
        public override string Legend => Name;

        public override string ClassName => StaticClass.Requirements.ClassName;
    }
}
