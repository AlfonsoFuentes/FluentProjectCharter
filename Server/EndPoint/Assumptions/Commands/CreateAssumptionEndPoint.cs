


using Server.Database.Entities;
using Shared.Models.Assumptions.Requests;
using System.Threading;

namespace Server.EndPoint.Assumptions.Commands
{

    public static class CreateAssumptionEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Assumptions.EndPoint.Create, async (CreateAssumptionRequest Data, IRepository Repository) =>
                {
                    if (!Data.PlanningId.HasValue && !Data.StartId.HasValue)
                        return Result.Fail();
                    var lastorder = await Repository.GetLastOrderAsync<Assumption, Project>(Data.ProjectId);

                    var row = Assumption.Create(Data.ProjectId, Data.StartId, Data.PlanningId, lastorder);

                    await Repository.AddAsync(row);

                    Data.Map(row);


                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(GetCacheKeys(row));


                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);


                });


            }
            private string[] GetCacheKeys(Assumption row)
            {
                List<string> cacheKeys = [.. StaticClass.Projects.Cache.Key(row.ProjectId),

                    .. StaticClass.Assumptions.Cache.Key(row.Id)
                ];
                return cacheKeys.Where(key => !string.IsNullOrEmpty(key)).ToArray();
            }
        }


        static Assumption Map(this CreateAssumptionRequest request, Assumption row)
        {
            row.Name = request.Name;
            return row;
        }

    }

}
