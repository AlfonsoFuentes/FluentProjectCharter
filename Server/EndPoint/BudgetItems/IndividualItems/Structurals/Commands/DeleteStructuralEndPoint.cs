using Server.Database.Entities.BudgetItems.Commons;
using Shared.Models.BudgetItems.Structurals.Requests;

namespace Server.EndPoint.BudgetItems.IndividualItems.Structurals.Commands
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
                    var cachekeys = GetCacheKeys(row);

                    await Repository.RemoveAsync(row);

                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(cachekeys);

                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);

                });

            }
            private string[] GetCacheKeys(BudgetItem row)
            {
                List<string> cacheKeys = [
                ..StaticClass.Projects.Cache.Key(row.ProjectId),
                StaticClass.BudgetItems.Cache.GetAll

                ];
                return cacheKeys.Where(key => !string.IsNullOrEmpty(key)).ToArray();
            }
        }




    }

}
