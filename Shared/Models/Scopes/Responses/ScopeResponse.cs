using Shared.Models.AcceptanceCriterias.Responses;
using Shared.Models.Assumptions.Responses;
using Shared.Models.Bennefits.Responses;
using Shared.Models.Constrainsts.Responses;
using Shared.Models.DeliverableRisks.Responses;
using Shared.Models.Deliverables.Responses;
using Shared.Models.FileResults.Generics.Reponses;
using Shared.Models.Requirements.Responses;
using static Shared.StaticClasses.StaticClass;

namespace Shared.Models.Scopes.Responses
{
    public class ScopeResponse : BaseResponse, IUpdateStateResponse
    {
        public string EndPointName => StaticClass.Scopes.EndPoint.UpdateState;
        public Guid ProjectId { get; set; }
        public Guid CaseId { get; set; }

        public DeliverableResponse? CurrentDeliverable { get; set; }
        public RequirementResponse? CurrentRequirement { get; set; }
        public AssumptionResponse? CurrentAssumption { get; set; }
        public DeliverableRiskResponse? CurrentDeliverableRisk { get; set; }
        public ConstrainstResponse? CurrentConstrainst { get; set; }
        public BennefitResponse? CurrentBennefit { get; set; }
        public AcceptanceCriteriaResponse? CurrentAcceptanceCriteria { get; set; }

        public string? Tab { get; set; } = string.Empty;
        public void Open()
        {
            IsNodeOpen = true;
        }
        public void Close()
        {
            IsNodeOpen = false;
        }
        public List<DeliverableResponse> Deliverables { get; set; } = new();
        public List<AssumptionResponse> Assumptions { get; set; } = new();
        public List<ConstrainstResponse> Constrainsts { get; set; } = new();
        public List<RequirementResponse> Requirements { get; set; } = new();
        public List<DeliverableRiskResponse> DeliverableRisks { get; set; } = new();
        public List<BennefitResponse> Bennefits { get; set; } = new();
        public List<AcceptanceCriteriaResponse> AcceptanceCriterias { get; set; } = new();

        DeliverableResponse LastDeliverable =>
            Deliverables.Count == 0 ? null! :
            Deliverables.OrderByDescending(x => x.Order).FirstOrDefault() ?? null!;

        public int LastOrder => LastDeliverable == null ? 1 : LastDeliverable.Order;
    }
}
