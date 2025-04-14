using DocumentFormat.OpenXml.Drawing.Charts;
using Server.ExtensionsMethods.Projects;
using Shared.Models.BudgetItems.IndividualItems.Valves.Responses;

namespace Server.EndPoint.BudgetItems.IndividualItems.Valves.Commands
{
    public static class CreateUpdateValveEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Valves.EndPoint.CreateUpdate, async (ValveResponse data, IRepository repository) =>
                {
                    var project = await ProjectMapper.GetProject(data.ProjectId, repository);
                    if (project == null) return Result.Fail(data.Fail);

                    Valve? row = null!;
                    if (data.Id == Guid.Empty)
                    {
                        int order = ProjectMapper.GetNextOrder<Valve>(project);
                        row = Valve.Create(project.Id, data.GanttTaskId);
                        row.Order = order;
                        await repository.AddAsync(row);
                        await NozzleMapper.CreateNozzles(repository, row.Id, data.Nozzles);
                    }
                    else
                    {
                        row = await repository.GetByIdAsync<Valve>(data.Id);
                        if (row == null) return Result.Fail(data.NotFound);
                        await repository.UpdateAsync(row);
                        await NozzleMapper.UpdateNozzles(repository, row.Nozzles, data.Nozzles, row.Id);

                    }
                    if (data.ShowDetails)
                    {
                        var valveTemplate = await ValveTemplateMapper.GetValveTemplate(repository, data);

                        if (row.ValveTemplateId == null && valveTemplate != null)
                        {
                            row.ValveTemplateId = valveTemplate.Id;


                        }
                        else if (row.ValveTemplateId != null && valveTemplate != null && valveTemplate.Id != row.ValveTemplateId.Value)
                        {
                            row.ValveTemplateId = valveTemplate.Id;
                        }

                    }

                    data.Map(row);


                    var result = await repository.Context.SaveChangesAndRemoveCacheAsync(GetCacheKeys(row));

                    return Result.EndPointResult(result, data.Succesfully, data.Fail);
                });
            }
            private string[] GetCacheKeys(Valve row)
            {
                var deliverable = row.GanttTaskId.HasValue ? StaticClass.GanttTasks.Cache.Key(row.GanttTaskId!.Value, row.ProjectId) : new[] { string.Empty };
                var budgetitems = StaticClass.BudgetItems.Cache.Key(row.Id, row.ProjectId, row.GanttTaskId);
                var templates = row.ValveTemplateId == null ? new[] { string.Empty } : StaticClass.ValveTemplates.Cache.Key(row.ValveTemplateId!.Value);
                var items = StaticClass.Valves.Cache.Key(row.Id, row.ProjectId);
                List<string> cacheKeys = [
                     ..budgetitems,
                     ..items,
                     ..deliverable,
                    ..templates

                ];

                return cacheKeys.Where(key => !string.IsNullOrEmpty(key)).ToArray();
            }

        }


    }
}

