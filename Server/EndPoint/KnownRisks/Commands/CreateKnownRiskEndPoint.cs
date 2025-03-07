using Server.Database.Entities.ProjectManagements;
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
                 
                    var lastorder = await Repository.GetLastOrderAsync<KnownRisk, Project>(Data.ProjectId);

                    var row = KnownRisk.Create(Data.ProjectId,lastorder);


                    await Repository.AddAsync(row);

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
                    .. StaticClass.KnownRisks.Cache.Key(row.Id)
                ];
                return cacheKeys.Where(key => !string.IsNullOrEmpty(key)).ToArray();
            }
        }


        static KnownRisk Map(this CreateKnownRiskRequest request, KnownRisk row)
        {
            row.Name = request.Name;
            return row;
        }

    }

}
