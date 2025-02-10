using Shared.Models.FileResults.Generics.Request;

namespace Shared.Models.Resources.Requests
{
    public class CreateResourceRequest : CreateMessageResponse, IRequest
    {
        public Guid? StartId { get; set; }
        public Guid? PlanningId { get; set; }
     
        public Guid ProjectId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string EndPointName => StaticClass.Resources.EndPoint.Create;
     
        public override string Legend => Name;

        public override string ClassName => StaticClass.Resources.ClassName;
    }
}
