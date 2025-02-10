using Shared.Models.BudgetItems.Taxs.Requests;


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
                List<string> cacheKeys = [
                ..StaticClass.Projects.Cache.Key(row.ProjectId)
                ,StaticClass.BudgetItems.Cache.GetAll,
                ..StaticClass.Taxs.Cache.Key(row.Id)
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
