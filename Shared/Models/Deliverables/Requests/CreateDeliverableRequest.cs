using Shared.Models.FileResults.Generics.Request;

namespace Shared.Models.Deliverables.Requests
{
    public class CreateDeliverableRequest : CreateMessageResponse, IRequest
    {
        
        
      
        public Guid ProjectId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string EndPointName => StaticClass.Deliverables.EndPoint.Create;
    
        public override string Legend => Name;

        public override string ClassName => StaticClass.Deliverables.ClassName;
    }
}
