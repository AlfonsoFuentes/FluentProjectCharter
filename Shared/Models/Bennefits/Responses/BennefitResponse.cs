namespace Shared.Models.Bennefits.Responses
{
    public class BennefitResponse : BaseResponse
    {

        //public Guid? SubDeliverableId { get; set; }
        public Guid DeliverableId { get; set; }
        public Guid ProjectId { get; set; }
    }
}
