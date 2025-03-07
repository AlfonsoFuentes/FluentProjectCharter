using Server.Database.Entities.ProjectManagements;
using Shared.Models.BudgetItems.IndividualItems.Taxs.Requests;

namespace Server.EndPoint.BudgetItems.IndividualItems.Taxs.Commands
{
    public static class UpdateTaxEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Taxs.EndPoint.Update, async (UpdateTaxRequest Data, IRepository Repository) =>
                {
                    Func<IQueryable<Tax>, IIncludableQueryable<Tax, object>> Includes = x => x.Include(x => x.TaxesItems);
                    Expression<Func<Tax, bool>> Criteria = x => x.Id == Data.Id;

                    var row = await Repository.GetAsync(Criteria: Criteria, Includes: Includes);


                    if (row == null) { return Result.Fail(Data.NotFound); }
                    await Repository.UpdateAsync(row);
                    if (Data.GanttTaskId.HasValue)
                    {
                        var deliverable = await Repository.GetByIdAsync<GanttTask>(Data.GanttTaskId.Value);
                        if (deliverable != null)
                        {
                            deliverable.ShowBudgetItems = true;
                            await Repository.UpdateAsync(deliverable);
                        }

                    }
                    await UpdateTaxesItems(Repository, row, Data);
                    Data.Map(row);

                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(GetCacheKeys(row));

                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);

                });
            }
            private string[] GetCacheKeys(BudgetItem row)
            {
                var deliverable = row.GanttTaskId.HasValue ? StaticClass.GanttTasks.Cache.Key(row.GanttTaskId!.Value, row.ProjectId) : new[] { string.Empty };
                List<string> cacheKeys = [
                 ..StaticClass.BudgetItems.Cache.Key(row.Id, row.ProjectId, row.GanttTaskId),
                 ..deliverable
                ];
                return cacheKeys.Where(key => !string.IsNullOrEmpty(key)).ToArray();
            }
            async Task UpdateTaxesItems(IRepository Repository, Tax row, UpdateTaxRequest Data)
            {
                if (Data.TaxItems != null)
                {
                    var taxesitems = row.TaxesItems.ToList();
                    foreach (var taxitem in Data.TaxItems)
                    {
                        var taxitemRow = taxesitems.FirstOrDefault(x => x.SelectedId == taxitem.BudgetItemId);
                        if (taxitemRow == null)
                        {
                            taxitemRow = TaxesItem.Create(row.Id, taxitem.BudgetItemId!.Value);


                            await Repository.AddAsync(taxitemRow);
                        }

                    }
                    var nozzlesToRemove = taxesitems.Where(x => !Data.TaxItems.Select(x => x.BudgetItemId).Contains(x.SelectedId)).ToList();
                    await Repository.RemoveRangeAsync(nozzlesToRemove);
                }
                else
                {
                    await Repository.RemoveRangeAsync(row.TaxesItems.ToList());
                }
            }

        }


        static Tax Map(this UpdateTaxRequest request, Tax row)
        {
            row.Name = request.Name;

            row.Budget = request.Budget;
            row.Percentage = request.Percentage;
            return row;
        }

    }

}
