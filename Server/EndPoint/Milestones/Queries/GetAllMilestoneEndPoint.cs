using Shared.Models.Milestones.Records;
using Shared.Models.Milestones.Responses;

namespace Server.EndPoint.Milestones.Queries
{
    public static class GetAllMilestoneEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Milestones.EndPoint.GetAll, async (MilestoneGetAll request, IQueryRepository repository) =>
                {
                    var rows = await GetMilestoneAsync(request, repository);

                    if (rows == null)
                    {
                        return Result<MilestoneResponseList>.Fail(
                            StaticClass.ResponseMessages.ReponseNotFound(StaticClass.Milestones.ClassLegend));
                    }

                    var maps = FilterMilestone(request, rows);

                    var response = new MilestoneResponseList
                    {
                        Items = maps
                    };

                    return Result<MilestoneResponseList>.Success(response);
                });
            }

            private static async Task<List<Milestone>?> GetMilestoneAsync(MilestoneGetAll request, IQueryRepository repository)
            {
                Func<IQueryable<Milestone>, IIncludableQueryable<Milestone, object>> Includes = x => x
                    .Include(x => x.Project)

                    .Include(x => x.Dependant!)
                    .Include(x => x.ParentMilestone)
                    ;

               
                Expression<Func<Milestone, bool>> criteria = x => x.ProjectId == request.ProjectId;
                string cacheKey = StaticClass.Milestones.Cache.GetAll;

                return await repository.GetAllAsync(Cache: cacheKey, Includes: Includes, Criteria: criteria);
            }

            private static List<MilestoneResponse> FilterMilestone(MilestoneGetAll request, List<Milestone> rows)
            {
                return rows.OrderBy(x => x.Order).Select(ac => ac.Map()).ToList();
            }
        }
    }

}