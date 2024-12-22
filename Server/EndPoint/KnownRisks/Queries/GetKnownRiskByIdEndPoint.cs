using Server.EndPoint.Projects.Queries;
using Shared.Models.KnownRisks.Records;

namespace Server.EndPoint.KnownRisks.Queries
{
    public static class GetKnownRiskByIdEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.KnownRisks.EndPoint.GetById, async (GetKnownRiskByIdRequest request, IQueryRepository Repository) =>
                {
                    //Func<IQueryable<KnownRisk>, IIncludableQueryable<KnownRisk, object>> Includes = x => null!
               
                    //;

                    Expression<Func<KnownRisk, bool>> Criteria = x => x.Id == request.Id;

                    string CacheKey = StaticClass.KnownRisks.Cache.GetById(request.Id);
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
