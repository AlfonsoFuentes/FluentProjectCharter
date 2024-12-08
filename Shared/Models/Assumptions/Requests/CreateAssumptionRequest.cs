using Shared.Models.FileResults.Generics.Request;

namespace Shared.Models.Assumptions.Requests
{
    public class CreateAssumptionRequest : CreateMessageResponse, IRequest
    {
        public Guid ProjectId { get; set; }
        public string Name { get; set; } = string.Empty;
        public Guid DeliverableId { get; set; }
        public string EndPointName => StaticClass.Assumptions.EndPoint.Create;

        public override string Legend => Name;

        public override string ClassName => StaticClass.Assumptions.ClassName;
    }
}
