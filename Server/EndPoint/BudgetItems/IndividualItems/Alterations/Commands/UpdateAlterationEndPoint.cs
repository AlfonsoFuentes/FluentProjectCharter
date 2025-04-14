using Server.Database.Entities.BudgetItems.Commons;
using Server.Database.Entities.ProjectManagements;
using Server.Repositories;
using Shared.Models.BudgetItems.IndividualItems.Alterations.Requests;
using Shared.Models.BudgetItems.IndividualItems.Alterations.Responses;


namespace Server.EndPoint.BudgetItems.IndividualItems.Alterations.Commands
{
    //public static class UpdateAlterationEndPoint
    //{
    //    public class EndPoint : IEndPoint
    //    {
    //        public void MapEndPoint(IEndpointRouteBuilder app)
    //        {
    //            app.MapPost(StaticClass.Alterations.EndPoint.Update, async (AlterationResponse data, IRepository repository) =>
    //            {
    //                var row = await repository.GetByIdAsync<Alteration>(data.Id);
    //                if (row == null)
    //                {
    //                    return Result.Fail(data.NotFound);
    //                }
                   

    //                data.Map(row);
    //                await repository.UpdateAsync(row);

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

    //    }

    //    static Alteration Map(this AlterationResponse request, Alteration row)
    //    {
    //        row.Name = request.Name;
    //        row.UnitaryCost = request.UnitaryCost;
    //        row.CostCenter = request.CostCenter.Name;
    //        row.Quantity = request.Quantity;
    //        row.BudgetUSD = request.BudgetUSD;

    //        return row;
    //    }
    //}

}
