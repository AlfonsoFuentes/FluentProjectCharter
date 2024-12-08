namespace Shared.Models.DeliverableRisks.Responses
{
    public class DeliverableRiskResponse : BaseResponse
    {

        public Guid DeliverableId { get; set; }
        public Guid ProjectId { get; set; }
    }
}
