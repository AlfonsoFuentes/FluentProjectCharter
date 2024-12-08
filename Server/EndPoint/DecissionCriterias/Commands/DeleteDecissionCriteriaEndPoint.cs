



using Server.Database.Entities;
using Shared.Models.DecissionCriterias.Requests;

namespace Server.EndPoint.DecissionCriterias.Commands
{
    public static class DeleteDecissionCriteriaEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.DecissionCriterias.EndPoint.Delete, async (DeleteDecissionCriteriaRequest Data, IRepository Repository) =>
                {
                    var row = await Repository.GetByIdAsync<DecissionCriteria>(Data.Id);
                    if (row == null) { return Result.Fail(Data.NotFound); }
                    await Repository.RemoveAsync(row);

                    List<string> cache = [..StaticClass.Projects.Cache.Key(Data.ProjectId), .. StaticClass.DecissionCriterias.Cache.Key(row.Id)];

                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(cache.ToArray());

                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);

                });
            }
        }




    }

}
