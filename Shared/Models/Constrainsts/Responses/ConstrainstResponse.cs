namespace Shared.Models.Constrainsts.Responses
{
    public class ConstrainstResponse : BaseResponse
    {

        public Guid? StartId { get; set; }
        public Guid? PlanningId { get; set; }
        public Guid ProjectId { get; set; }
    }
}
