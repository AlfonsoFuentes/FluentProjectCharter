using Server.Database.Entities.ProjectManagements;
using Shared.Models.BudgetItems.IndividualItems.EHSs.Requests;

namespace Server.EndPoint.BudgetItems.IndividualItems.EHSs.Commands
{
    public static class CreateEHSEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.EHSs.EndPoint.Create, async (CreateEHSRequest data, IRepository repository) =>
                {
                    var project = await GetProject(data.ProjectId, repository);
                    if (project == null) return Result.Fail(data.Fail);

                    int order = GetNextOrder(project);

                    var row = EHS.Create(project.Id, data.GanttTaskId);
                    if (data.GanttTaskId.HasValue)
                    {
                        var deliverable = await repository.GetByIdAsync<GanttTask>(data.GanttTaskId.Value);
                        if (deliverable != null)
                        {
                            deliverable.ShowBudgetItems = true;
                            await repository.UpdateAsync(deliverable);
                        }
                    }
                    row.Order = order;

                    data.Map(row);
                    await repository.AddAsync(row);

                    var result = await repository.Context.SaveChangesAndRemoveCacheAsync(GetCacheKeys(row));

                    return Result.EndPointResult(result, data.Succesfully, data.Fail);
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
                var alterations = project.BudgetItems.OfType<EHS>();
                return alterations.Any() ? alterations.Max(a => a.Order) + 1 : 1;
            }
        }

        static EHS Map(this CreateEHSRequest request, EHS row)
        {
            row.Name = request.Name;
            row.UnitaryCost = request.UnitaryCost;

            row.Quantity = request.Quantity;
            row.Budget = request.Budget;
            return row;

        }
    }



}
