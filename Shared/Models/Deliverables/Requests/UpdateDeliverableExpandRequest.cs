using Shared.Models.FileResults.Generics.Request;

namespace Shared.Models.Deliverables.Requests
{
    public class UpdateDeliverableExpandRequest : CreateMessageResponse, IRequest
    {
        public Guid ProjectId { get; set; }
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string EndPointName => StaticClass.Deliverables.EndPoint.UpdateExpand;
        public override string ClassName => StaticClass.Deliverables.ClassName;
        public bool Expanded {  get; set; }
        public override string Legend => Name;
    }
}
