using Server.EndPoint.Projects.Queries;
using Shared.Models.Cases.Records;

namespace Server.EndPoint.Cases.Queries
{
    public static class GetCaseByIdEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Cases.EndPoint.GetById, async (GetCaseByIdRequest request, IQueryRepository Repository) =>
                {
                    Func<IQueryable<Case>, IIncludableQueryable<Case, object>> Includes = x => x.Include(x => x.OrganizationStrategy!);

                    Expression<Func<Case, bool>> Criteria = x => x.Id == request.Id;

                    string CacheKey = StaticClass.Cases.Cache.GetById(request.Id);
                    var row = await Repository.GetAsync(Cache: CacheKey, Criteria: Criteria, Includes: Includes);

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
