using Server.Database.Entities.BudgetItems.Commons;
using Server.Database.Entities.ProjectManagements;
using Shared.Models.BudgetItems.Structurals.Requests;


namespace Server.EndPoint.BudgetItems.IndividualItems.Structurals.Commands
{
    public static class UpdateStructuralEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Structurals.EndPoint.Update, async (UpdateStructuralRequest Data, IRepository Repository) =>
                {
                    var row = await Repository.GetByIdAsync<Structural>(Data.Id);
                    if (row == null) { return Result.Fail(Data.NotFound); }
                    await Repository.UpdateAsync(row);
                    Data.Map(row);
                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(GetCacheKeys(row));
                    if (Data.DeliverableId.HasValue)
                    {
                        var deliverable = await Repository.GetByIdAsync<Deliverable>(Data.DeliverableId.Value);
                        if (deliverable != null)
                        {
                            deliverable.ShowBudgetItems = true;
                            await Repository.UpdateAsync(deliverable);
                        }

                    }
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


        static Structural Map(this UpdateStructuralRequest request, Structural row)
        {
            row.Name = request.Name;
            row.UnitaryCost = request.UnitaryCost;

            row.Quantity = request.Quantity;
            row.Budget = request.Budget;

            return row;
        }

    }

}
