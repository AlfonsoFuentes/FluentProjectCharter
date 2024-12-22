using Shared.Models.FileResults.Generics.Request;
using Shared.Models.OrganizationStrategies.Responses;

namespace Shared.Models.AppStates.Requests
{
    public class CreateAppStateRequest : CreateMessageResponse, IRequest
    {

        public Guid ProjectId { get; set; }
        public string? ProjectAcordionActive { get; set; } = string.Empty;
        public string? ProjectTabActive { get; set; } = string.Empty;
        public string? CaseAcordionActive { get; set; } = string.Empty;
        public string? DeliverableAcordionActive { get; set; } = string.Empty;
        public string? ScopeAcordionActive { get; set; } = string.Empty;

        public string? DeliverableTabActive { get; set; } = string.Empty;
        public string? CaseTabActive { get; set; } = string.Empty;
        public string EndPointName => StaticClass.AppStates.EndPoint.Create;

        public OrganizationStrategyResponse? OrganizationStrategy { get; set; }
        public override string Legend => "";

        public override string ClassName => StaticClass.AppStates.ClassName;
    }
}
