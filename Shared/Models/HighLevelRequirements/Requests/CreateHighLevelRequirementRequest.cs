using Shared.Models.FileResults.Generics.Request;
using Shared.Models.OrganizationStrategies.Responses;

namespace Shared.Models.HighLevelRequirements.Requests
{
    public class CreateHighLevelRequirementRequest : CreateMessageResponse, IRequest
    {

        public Guid ProjectId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string EndPointName => StaticClass.HighLevelRequirements.EndPoint.Create;

        public OrganizationStrategyResponse? OrganizationStrategy { get; set; }
        public override string Legend => Name;

        public override string ClassName => StaticClass.HighLevelRequirements.ClassName;
    }
}
