using Server.EndPoint.Projects.Queries;
using Shared.Models.Bennefits.Records;

namespace Server.EndPoint.Bennefits.Queries
{
    public static class GetBennefitByIdEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Bennefits.EndPoint.GetById, async (GetBennefitByIdRequest request, IQueryRepository Repository) =>
                {
                   
                    Expression<Func<Bennefit, bool>> Criteria = x => x.Id == request.Id;

                    string CacheKey = StaticClass.Bennefits.Cache.GetById(request.Id);
                    var row = await Repository.GetAsync(Cache: CacheKey, Criteria: Criteria);

                    if (row == null)
                    {
                        return Result.Fail(request.NotFound);
                    }

                    var response = row.Map(request.ProjectId);
                    return Result.Success(response);

                });
            }
        }



    }
}
