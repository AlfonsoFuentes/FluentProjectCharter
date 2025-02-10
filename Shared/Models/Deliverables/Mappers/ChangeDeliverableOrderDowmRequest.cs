using Shared.Models.FileResults.Generics.Request;

namespace Shared.Models.Deliverables.Mappers
{
    public class ChangeDeliverableOrderDowmRequest : UpdateMessageResponse, IRequest
    {

        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Guid ProjectId { get; set; }
        public string EndPointName => StaticClass.Deliverables.EndPoint.UpdateDown;
        public int Order { get; set; }
        public override string Legend => Name;

        public override string ClassName => StaticClass.Deliverables.ClassName;
    }

}
