using Server.Database.Entities.BudgetItems.Commons;
using Shared.Models.BudgetItems.IndividualItems.EHSs.Requests;
using static Shared.StaticClasses.StaticClass;

namespace Server.EndPoint.BudgetItems.IndividualItems.EHSs.Commands
{
    public static class DeleteEHSEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.EHSs.EndPoint.Delete, async (DeleteEHSRequest Data, IRepository Repository) =>
                {
                    var row = await Repository.GetByIdAsync<EHS>(Data.Id);
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
