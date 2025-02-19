using Shared.Enums.TasksRelationTypeTypes;
using Shared.Models.Milestones.Records;
using Shared.Models.Milestones.Responses;
namespace Server.EndPoint.Milestones.Queries
{
    public static class GetMilestoneByIdEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Milestones.EndPoint.GetById, async (GetMilestoneByIdRequest request, IQueryRepository Repository) =>
                {
                    Func<IQueryable<Milestone>, IIncludableQueryable<Milestone, object>> Includes = x => x
                    .Include(x => x.Project)

                    .Include(x => x.Dependant!)
                    .Include(x => x.ParentMilestone)
                    ;




                    Expression<Func<Milestone, bool>> Criteria = x => x.Id == request.Id;

                    string CacheKey = StaticClass.Milestones.Cache.GetById(request.Id);
                    var row = await Repository.GetAsync(Cache: CacheKey, Criteria: Criteria, Includes: Includes, OrderBy: x => x.Order);

                    if (row == null)
                    {
                        return Result.Fail(request.NotFound);
                    }

                    var response = row.Map();
                    return Result.Success(response);

                });
            }
        }


        public static MilestoneResponse Map(this Milestone row)
        {

            MilestoneResponse result = new()
            {
                Id = row.Id,
                Name = row.Name,
                StartId = row.StartId,
                PlanningId = row.PlanningId,

                Order = row.Order,

                ProjectId = row.ProjectId,


                DependencyName = row.Dependant == null ? string.Empty : row.Dependant.Name,
                DependencyType = TasksRelationTypeEnum.GetType(row.DependencyType),
                StartDate = row.StartDate,
                EndDate = row.EndDate,
                DurationInput = row.Duration,

                ParentTaskName = row.ParentMilestone == null ? string.Empty : row.ParentMilestone.Name,

            };

            return result;

        }




    }
}
