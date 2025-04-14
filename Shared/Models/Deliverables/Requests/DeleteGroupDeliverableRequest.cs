using Shared.Models.Deliverables.Responses;
using Shared.Models.FileResults.Generics.Request;

namespace Shared.Models.Deliverables.Requests
{
    public class DeleteGroupDeliverableRequest : DeleteMessageResponse, IRequest
    {
        public Guid ProjectId { get; set; }
        public override string Legend => "Group of Deliverable";

        public override string ClassName => StaticClass.Deliverables.ClassName;

        public HashSet<DeliverableResponse> SelecteItems { get; set; } = null!;

        public string EndPointName => StaticClass.Deliverables.EndPoint.DeleteGroup;
    }
}
