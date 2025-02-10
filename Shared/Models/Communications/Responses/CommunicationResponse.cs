namespace Shared.Models.Communications.Responses
{
    public class CommunicationResponse : BaseResponse
    {

        public Guid? StartId { get; set; }
        public Guid? PlanningId { get; set; }
        public Guid ProjectId { get; set; }
    }
}
