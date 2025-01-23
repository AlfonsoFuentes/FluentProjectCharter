using Shared.Models.FileResults.Generics.Request;

namespace Shared.Models.Deliverables.Requests
{
    public class ChangeDeliverableOrderDowmRequest : UpdateMessageResponse, IRequest
    {
        public Guid ScopeId { get; set; }
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Guid ProjectId { get; set; }
        public string EndPointName => StaticClass.Deliverables.EndPoint.UpdateDown;

        public override string Legend => Name;

        public override string ClassName => StaticClass.Deliverables.ClassName;
    }
}
