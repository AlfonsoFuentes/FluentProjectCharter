using Server.EndPoint.Deliverables.Queries;
using Shared.Models.Deliverables.Records;

namespace Server.EndPoint.Deliverables.Queries
{
    public static class GetAllDeliverableEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Deliverables.EndPoint.GetAll, async (DeliverableGetAll request, IQueryRepository repository) =>
                {
                    var rows = await GetDeliverableAsync(request, repository);

                    if (rows == null)
                    {
                        return Result<DeliverableResponseList>.Fail(
                            StaticClass.ResponseMessages.ReponseNotFound(StaticClass.Deliverables.ClassLegend));
                    }

                    var maps = FilterDeliverable(request, rows);

                    var response = new DeliverableResponseList
                    {
                        Items = maps
                    };

                    return Result<DeliverableResponseList>.Success(response);
                });
            }

            private static async Task<Project?> GetDeliverableAsync(DeliverableGetAll request, IQueryRepository repository)
            {
                Func<IQueryable<Project>, IIncludableQueryable<Project, object>> includes = x => x.Include(p => p.Deliverables);
                Expression<Func<Project, bool>> criteria = x => x.Id == request.ProjectId;
                string cacheKey = StaticClass.Deliverables.Cache.GetAll;

                return await repository.GetAsync(Cache: cacheKey, Includes: includes, Criteria: criteria);
            }

            private static List<DeliverableResponse> FilterDeliverable(DeliverableGetAll request, Project project)
            {
                return  project.Deliverables.OrderBy(x => x.Order).Select(ac => ac.Map()).ToList();
            }
        }
    }
}