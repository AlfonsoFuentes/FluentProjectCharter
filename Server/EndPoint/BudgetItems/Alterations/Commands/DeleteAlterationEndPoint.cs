using Server.Database.Entities.BudgetItems.Commons;
using Shared.Models.BudgetItems.Alterations.Requests;

namespace Server.EndPoint.Alterations.Commands
{
    public static class DeleteAlterationEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Alterations.EndPoint.Delete, async (DeleteAlterationRequest Data, IRepository Repository) =>
                {
                    var row = await Repository.GetByIdAsync<Alteration>(Data.Id);
                    if (row == null) { return Result.Fail(Data.NotFound); }
                    await Repository.RemoveAsync(row);

                    List<string> cache = [..StaticClass.Projects.Cache.Key(Data.ProjectId), .. StaticClass.Alterations.Cache.Key(row.Id)];

                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(cache.ToArray());
                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);

                });
            }
        }




    }

}
