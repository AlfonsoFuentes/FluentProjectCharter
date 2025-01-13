using Server.Database.Entities.BudgetItems.Commons;
using Shared.Models.BudgetItems.Taxs.Requests;

namespace Server.EndPoint.Taxs.Commands
{
    public static class DeleteTaxEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Taxs.EndPoint.Delete, async (DeleteTaxRequest Data, IRepository Repository) =>
                {
                    var row = await Repository.GetByIdAsync<Tax>(Data.Id);
                    if (row == null) { return Result.Fail(Data.NotFound); }
                    await Repository.RemoveAsync(row);

                    List<string> cache = [..StaticClass.Projects.Cache.Key(Data.ProjectId), .. StaticClass.Taxs.Cache.Key(row.Id)];

                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(cache.ToArray());
                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);

                });
            }
        }




    }

}
