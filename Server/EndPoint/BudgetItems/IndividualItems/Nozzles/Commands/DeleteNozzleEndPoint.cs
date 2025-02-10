using Server.Database.Entities.BudgetItems.Commons;
using Shared.Models.BudgetItems.IndividualItems.Nozzles.Requests;

namespace Server.EndPoint.BudgetItems.IndividualItems.Nozzles.Commands
{
    public static class DeleteNozzleEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Nozzles.EndPoint.Delete, async (DeleteNozzleRequest Data, IRepository Repository) =>
                {
                    var row = await Repository.GetByIdAsync<Nozzle>(Data.Id);
                    if (row == null) { return Result.Fail(Data.NotFound); }
                    await Repository.RemoveAsync(row);

                    List<string> cache = [.. StaticClass.Nozzles.Cache.Key(row.Id), StaticClass.BudgetItems.Cache.GetAll];

                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(cache.ToArray());
                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);

                });
            }
        }




    }

}
