using Shared.Models.FileResults.Generics.Request;

namespace Shared.Models.DeliverableRisks.Requests
{
    public class DeleteDeliverableRiskRequest : DeleteMessageResponse, IRequest
    {
        public string Name { get; set; } = string.Empty;
        public override string Legend => Name;
        public Guid ProjectId { get; set; }
        public override string ClassName => StaticClass.DeliverableRisks.ClassName;

        public Guid Id { get; set; }

        public string EndPointName => StaticClass.DeliverableRisks.EndPoint.Delete;
    }
}
