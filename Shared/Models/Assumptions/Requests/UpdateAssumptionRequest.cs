using Shared.Models.FileResults.Generics.Request;

namespace Shared.Models.Assumptions.Requests
{
    public class UpdateAssumptionRequest : UpdateMessageResponse, IRequest
    {

        public Guid ProjectId { get; set; }
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
  
        public string EndPointName => StaticClass.Assumptions.EndPoint.Update;

        public override string Legend => Name;

        public override string ClassName => StaticClass.Assumptions.ClassName;
    }
}
