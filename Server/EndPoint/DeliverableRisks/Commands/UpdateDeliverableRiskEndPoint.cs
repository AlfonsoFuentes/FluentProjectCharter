using Shared.Models.DeliverableRisks.Requests;


namespace Server.EndPoint.DeliverableRisks.Commands
{
    public static class UpdateDeliverableRiskEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.DeliverableRisks.EndPoint.Update, async (UpdateDeliverableRiskRequest Data, IRepository Repository) =>
                {
                    var row = await Repository.GetByIdAsync<DeliverableRisk>(Data.Id);
                    if (row == null) { return Result.Fail(Data.NotFound); }
                    await Repository.UpdateAsync(row);
                    Data.Map(row);
                    List<string> cache = [..StaticClass.Projects.Cache.Key(Data.ProjectId), .. StaticClass.DeliverableRisks.Cache.Key(row.Id)];

                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(cache.ToArray());

                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);


                });
            }
        }


        static DeliverableRisk Map(this UpdateDeliverableRiskRequest request, DeliverableRisk row)
        {
            row.Name = request.Name;
            return row;
        }

    }

}
