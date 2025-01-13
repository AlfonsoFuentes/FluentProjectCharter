namespace Shared.Models.Assumptions.Responses
{
    public class AssumptionResponse : BaseResponse
    {
        //public Guid? SubDeliverableId { get; set; }
        public Guid DeliverableId { get; set; }
        public Guid ProjectId { get; set; }
    }
}
