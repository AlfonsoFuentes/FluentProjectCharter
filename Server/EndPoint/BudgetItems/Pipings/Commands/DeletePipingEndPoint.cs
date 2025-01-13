using Server.Database.Entities.BudgetItems.Commons;
using Shared.Models.BudgetItems.Pipings.Requests;

namespace Server.EndPoint.Pipings.Commands
{
    public static class DeletePipingEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Pipings.EndPoint.Delete, async (DeletePipingRequest Data, IRepository Repository) =>
                {
                    var row = await Repository.GetByIdAsync<Isometric>(Data.Id);
                    if (row == null) { return Result.Fail(Data.NotFound); }
                    await Repository.RemoveAsync(row);

                    List<string> cache = [..StaticClass.Projects.Cache.Key(Data.ProjectId), .. StaticClass.Pipings.Cache.Key(row.Id)];

                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(cache.ToArray());
                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);

                });
            }
        }




    }

}
