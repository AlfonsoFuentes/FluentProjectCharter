﻿using DocumentFormat.OpenXml.Spreadsheet;
using Server.Database.Entities.ProjectManagements;
using Shared.Models.BudgetItems.Requests;

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
                        StaticClass.BudgetItems.Cache.GetAll(Data.ProjectId)
                    ];
                    if (Data.GanttTaskId.HasValue)
                    {
                        var deliverable = await Repository.GetByIdAsync<GanttTask>(Data.GanttTaskId.Value);
                        if (deliverable != null)
                        {
                            deliverable.ShowBudgetItems = true;
                            await Repository.UpdateAsync(deliverable);
                        }

                    }
                    foreach (var datarow in Data.DeleteGroup)
                    {
                        var row = await Repository.GetByIdAsync<BudgetItem>(datarow);
                        if (row != null)
                        {
                            await Repository.RemoveAsync(row);

                        }


                    }

                    cache.Add(StaticClass.GanttTasks.Cache.GetAll(Data.ProjectId));

                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(cache.ToArray());
                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);

                });
            }
        }




    }

}
