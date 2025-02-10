using Server.Database.Entities.BudgetItems.Commons;
using Server.Repositories;
using Shared.Models.BudgetItems.IndividualItems.Foundations.Requests;


namespace Server.EndPoint.BudgetItems.IndividualItems.Foundations.Commands
{
    public static class UpdateFoundationEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Foundations.EndPoint.Update, async (UpdateFoundationRequest Data, IRepository Repository) =>
                {
                    var row = await Repository.GetByIdAsync<Foundation>(Data.Id);
                    if (row == null) { return Result.Fail(Data.NotFound); }
                    await Repository.UpdateAsync(row);
                    Data.Map(row);


                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(GetCacheKeys(row));

                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);

                });
            }
            private string[] GetCacheKeys(BudgetItem row)
            {
                List<string> cacheKeys = [
                ..StaticClass.Projects.Cache.Key(row.ProjectId)
                ,StaticClass.BudgetItems.Cache.GetAll,
                ..StaticClass.Foundations.Cache.Key(row.Id)
                ];
                return cacheKeys.Where(key => !string.IsNullOrEmpty(key)).ToArray();
            }
        }


        static Foundation Map(this UpdateFoundationRequest request, Foundation row)
        {
            row.Name = request.Name;
            row.UnitaryCost = request.UnitaryCost;

            row.Quantity = request.Quantity;
            row.Budget = request.Budget;

            return row;
        }

    }

}
