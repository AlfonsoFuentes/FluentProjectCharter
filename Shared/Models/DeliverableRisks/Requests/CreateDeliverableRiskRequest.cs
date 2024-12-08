using Shared.Models.FileResults.Generics.Request;

namespace Shared.Models.DeliverableRisks.Requests
{
    public class CreateDeliverableRiskRequest : CreateMessageResponse, IRequest
    {
        public Guid DeliverableId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string EndPointName => StaticClass.DeliverableRisks.EndPoint.Create;
        public Guid ProjectId { get; set; }
        public override string Legend => Name;

        public override string ClassName => StaticClass.DeliverableRisks.ClassName;
    }
}
