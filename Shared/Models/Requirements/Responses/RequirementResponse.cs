namespace Shared.Models.Requirements.Responses
{
    public class RequirementResponse : BaseResponse
    {

        //public Guid? SubDeliverableId { get; set; }
        public Guid DeliverableId { get; set; }
        public Guid ProjectId { get; set; }

    }
}
