using Server.Database.Entities.BudgetItems.Commons;
using Shared.Models.BudgetItems.IndividualItems.Electricals.Requests;

namespace Server.EndPoint.BudgetItems.IndividualItems.Electricals.Commands
{
    public static class DeleteElectricalEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Electricals.EndPoint.Delete, async (DeleteElectricalRequest Data, IRepository Repository) =>
                {
                    var row = await Repository.GetByIdAsync<Electrical>(Data.Id);
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
