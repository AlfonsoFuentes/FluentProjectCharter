using Shared.Models.BudgetItems.Taxs.Requests;

namespace Server.EndPoint.BudgetItems.IndividualItems.Taxs.Commands
{

    public static class CreateTaxEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Taxs.EndPoint.Create, async (CreateTaxRequest data, IRepository repository) =>
                {
                    var project = await GetProject(data.ProjectId, repository);
                    if (project == null) return Result.Fail(data.Fail);

                    int order = GetNextOrder(project);

                    var row = Tax.Create(project.Id);
                    row.Order = order;

                    data.Map(row);
                    foreach (var item in data.TaxItems)
                    {
                        var taxitem = TaxesItem.Create(row.Id, item.BudgetItemId!.Value);
                        await repository.AddAsync(taxitem);

                    }
                    await repository.AddAsync(row);

                    var result = await repository.Context.SaveChangesAndRemoveCacheAsync(GetCacheKeys(row));

                    return Result.EndPointResult(result, data.Succesfully, data.Fail);
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
            private async Task<Project?> GetProject(Guid ProjectId, IRepository repository)
            {
                Func<IQueryable<Project>, IIncludableQueryable<Project, object>> includes = x => x

                    .Include(d => d.BudgetItems)
                    ;

                Expression<Func<Project, bool>> criteria = d => d.Id == ProjectId;

                return await repository.GetAsync(Criteria: criteria, Includes: includes);
            }

            private int GetNextOrder(Project project)
            {
                var alterations = project.BudgetItems.OfType<Tax>();
                return alterations.Any() ? alterations.Max(a => a.Order) + 1 : 1;
            }
        }



        static Tax Map(this CreateTaxRequest request, Tax row)
        {
            row.Name = request.Name;
            row.Percentage = request.Percentage;
            row.Budget = request.Budget;
            return row;
        }

    }

}
