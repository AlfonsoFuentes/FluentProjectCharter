namespace Shared.Models.Requirements.Responses
{
    public class RequirementResponse : BaseResponse
    {


        public Guid? StartId { get; set; }
        public Guid? PlanningId { get; set; }
        public Guid ProjectId { get; set; }

    }
}
