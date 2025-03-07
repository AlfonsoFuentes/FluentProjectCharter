using Server.Database.Entities.ProjectManagements;
using Server.Repositories;
using Shared.Models.Backgrounds.Records;

namespace Server.EndPoint.BackGrounds.Queries
{
    public static class GetAllBackGroundEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.BackGrounds.EndPoint.GetAll, async (BackGroundGetAll request, IQueryRepository Repository) =>
                {
                    Func<IQueryable<Project>, IIncludableQueryable<Project, object>> includes = x => x.Include(p => p.BackGrounds);
                    Expression<Func<Project, bool>> criteria = x => x.Id == request.ProjectId;
                    string cacheKey = StaticClass.BackGrounds.Cache.GetAll;

                    var rows = await Repository.GetAsync(Cache: cacheKey, Includes: includes, Criteria: criteria);

                    if (rows == null)
                    {
                        return Result<BackGroundResponseList>.Fail(
                        StaticClass.ResponseMessages.ReponseNotFound(StaticClass.BackGrounds.ClassLegend));
                    }

                    var maps = rows.BackGrounds.Select(x => x.Map()).ToList();


                    BackGroundResponseList response = new BackGroundResponseList()
                    {
                        ProjectName = rows.Name,

                        Items = maps,

                    };
                    return Result<BackGroundResponseList>.Success(response);

                });
            }
        }
    }
}