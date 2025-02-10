using Server.Database.Entities.BudgetItems.Commons;
using Server.Repositories;
using Shared.Models.BudgetItems.IndividualItems.EHSs.Requests;


namespace Server.EndPoint.BudgetItems.IndividualItems.EHSs.Commands
{
    public static class UpdateEHSEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.EHSs.EndPoint.Update, async (UpdateEHSRequest Data, IRepository Repository) =>
                {
                    var row = await Repository.GetByIdAsync<EHS>(Data.Id);
                    if (row == null) { return Result.Fail(Data.NotFound); }
                    await Repository.UpdateAsync(row);
                    Data.Map(row);

                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(GetCacheKeys(row));

                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);

                });
            }
            private string[] GetCacheKeys(BudgetItem alteration)
            {
                List<string> cacheKeys = [
                ..StaticClass.Projects.Cache.Key(alteration.ProjectId),
               StaticClass.BudgetItems.Cache.GetAll,
                ..StaticClass.EHSs.Cache.Key(alteration.Id)
                ];
                return cacheKeys.Where(key => !string.IsNullOrEmpty(key)).ToArray();
            }
        }


        static EHS Map(this UpdateEHSRequest request, EHS row)
        {
            row.Name = request.Name;
            row.UnitaryCost = request.UnitaryCost;

            row.Quantity = request.Quantity;
            row.Budget = request.Budget;

            return row;
        }

    }

}
