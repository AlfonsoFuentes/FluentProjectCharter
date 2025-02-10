namespace Shared.Models.LearnedLessons.Responses
{
    public class LearnedLessonResponse : BaseResponse
    {

        public Guid? StartId { get; set; }
        public Guid? PlanningId { get; set; }
        public Guid ProjectId { get; set; }
    }
}
