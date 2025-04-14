using Server.Database.Entities.ProjectManagements;
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
                    Func<IQueryable<Project>, IIncludableQueryable<Project, object>> includes = x => x.Include(p => p.Deliverables);
                    Expression<Func<Project, bool>> criteria = x => x.Id == request.ProjectId;
                    string cacheKey = StaticClass.Deliverables.Cache.GetAll(request.ProjectId);

                    var rows = await repository.GetAsync(Cache: cacheKey, Includes: includes, Criteria: criteria);

                    if (rows == null)
                    {
                        return Result<DeliverableResponseList>.Fail(
                            StaticClass.ResponseMessages.ReponseNotFound(StaticClass.Deliverables.ClassLegend));
                    }

                    var maps = rows.Deliverables.OrderBy(x => x.Order).Select(x => x.Map()).ToList();

                    var response = new DeliverableResponseList
                    {
                        Items = maps,
                        ProjectId = rows.Id,
                    };

                    return Result<DeliverableResponseList>.Success(response);
                });
            }

          


        }
    }
}