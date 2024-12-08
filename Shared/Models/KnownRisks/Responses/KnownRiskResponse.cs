namespace Shared.Models.KnownRisks.Responses
{
    public class KnownRiskResponse : BaseResponse
    {
        public Guid ProjectId { get; set; }

        public Guid CaseId { get; set; }
    }
}
