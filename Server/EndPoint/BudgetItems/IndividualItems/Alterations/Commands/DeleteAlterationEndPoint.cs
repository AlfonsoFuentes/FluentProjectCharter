using Server.Database.Entities.ProjectManagements;
using Shared.Models.BudgetItems.IndividualItems.Alterations.Requests;

namespace Server.EndPoint.BudgetItems.IndividualItems.Alterations.Commands
{
    public static class DeleteAlterationEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Alterations.EndPoint.Delete, async (DeleteAlterationRequest Data, IRepository Repository) =>
                {
                    var row = await Repository.GetByIdAsync<Alteration>(Data.Id);
                    if (row == null) { return Result.Fail(Data.NotFound); }

                    var cachekey = GetCacheKeys(row);

                    await Repository.RemoveAsync(row);
                    if (Data.DeliverableId.HasValue)
                    {
                        var deliverable = await Repository.GetByIdAsync<Deliverable>(Data.DeliverableId.Value);
                        if (deliverable != null)
                        {
                            deliverable.ShowBudgetItems = true;
                            await Repository.UpdateAsync(deliverable);
                        }

                    }
                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(cachekey);
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




    }

}
