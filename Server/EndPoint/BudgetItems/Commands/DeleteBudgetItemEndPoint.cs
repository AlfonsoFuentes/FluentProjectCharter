using Server.Database.Entities.BudgetItems.Commons;
using Server.Database.Entities.ProjectManagements;
using Shared.Models.BudgetItems;
using Shared.Models.BudgetItems.Valves.Requests;
using System.Collections.Generic;

namespace Server.EndPoint.BudgetItems.Commands
{
    public static class DeleteBudgetItemEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.BudgetItems.EndPoint.Delete, async (DeleteBudgetItemRequest Data, IRepository Repository) =>
                {
                    var row = await Repository.GetByIdAsync<BudgetItem>(Data.Id);
                    if (row == null) { return Result.Fail(Data.NotFound); }


                    List<string> cache = [
                        .. StaticClass.BudgetItems.Cache.Key(row.Id,row.ProjectId,row.DeliverableId)];
                    if (Data.DeliverableId.HasValue)
                    {
                        var deliverable = await Repository.GetByIdAsync<Deliverable>(Data.DeliverableId.Value);
                        if (deliverable != null)
                        {
                            deliverable.ShowBudgetItems = true;
                            await Repository.UpdateAsync(deliverable);
                        }

                    }
                    cache.Add(StaticClass.Deliverables.Cache.GetAll(row.ProjectId));
                    await Repository.RemoveAsync(row);

                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(cache.ToArray());
                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);

                });
            }
        }




    }

}
