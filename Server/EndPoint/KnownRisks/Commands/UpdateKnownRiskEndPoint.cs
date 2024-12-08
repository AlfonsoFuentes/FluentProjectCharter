using Shared.Models.KnownRisks.Requests;


namespace Server.EndPoint.KnownRisks.Commands
{
    public static class UpdateKnownRiskEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.KnownRisks.EndPoint.Update, async (UpdateKnownRiskRequest Data, IRepository Repository) =>
                {
                    var row = await Repository.GetByIdAsync<KnownRisk>(Data.Id);
                    if (row == null) { return Result.Fail(Data.NotFound); }
                    await Repository.UpdateAsync(row);
                    Data.Map(row);
                    List<string> cache = [..StaticClass.Projects.Cache.Key(Data.ProjectId), .. StaticClass.KnownRisks.Cache.Key(row.Id)];

                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(cache.ToArray());

                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);

                });
            }
        }


        static KnownRisk Map(this UpdateKnownRiskRequest request, KnownRisk row)
        {
            row.Name = request.Name;
            return row;
        }

    }

}
