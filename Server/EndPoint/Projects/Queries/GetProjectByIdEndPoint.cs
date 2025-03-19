using Microsoft.EntityFrameworkCore.Query;

namespace Server.EndPoint.Projects.Queries
{
    public static class GetProjectByIdEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Projects.EndPoint.GetById, async (GetProjectByIdRequest request, IQueryRepository Repository) =>
                {
                    //Func<IQueryable<Project>, IIncludableQueryable<Project, object>> Includes = x => x
                    //  .Include(x => x.Manager!)
                    // .Include(x => x.Sponsor!);

                    Expression<Func<Project, bool>> Criteria = x => x.Id == request.Id;

                    string CacheKey = StaticClass.Projects.Cache.GetById(request.Id);
                    var row = await Repository.GetAsync(Cache: CacheKey, Criteria: Criteria);

                    if (row == null)
                    {
                        return Result.Fail(request.NotFound);
                    }

                    var response = row.Map();
                    return Result.Success(response);

                });
            }
        }



    }
}
