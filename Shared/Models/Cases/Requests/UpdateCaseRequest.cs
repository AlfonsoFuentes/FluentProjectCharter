using Shared.Models.FileResults.Generics.Request;
using Shared.Models.OrganizationStrategies.Responses;

namespace Shared.Models.Cases.Requests
{
    public class UpdateCaseRequest : UpdateMessageResponse, IRequest
    {
        public Guid ProjectId { get; set; }
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public OrganizationStrategyResponse? OrganizationStrategy { get; set; }
        public string EndPointName => StaticClass.Cases.EndPoint.Update;

        public override string Legend => Name;

        public override string ClassName => StaticClass.Cases.ClassName;
    }
}
