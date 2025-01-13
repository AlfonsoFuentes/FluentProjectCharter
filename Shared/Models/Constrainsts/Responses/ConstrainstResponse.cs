namespace Shared.Models.Constrainsts.Responses
{
    public class ConstrainstResponse : BaseResponse
    {

        //public Guid? SubDeliverableId { get; set; }
        public Guid DeliverableId { get; set; }
        public Guid ProjectId { get; set; }
    }
}
