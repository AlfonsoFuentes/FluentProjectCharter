﻿using Server.Database.Entities.ProjectManagements;
using Shared.Models.BudgetItems.Requests;

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
                        .. StaticClass.BudgetItems.Cache.Key(row.Id,row.ProjectId,row.GanttTaskId)];
                    if (Data.GanttTaskId.HasValue)
                    {
                        var deliverable = await Repository.GetByIdAsync<GanttTask>(Data.GanttTaskId.Value);
                        if (deliverable != null)
                        {
                            deliverable.ShowBudgetItems = true;
                            await Repository.UpdateAsync(deliverable);
                        }

                    }
                    cache.Add(StaticClass.GanttTasks.Cache.GetAll(row.ProjectId));
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
