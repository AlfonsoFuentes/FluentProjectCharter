using DocumentFormat.OpenXml.Spreadsheet;
using Server.Database.Entities.ProjectManagements;
using Shared.Models.BudgetItems.IndividualItems.EngineeringDesigns.Requests;


namespace Server.EndPoint.BudgetItems.IndividualItems.EngineeringDesigns.Commands
{
    public static class UpdateEngineeringDesignEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.EngineeringDesigns.EndPoint.Update, async (UpdateEngineeringDesignRequest data, IRepository repository) =>
                {
                    var row = await repository.GetByIdAsync<EngineeringDesign>(data.Id);
                    if (row == null)
                    {
                        return Result.Fail(data.NotFound);
                    }
                    if (data.GanttTaskId.HasValue)
                    {
                        var deliverable = await repository.GetByIdAsync<GanttTask>(data.GanttTaskId.Value);
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
                var deliverable = row.GanttTaskId.HasValue ? StaticClass.GanttTasks.Cache.Key(row.GanttTaskId!.Value, row.ProjectId) : new[] { string.Empty };
                List<string> cacheKeys = [
                 ..StaticClass.BudgetItems.Cache.Key(row.Id, row.ProjectId, row.GanttTaskId),
                 ..deliverable
                ];
                return cacheKeys.Where(key => !string.IsNullOrEmpty(key)).ToArray();
            }

        }


        static EngineeringDesign Map(this UpdateEngineeringDesignRequest request, EngineeringDesign row)
        {
            row.Name = request.Name;

            row.Budget = request.Budget;

            return row;
        }

    }

}
