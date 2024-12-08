using Shared.Models.KnownRisks.Requests;

namespace Server.EndPoint.KnownRisks.Commands
{
    public static class DeleteKnownRiskEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.KnownRisks.EndPoint.Delete, async (DeleteKnownRiskRequest Data, IRepository Repository) =>
                {
                    var row = await Repository.GetByIdAsync<KnownRisk>(Data.Id);
                    if (row == null) { return Result.Fail(Data.NotFound); }
                    await Repository.RemoveAsync(row);

                    List<string> cache = [..StaticClass.Projects.Cache.Key(Data.ProjectId), .. StaticClass.KnownRisks.Cache.Key(row.Id)];

                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(cache.ToArray());
                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);

                });
            }
        }




    }

}
