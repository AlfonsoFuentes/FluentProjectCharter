using Shared.Models.FileResults.Generics.Request;

namespace Shared.Models.Requirements.Requests
{
    public class UpdateRequirementRequest : UpdateMessageResponse, IRequest
    {

        public Guid ProjectId { get; set; }
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public string EndPointName => StaticClass.Requirements.EndPoint.Update;

        public override string Legend => Name;

        public override string ClassName => StaticClass.Requirements.ClassName;
    }
}
