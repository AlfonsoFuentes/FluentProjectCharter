using Shared.Models.FileResults.Generics.Request;

namespace Shared.Models.Deliverables.Requests
{
    public class DeleteDeliverableRequest : DeleteMessageResponse, IRequest
    {
        public string Name { get; set; } = string.Empty;
        public override string Legend => Name;
  
        public override string ClassName => StaticClass.Deliverables.ClassName;

        public Guid Id { get; set; }
      

        public string EndPointName => StaticClass.Deliverables.EndPoint.Delete;
    }
}
