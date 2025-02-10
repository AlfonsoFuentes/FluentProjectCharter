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
                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(GetCacheKeys(row));

                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);


                });
            }
            private string[] GetCacheKeys(KnownRisk row)
            {
                List<string> cacheKeys = [
                    .. StaticClass.Projects.Cache.Key(row.ProjectId),
                    .. StaticClass.KnownRisks.Cache.Key(row.Id)
                ];
                return cacheKeys.Where(key => !string.IsNullOrEmpty(key)).ToArray();
            }
        }


        static KnownRisk Map(this UpdateKnownRiskRequest request, KnownRisk row)
        {
            row.Name = request.Name;
            return row;
        }

    }

}
