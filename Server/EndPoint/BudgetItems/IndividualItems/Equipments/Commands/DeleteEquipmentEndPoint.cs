using Shared.Models.BudgetItems.IndividualItems.Equipments.Requests;

namespace Server.EndPoint.BudgetItems.IndividualItems.Equipments.Commands
{
    public static class DeleteEquipmentEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Equipments.EndPoint.Delete, async (DeleteEquipmentRequest Data, IRepository Repository) =>
                {
                    var row = await Repository.GetByIdAsync<Equipment>(Data.Id);
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
