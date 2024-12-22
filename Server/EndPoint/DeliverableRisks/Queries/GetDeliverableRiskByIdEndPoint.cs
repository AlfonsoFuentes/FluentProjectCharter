using Server.EndPoint.Projects.Queries;
using Shared.Models.DeliverableRisks.Records;

namespace Server.EndPoint.DeliverableRisks.Queries
{
    public static class GetDeliverableRiskByIdEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.DeliverableRisks.EndPoint.GetById, async (GetDeliverableRiskByIdRequest request, IQueryRepository Repository) =>
                {
                    //Func<IQueryable<DeliverableRisk>, IIncludableQueryable<DeliverableRisk, object>> Includes = x => null!
               
                    //;

                    Expression<Func<DeliverableRisk, bool>> Criteria = x => x.Id == request.Id;

                    string CacheKey = StaticClass.DeliverableRisks.Cache.GetById(request.Id);
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
