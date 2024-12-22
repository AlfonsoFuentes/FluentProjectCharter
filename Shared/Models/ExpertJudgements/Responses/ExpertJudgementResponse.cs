using Shared.Models.StakeHolders.Responses;

namespace Shared.Models.ExpertJudgements.Responses
{
    public class ExpertJudgementResponse : BaseResponse
    {
        public Guid ProjectId { get; set; }

        public Guid CaseId { get; set; }
        public StakeHolderResponse? Expert { get; set; }
    }
}
