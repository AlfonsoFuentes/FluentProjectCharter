using Shared.Models.SucessfullFactors.Requests;

namespace Server.EndPoint.SucessfullFactors.Commands
{
    public static class DeleteSucessfullFactorEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.SucessfullFactors.EndPoint.Delete, async (DeleteSucessfullFactorRequest Data, IRepository Repository) =>
                {
                    var row = await Repository.GetByIdAsync<SucessfullFactor>(Data.Id);
                    if (row == null) { return Result.Fail(Data.NotFound); }
                    await Repository.RemoveAsync(row);

                    List<string> cache = [..StaticClass.Projects.Cache.Key(Data.ProjectId), .. StaticClass.SucessfullFactors.Cache.Key(row.Id)];

                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(cache.ToArray());

                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);

                });
            }
        }




    }

}
