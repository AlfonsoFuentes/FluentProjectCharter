using Shared.Models.BudgetItems.Valves.Requests;

namespace Server.EndPoint.BudgetItems.IndividualItems.Valves.Commands
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
                 ..StaticClass.BudgetItems.Cache.Key(row.Id)

                ];
                return cacheKeys.Where(key => !string.IsNullOrEmpty(key)).ToArray();
            }
        }




    }

}
