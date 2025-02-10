using Shared.Models.FileResults.Generics.Request;

namespace Shared.Models.Objectives.Requests
{
    public class CreateObjectiveRequest : CreateMessageResponse, IRequest
    {

        public Guid? StartId { get; set; }
        public Guid? PlanningId { get; set; }

        public Guid ProjectId { get; set; }

        public string Name { get; set; } = string.Empty;
        public string EndPointName => StaticClass.Objectives.EndPoint.Create;

     
        public override string Legend => Name;

        public override string ClassName => StaticClass.Objectives.ClassName;
    }
}
