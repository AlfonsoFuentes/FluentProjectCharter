using Server.Database.Entities.BudgetItems.Commons;
using Shared.Models.BudgetItems.Structurals.Requests;

namespace Server.EndPoint.Structurals.Commands
{
    public static class DeleteStructuralEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Structurals.EndPoint.Delete, async (DeleteStructuralRequest Data, IRepository Repository) =>
                {
                    var row = await Repository.GetByIdAsync<Structural>(Data.Id);
                    if (row == null) { return Result.Fail(Data.NotFound); }
                    await Repository.RemoveAsync(row);

                    List<string> cache = [..StaticClass.Projects.Cache.Key(Data.ProjectId), .. StaticClass.Structurals.Cache.Key(row.Id)];

                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(cache.ToArray());
                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);

                });
            }
        }




    }

}
