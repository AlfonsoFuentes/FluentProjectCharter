using Shared.Models.FileResults.Generics.Request;
using Shared.Models.OrganizationStrategies.Responses;

namespace Shared.Models.AppStates.Requests
{
    public class UpdateActiveStateRequest
    {
        public Guid? ProjectId { set; get; }
        public string? ProjectTab { get; set; } = string.Empty;
        public Guid? CaseId { set; get; }
        public string? CaseTab { get; set; } = string.Empty;
        public Guid? ScopeId { set; get; }
        public string? ScopeTab { get; set; } = string.Empty;
        public Guid? DeliverableId { set; get; }
        public string? DeliverableTab { get; set; } = string.Empty;


    }

}
