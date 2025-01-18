using Server.Database.Entities.BudgetItems.Commons;
using Shared.Models.BudgetItems.Pipes.Requests;

namespace Server.EndPoint.Pipes.Commands
{
    public static class DeletePipeEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Pipes.EndPoint.Delete, async (DeletePipeRequest Data, IRepository Repository) =>
                {
                    var row = await Repository.GetByIdAsync<Pipe>(Data.Id);
                    if (row == null) { return Result.Fail(Data.NotFound); }
                    await Repository.RemoveAsync(row);

                    List<string> cache = [..StaticClass.Projects.Cache.Key(Data.ProjectId), .. StaticClass.Pipes.Cache.Key(row.Id)];

                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(cache.ToArray());
                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);

                });
            }
        }




    }

}
