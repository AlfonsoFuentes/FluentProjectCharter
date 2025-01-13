using Server.Database.Entities.BudgetItems.Commons;
using Shared.Models.BudgetItems.Valves.Requests;

namespace Server.EndPoint.Valves.Commands
{
    public static class DeleteValveEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Valves.EndPoint.Delete, async (DeleteValveRequest Data, IRepository Repository) =>
                {
                    var row = await Repository.GetByIdAsync<Valve>(Data.Id);
                    if (row == null) { return Result.Fail(Data.NotFound); }
                    await Repository.RemoveAsync(row);

                    List<string> cache = [..StaticClass.Projects.Cache.Key(Data.ProjectId), .. StaticClass.Valves.Cache.Key(row.Id)];

                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(cache.ToArray());
                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);

                });
            }
        }




    }

}
