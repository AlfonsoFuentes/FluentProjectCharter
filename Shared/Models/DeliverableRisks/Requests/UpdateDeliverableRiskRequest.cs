using Shared.Models.FileResults.Generics.Request;

namespace Shared.Models.DeliverableRisks.Requests
{
    public class UpdateDeliverableRiskRequest : UpdateMessageResponse, IRequest
    {
        public Guid ScopeId { get; set; }
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Guid ProjectId { get; set; }
        public string EndPointName => StaticClass.DeliverableRisks.EndPoint.Update;

        public override string Legend => Name;

        public override string ClassName => StaticClass.DeliverableRisks.ClassName;
    }
}
