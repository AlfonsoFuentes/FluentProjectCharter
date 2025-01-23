


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
                    var row = Assumption.Create(Data.ProjectId,Data.ScopeId);

                    await Repository.AddAsync(row);

                    Data.Map(row);
                    List<string> cache = [..StaticClass.Projects.Cache.Key(Data.ProjectId), .. StaticClass.Assumptions.Cache.Key(row.Id)];

                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(cache.ToArray());


                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);


                });


            }
        }


        static Assumption Map(this CreateAssumptionRequest request, Assumption row)
        {
            row.Name = request.Name;
            return row;
        }

    }

}
