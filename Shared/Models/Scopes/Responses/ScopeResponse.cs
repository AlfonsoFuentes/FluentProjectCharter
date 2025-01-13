using Shared.Models.Deliverables.Responses;
using Shared.Models.FileResults.Generics.Reponses;
using static Shared.StaticClasses.StaticClass;

namespace Shared.Models.Scopes.Responses
{
    public class ScopeResponse : BaseResponse, IUpdateStateResponse
    {
        public string EndPointName => StaticClass.Scopes.EndPoint.UpdateState;
        public Guid ProjectId { get; set; }
        public Guid CaseId { get; set; }

        public DeliverableResponse? CurrentDeliverable { get; set; }
        public List<DeliverableResponse> Deliverables { get; set; } = new();
        public bool IsNodeOpen { get; set; }
        public string? Tab { get; set; } = string.Empty;
        public void Open()
        {
            IsNodeOpen = true;
        }
        public void Close()
        {
            IsNodeOpen = false;
        }
    }
}
