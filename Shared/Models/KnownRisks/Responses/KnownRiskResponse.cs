namespace Shared.Models.KnownRisks.Responses
{
    public class KnownRiskResponse : BaseResponse
    {

        public Guid? StartId { get; set; }
        public Guid? PlanningId { get; set; }
        public Guid ProjectId { get; set; }
    }
}
