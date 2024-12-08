using Shared.Models.Assumptions.Responses;
using Shared.Models.Bennefits.Responses;
using Shared.Models.Constrainsts.Responses;
using Shared.Models.DeliverableRisks.Responses;
using Shared.Models.Requirements.Responses;

namespace Shared.Models.Deliverables.Responses
{
    public class DeliverableResponse : BaseResponse
    {
        public Guid ProjectId { get; set; }

        public Guid ScopeId { get; set; }

        public List<RequirementResponse> Requirements { get; set; } = new();
        public List<AssumptionResponse> Assumptions { get; set; } = new();
        public List<DeliverableRiskResponse> DeliverableRisks { get; set; } = new();
        public List<ConstrainstResponse> Constrainsts { get; set; } = new();

        public List<BennefitResponse> Bennefits { get; set; } = new();
    }
}
