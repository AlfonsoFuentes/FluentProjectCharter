using DocumentFormat.OpenXml.Drawing.Charts;
using Server.ExtensionsMethods.InstrumentTemplateMapper;
using Server.ExtensionsMethods.Projects;
using Shared.Models.BudgetItems.IndividualItems.Instruments.Responses;

namespace Server.EndPoint.BudgetItems.IndividualItems.Instruments.Commands
{
    public static class CreateUpdateInstrumentEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Instruments.EndPoint.CreateUpdate, async (InstrumentResponse data, IRepository repository) =>
                {
                    var project = await ProjectMapper.GetProject(data.ProjectId, repository);
                    if (project == null) return Result.Fail(data.Fail);

                    Instrument? row = null!;
                    if (data.Id == Guid.Empty)
                    {
                        int order = ProjectMapper.GetNextOrder<Instrument>(project);
                        row = Instrument.Create(project.Id, data.GanttTaskId);
                        row.Order = order;
                        await repository.AddAsync(row);
                        await NozzleMapper.CreateNozzles(repository, row.Id, data.Nozzles);
                    }
                    else
                    {
                        row = await repository.GetByIdAsync<Instrument>(data.Id);
                        if (row == null) return Result.Fail(data.NotFound);
                        await repository.UpdateAsync(row);
                        await NozzleMapper.UpdateNozzles(repository, row.Nozzles, data.Nozzles, row.Id);
                    }
                    if (data.ShowDetails)
                    {
                        var instrumentTemplate = await InstrumentTemplateMapper.GetInstrumentTemplate(repository, data);

                        if (row.InstrumentTemplateId == null && instrumentTemplate != null)
                        {
                            row.InstrumentTemplateId = instrumentTemplate.Id;


                        }
                        else if (row.InstrumentTemplateId != null && instrumentTemplate != null && instrumentTemplate.Id != row.InstrumentTemplateId.Value)
                        {
                            row.InstrumentTemplateId = instrumentTemplate.Id;
                        }

                    }

                    data.Map(row);


                    var result = await repository.Context.SaveChangesAndRemoveCacheAsync(GetCacheKeys(row));

                    return Result.EndPointResult(result, data.Succesfully, data.Fail);
                });
            }
            private string[] GetCacheKeys(Instrument row)
            {
                var deliverable = row.GanttTaskId.HasValue ? StaticClass.GanttTasks.Cache.Key(row.GanttTaskId!.Value, row.ProjectId) : new[] { string.Empty };
                var budgetitems = StaticClass.BudgetItems.Cache.Key(row.Id, row.ProjectId, row.GanttTaskId);
                var items = StaticClass.Instruments.Cache.Key(row.Id, row.ProjectId);
                var templates = row.InstrumentTemplateId == null ? new[] { string.Empty } : StaticClass.ValveTemplates.Cache.Key(row.InstrumentTemplateId!.Value);
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

