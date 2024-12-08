


using Server.Database.Entities;
using Shared.Models.SucessfullFactors.Requests;
using System.Threading;

namespace Server.EndPoint.SucessfullFactors.Commands
{

    public static class CreateSucessfullFactorEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.SucessfullFactors.EndPoint.Create, async (CreateSucessfullFactorRequest Data, IRepository Repository) =>
                {
                    var row = SucessfullFactor.Create(Data.CaseId);

                    await Repository.AddAsync(row);

                    Data.Map(row);
                    List<string> cache = [..StaticClass.Projects.Cache.Key(Data.ProjectId), .. StaticClass.SucessfullFactors.Cache.Key(row.Id)];

                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(cache.ToArray());

                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);


                });


            }
        }


        static SucessfullFactor Map(this CreateSucessfullFactorRequest request, SucessfullFactor row)
        {
            row.Name = request.Name;
            return row;
        }

    }

}
