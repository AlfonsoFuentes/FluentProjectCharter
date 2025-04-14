using Server.ExtensionsMethods.Projects;
using Shared.Models.BudgetItems.IndividualItems.Equipments.Responses;

namespace Server.EndPoint.BudgetItems.IndividualItems.Equipments.Commands
{
    public static class CreateUpdateEquipmentEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Equipments.EndPoint.CreateUpdate, async (EquipmentResponse data, IRepository repository) =>
                {
                    var project = await ProjectMapper.GetProject(data.ProjectId, repository);
                    if (project == null) return Result.Fail(data.Fail);

                    Equipment? row = null!;
                    if (data.Id == Guid.Empty)
                    {
                        int order = ProjectMapper.GetNextOrder<Equipment>(project);
                        row = Equipment.Create(project.Id, data.GanttTaskId);
                        row.Order = order;
                        await repository.AddAsync(row);
                        await NozzleMapper.CreateNozzles(repository, row.Id, data.Nozzles);
                    }
                    else
                    {
                        row = await repository.GetByIdAsync<Equipment>(data.Id);
                        if (row == null) return Result.Fail(data.NotFound);
                        await repository.UpdateAsync(row);
                        await NozzleMapper.UpdateNozzles(repository, row.Nozzles, data.Nozzles, row.Id);
                    }
                    if (data.ShowDetails)
                    {
                        var equipmentTemplate = await EquipmentTemplateMapper.GetEquipmentTemplate(repository, data);

                        if (row.EquipmentTemplateId == null && equipmentTemplate != null)
                        {
                            row.EquipmentTemplateId = equipmentTemplate.Id;

                        }
                        else if (row.EquipmentTemplateId != null && equipmentTemplate != null && equipmentTemplate.Id != row.EquipmentTemplateId.Value)
                        {
                            row.EquipmentTemplateId = equipmentTemplate.Id;
                        }

                    }

                    data.Map(row);


                    var result = await repository.Context.SaveChangesAndRemoveCacheAsync(GetCacheKeys(row));

                    return Result.EndPointResult(result, data.Succesfully, data.Fail);
                });
            }
            private string[] GetCacheKeys(Equipment row)
            {
                var deliverable = row.GanttTaskId.HasValue ? StaticClass.GanttTasks.Cache.Key(row.GanttTaskId!.Value, row.ProjectId) : new[] { string.Empty };
                var budgetitems = StaticClass.BudgetItems.Cache.Key(row.Id, row.ProjectId, row.GanttTaskId);
                var items = StaticClass.Equipments.Cache.Key(row.Id, row.ProjectId);
                var templates = row.EquipmentTemplateId == null ? new[] { string.Empty } : StaticClass.ValveTemplates.Cache.Key(row.EquipmentTemplateId!.Value);
                List<string> cacheKeys = [
                     ..budgetitems,
                     ..items,
                     ..deliverable,
                     ..templates,

                ];

                return cacheKeys.Where(key => !string.IsNullOrEmpty(key)).ToArray();
            }

        }


    }
}

