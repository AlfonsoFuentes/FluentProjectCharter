using Server.EndPoint.Projects.Queries;
using Shared.Models.HighLevelRequirements.Records;

namespace Server.EndPoint.HighLevelRequirements.Queries
{
    public static class GetHighLevelRequirementByIdEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.HighLevelRequirements.EndPoint.GetById, async (GetHighLevelRequirementByIdRequest request, IQueryRepository Repository) =>
                {
                    

                    Expression<Func<HighLevelRequirement, bool>> Criteria = x => x.Id == request.Id;

                    string CacheKey = StaticClass.HighLevelRequirements.Cache.GetById(request.Id);
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
