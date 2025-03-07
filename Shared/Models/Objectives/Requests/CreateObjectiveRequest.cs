using Shared.Models.FileResults.Generics.Request;

namespace Shared.Models.Objectives.Requests
{
    public class CreateObjectiveRequest : CreateMessageResponse, IRequest
    {

        
        

        public Guid ProjectId { get; set; }

        public string Name { get; set; } = string.Empty;
        public string EndPointName => StaticClass.Objectives.EndPoint.Create;

     
        public override string Legend => Name;

        public override string ClassName => StaticClass.Objectives.ClassName;
    }
}
