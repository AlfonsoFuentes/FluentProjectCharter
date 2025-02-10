using DocumentFormat.OpenXml.Spreadsheet;
using Server.Database.Entities.BudgetItems.Commons;
using Shared.Models.BudgetItems.IndividualItems.Alterations.Requests;

namespace Server.EndPoint.BudgetItems.IndividualItems.Alterations.Commands
{
    public static class CreateAlterationEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Alterations.EndPoint.Create, async (CreateAlterationRequest data, IRepository repository) =>
                {
                    var project = await GetProject(data.ProjectId, repository);
                    if (project == null) return Result.Fail(data.Fail);

                    int order = GetNextOrder(project);

                    var row = Alteration.Create(project.Id);
                    row.Order = order;

                    data.Map(row);
                    await repository.AddAsync(row);

                    var result = await repository.Context.SaveChangesAndRemoveCacheAsync(GetCacheKeys(row));

                    return Result.EndPointResult(result, data.Succesfully, data.Fail);
                });
            }
            private string[] GetCacheKeys(BudgetItem alteration)
            {
                List<string> cacheKeys = [
                ..StaticClass.Projects.Cache.Key(alteration.ProjectId),
                 StaticClass.BudgetItems.Cache.GetAll,
                ..StaticClass.Alterations.Cache.Key(alteration.Id)
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
                var alterations = project.BudgetItems.OfType<Alteration>();
                return alterations.Any() ? alterations.Max(a => a.Order) + 1 : 1;
            }
        }

        static Alteration Map(this CreateAlterationRequest request, Alteration alteration)
        {
            alteration.Name = request.Name;
            alteration.UnitaryCost = request.UnitaryCost;
            alteration.CostCenter = request.CostCenter.Name;
            alteration.Quantity = request.Quantity;
            alteration.Budget = request.Budget;
            return alteration;
        }
    }
}

