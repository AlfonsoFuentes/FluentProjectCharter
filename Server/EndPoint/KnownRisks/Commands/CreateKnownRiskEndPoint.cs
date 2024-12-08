


using Server.Database.Entities;
using Shared.Models.KnownRisks.Requests;
using System.Threading;

namespace Server.EndPoint.KnownRisks.Commands
{

    public static class CreateKnownRiskEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.KnownRisks.EndPoint.Create, async (CreateKnownRiskRequest Data, IRepository Repository) =>
                {
                    var row = KnownRisk.Create(Data.CaseId);

                    await Repository.AddAsync(row);

                    Data.Map(row);
                    List<string> cache = [..StaticClass.Projects.Cache.Key(Data.ProjectId), .. StaticClass.KnownRisks.Cache.Key(row.Id)];

                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(cache.ToArray());
                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);


                });


            }
        }


        static KnownRisk Map(this CreateKnownRiskRequest request, KnownRisk row)
        {
            row.Name = request.Name;
            return row;
        }

    }

}
