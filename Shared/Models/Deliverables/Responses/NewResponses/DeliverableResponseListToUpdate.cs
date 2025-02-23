using Shared.Models.FileResults.Generics.Reponses;
using Shared.Models.FileResults.Generics.Request;

namespace Shared.Models.Deliverables.Responses.NewResponses
{
    public class DeliverableResponseListToUpdate : UpdateMessageResponse, IResponseAll, IRequest
    {
        public string EndPointName => StaticClass.Deliverables.EndPoint.UpdateEDT;
        public override string Legend => "Deliverables";

        public override string ClassName => StaticClass.Deliverables.ClassName;
        public Guid ProjectId { get; set; }
        public List<DeliverableResponse> FlatOrderedItems { get; set; } = new();
        public List<DeliverableResponse> Items { get; set; } = new();
    }
}
