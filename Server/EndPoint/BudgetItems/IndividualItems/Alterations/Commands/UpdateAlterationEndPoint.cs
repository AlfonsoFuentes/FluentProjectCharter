﻿using Server.Database.Entities.BudgetItems.Commons;
using Server.Database.Entities.ProjectManagements;
using Server.Repositories;
using Shared.Models.BudgetItems.IndividualItems.Alterations.Requests;


namespace Server.EndPoint.BudgetItems.IndividualItems.Alterations.Commands
{
    public static class UpdateAlterationEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Alterations.EndPoint.Update, async (UpdateAlterationRequest data, IRepository repository) =>
                {
                    var row = await repository.GetByIdAsync<Alteration>(data.Id);
                    if (row == null)
                    {
                        return Result.Fail(data.NotFound);
                    }
                    if (data.DeliverableId.HasValue)
                    {
                        var deliverable = await repository.GetByIdAsync<Deliverable>(data.DeliverableId.Value);
                        if (deliverable != null)
                        {
                            deliverable.ShowBudgetItems = true;
                            await repository.UpdateAsync(deliverable);
                        }

                    }

                    data.Map(row);
                    await repository.UpdateAsync(row);

                    var result = await repository.Context.SaveChangesAndRemoveCacheAsync(GetCacheKeys(row));

                    return Result.EndPointResult(result, data.Succesfully, data.Fail);
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

        static Alteration Map(this UpdateAlterationRequest request, Alteration row)
        {
            row.Name = request.Name;
            row.UnitaryCost = request.UnitaryCost;
            row.CostCenter = request.CostCenter.Name;
            row.Quantity = request.Quantity;
            row.Budget = request.Budget;

            return row;
        }
    }

}
