using Shared.Models.LearnedLessons.Requests;
using Shared.Models.LearnedLessons.Responses;
using Shared.Models.Milestones.Requests;
using Shared.Models.Milestones.Responses;

namespace Shared.Models.LearnedLessons.Mappers
{
    public static class MilestonesMapper
    {
        public static UpdateMilestoneNameRequest ToUpdateName(this MilestoneResponse response)
        {
            return new()
            {
                Id = response.Id,
                Name = response.Name,
                ProjectId = response.ProjectId,

            };

        }
        //public static ChangeLearnedLessonOrderDowmRequest ToDown(this LearnedLessonResponse response)
        //{
        //    return new()
        //    {
        //        Id = response.Id,
        //        Name = response.Name,
        //        ProjectId = response.ProjectId,
        //        Order = response.Order,


        //    };
        //}
        //public static ChangeLearnedLessonOrderUpRequest ToUp(this LearnedLessonResponse response)
        //{
        //    return new()
        //    {
        //        ProjectId = response.ProjectId,
        //        Id = response.Id,
        //        Name = response.Name,
        //        Order = response.Order,
        //    };
        //}
        //public static UpdateLearnedLessonRequest ToUpdate(this LearnedLessonResponse response)
        //{
        //    return new()
        //    {
        //        Id = response.Id,
        //        Name = response.Name,
        //        ProjectId = response.ProjectId,
        //    };
        //}
        //public static CreateLearnedLessonRequest ToCreate(this LearnedLessonResponse response, Guid? startid, Guid? planid)
        //{
        //    return new()
        //    {

        //        Name = response.Name,
        //        ProjectId = response.ProjectId,
        //        StartId = startid,
        //        PlanningId = planid
        //    };
        //}
    }

}
