using Shared.Models.FileResults.Generics.Request;
using Shared.Models.OrganizationStrategies.Responses;

namespace Shared.Models.Cases.Requests
{
    public class CreateCaseRequest : CreateMessageResponse, IRequest
    {

        public Guid ProjectId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string EndPointName => StaticClass.Cases.EndPoint.Create;

        public OrganizationStrategyResponse? OrganizationStrategy { get; set; }
        public override string Legend => Name;

        public override string ClassName => StaticClass.Cases.ClassName;
    }
}
