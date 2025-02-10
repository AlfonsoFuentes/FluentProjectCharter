using Server.Database.Entities.BudgetItems.Commons;
using Shared.Models.BudgetItems.IndividualItems.Paintings.Requests;

namespace Server.EndPoint.BudgetItems.IndividualItems.Paintings.Commands
{
    public static class UpdatePaintingEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Paintings.EndPoint.Update, async (UpdatePaintingRequest Data, IRepository Repository) =>
                {
                    var row = await Repository.GetByIdAsync<Painting>(Data.Id);
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
                ..StaticClass.Paintings.Cache.Key(row.Id)
                ];
                return cacheKeys.Where(key => !string.IsNullOrEmpty(key)).ToArray();
            }
        }


        static Painting Map(this UpdatePaintingRequest request, Painting row)
        {
            row.Name = request.Name;
            row.UnitaryCost = request.UnitaryCost;

            row.Quantity = request.Quantity;
            row.Budget = request.Budget;

            return row;
        }

    }

}
