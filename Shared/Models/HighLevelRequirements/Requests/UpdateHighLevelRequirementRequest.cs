using Shared.Models.FileResults.Generics.Request;
using Shared.Models.OrganizationStrategies.Responses;

namespace Shared.Models.HighLevelRequirements.Requests
{
    public class UpdateHighLevelRequirementRequest : UpdateMessageResponse, IRequest
    {
        public Guid ProjectId { get; set; }
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public EngineeringFluidCodeResponse? OrganizationStrategy { get; set; }
        public string EndPointName => StaticClass.HighLevelRequirements.EndPoint.Update;

        public override string Legend => Name;

        public override string ClassName => StaticClass.HighLevelRequirements.ClassName;
    }
}
