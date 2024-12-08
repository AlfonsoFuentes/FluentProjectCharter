using Shared.Models.Deliverables.Responses;
using static Shared.StaticClasses.StaticClass;

namespace Shared.Models.Scopes.Responses
{
    public class ScopeResponse : BaseResponse
    {
        public Guid ProjectId { get; set; }
        public Guid CaseId { get; set; }

        public List<DeliverableResponse> Deliverables { get; set; } = new();
    }
}
