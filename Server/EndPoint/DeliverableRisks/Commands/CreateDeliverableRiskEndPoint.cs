using Shared.Models.DeliverableRisks.Requests;

namespace Server.EndPoint.DeliverableRisks.Commands
{

    public static class CreateDeliverableRiskEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.DeliverableRisks.EndPoint.Create, async (CreateDeliverableRiskRequest Data, IRepository Repository) =>
                {
                    var row = DeliverableRisk.Create(Data.DeliverableId);

                    await Repository.AddAsync(row);

                    Data.Map(row);
                    List<string> cache = [..StaticClass.Projects.Cache.Key(Data.ProjectId), .. StaticClass.DeliverableRisks.Cache.Key(row.Id)];

                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(cache.ToArray());
                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);


                });


            }
        }


        static DeliverableRisk Map(this CreateDeliverableRiskRequest request, DeliverableRisk row)
        {
            row.Name = request.Name;
            return row;
        }

    }

}
