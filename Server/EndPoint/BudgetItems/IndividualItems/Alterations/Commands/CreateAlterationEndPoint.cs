using DocumentFormat.OpenXml.Spreadsheet;
using Org.BouncyCastle.Asn1.Cms;
using Server.Database.Entities.BudgetItems.Commons;
using Server.Database.Entities.ProjectManagements;
using Shared.Models.BudgetItems.IndividualItems.Alterations.Requests;
using Shared.Models.BudgetItems.IndividualItems.Alterations.Responses;

namespace Server.EndPoint.BudgetItems.IndividualItems.Alterations.Commands
{
    //public static class CreateAlterationEndPoint
    //{
    //    public class EndPoint : IEndPoint
    //    {
    //        public void MapEndPoint(IEndpointRouteBuilder app)
    //        {
    //            app.MapPost(StaticClass.Alterations.EndPoint.Create, async (AlterationResponse data, IRepository repository) =>
    //            {
    //                var project = await GetProject(data.ProjectId, repository);
    //                if (project == null) return Result.Fail(data.Fail);

    //                int order = GetNextOrder(project);

    //                var row = Alteration.Create(project.Id, data.GanttTaskId);
    //                row.Order = order;
    //                if (data.GanttTaskId.HasValue)
    //                {
    //                    var deliverable = await repository.GetByIdAsync<GanttTask>(data.GanttTaskId.Value);
    //                    if (deliverable != null)
    //                    {
    //                        deliverable.ShowBudgetItems = true;
    //                        await repository.UpdateAsync(deliverable);
    //                    }
    //                }

    //                data.Map(row);
    //                await repository.AddAsync(row);

    //                var result = await repository.Context.SaveChangesAndRemoveCacheAsync(GetCacheKeys(row));

    //                return Result.EndPointResult(result, data.Succesfully, data.Fail);
    //            });
    //        }
    //        private string[] GetCacheKeys(BudgetItem row)
    //        {
    //            var deliverable = row.GanttTaskId.HasValue ? StaticClass.GanttTasks.Cache.Key(row.GanttTaskId!.Value, row.ProjectId) : new[] { string.Empty };
    //            var budgetitems = StaticClass.BudgetItems.Cache.Key(row.Id, row.ProjectId, row.GanttTaskId);
    //            var items = StaticClass.Alterations.Cache.Key(row.Id, row.ProjectId);
    //            List<string> cacheKeys = [
    //                 ..budgetitems,
    //                 ..items,
    //                 ..deliverable,

    //            ];

    //            return cacheKeys.Where(key => !string.IsNullOrEmpty(key)).ToArray();
    //        }
    //        private async Task<Project?> GetProject(Guid ProjectId, IRepository repository)
    //        {
    //            Func<IQueryable<Project>, IIncludableQueryable<Project, object>> includes = x => x

    //                .Include(d => d.BudgetItems)
    //                ;

    //            Expression<Func<Project, bool>> criteria = d => d.Id == ProjectId;

    //            return await repository.GetAsync(Criteria: criteria, Includes: includes);
    //        }

    //        private int GetNextOrder(Project project)
    //        {
    //            var alterations = project.BudgetItems.OfType<Alteration>();
    //            return alterations.Any() ? alterations.Max(a => a.Order) + 1 : 1;
    //        }
    //    }

    //    static Alteration Map(this AlterationResponse request, Alteration alteration)
    //    {
    //        alteration.Name = request.Name;
    //        alteration.UnitaryCost = request.UnitaryCost;
    //        alteration.CostCenter = request.CostCenter.Name;
    //        alteration.Quantity = request.Quantity;
    //        alteration.BudgetUSD = request.BudgetUSD;
    //        alteration.IsAlteration = true;
    //        return alteration;
    //    }
    //}
}

