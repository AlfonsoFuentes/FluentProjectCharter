namespace Shared.Models.Acquisitions.Responses
{
    public class AcquisitionResponse : BaseResponse
    {

        public Guid? StartId { get; set; }
        public Guid? PlanningId { get; set; }
        public Guid ProjectId { get; set; }
    }
}
