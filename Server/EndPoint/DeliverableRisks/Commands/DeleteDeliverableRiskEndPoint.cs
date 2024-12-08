



using Server.Database.Entities;
using Shared.Models.DeliverableRisks.Requests;

namespace Server.EndPoint.DeliverableRisks.Commands
{
    public static class DeleteDeliverableRiskEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.DeliverableRisks.EndPoint.Delete, async (DeleteDeliverableRiskRequest Data, IRepository Repository) =>
                {
                    var row = await Repository.GetByIdAsync<DeliverableRisk>(Data.Id);
                    if (row == null) { return Result.Fail(Data.NotFound); }
                    await Repository.RemoveAsync(row);

                    List<string> cache = [..StaticClass.Projects.Cache.Key(Data.ProjectId), .. StaticClass.DeliverableRisks.Cache.Key(row.Id)];

                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(cache.ToArray());
                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);

                });
            }
        }




    }

}
