using Server.EndPoint.Projects.Queries;
using Shared.Models.DecissionCriterias.Records;

namespace Server.EndPoint.DecissionCriterias.Queries
{
    public static class GetDecissionCriteriaByIdEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.DecissionCriterias.EndPoint.GetById, async (GetDecissionCriteriaByIdRequest request, IQueryRepository Repository) =>
                {
                    //Func<IQueryable<DecissionCriteria>, IIncludableQueryable<DecissionCriteria, object>> Includes = x => null!
               
                    //;

                    Expression<Func<DecissionCriteria, bool>> Criteria = x => x.Id == request.Id;

                    string CacheKey = StaticClass.DecissionCriterias.Cache.GetById(request.Id);
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
