using Server.Database.Entities.BudgetItems.Commons;
using Server.Database.Entities.ProjectManagements;
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

                    if (Data.DeliverableId.HasValue)
                    {
                        var deliverable = await Repository.GetByIdAsync<Deliverable>(Data.DeliverableId.Value);
                        if (deliverable != null)
                        {
                            deliverable.ShowBudgetItems = true;
                            await Repository.UpdateAsync(deliverable);
                        }

                    }
                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(GetCacheKeys(row));

                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);

                });
            }
            private string[] GetCacheKeys(BudgetItem row)
            {
                List<string> cacheKeys = [
                  ..StaticClass.BudgetItems.Cache.Key(row.Id, row.ProjectId),
                ..StaticClass.Deliverables.Cache.Key(row.DeliverableId!.Value, row.ProjectId)
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
