using DocumentFormat.OpenXml.Spreadsheet;
using Shared.Models.BudgetItems;

namespace Server.EndPoint.BudgetItems.Commands
{
    public static class DeleteBudgetItemGroupEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.BudgetItems.EndPoint.DeleteGroup, async (DeleteBudgetItemGroupRequest Data, IRepository Repository) =>
                {

                    List<string> cache = [
                        StaticClass.BudgetItems.Cache.GetAll
                    ];

                    foreach (var datarow in Data.DeleteGroup)
                    {
                        var row = await Repository.GetByIdAsync<BudgetItem>(datarow);
                        if (row != null)
                        {
                            await Repository.RemoveAsync(row);

                        }


                    }



                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(cache.ToArray());
                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);

                });
            }
        }




    }

}
