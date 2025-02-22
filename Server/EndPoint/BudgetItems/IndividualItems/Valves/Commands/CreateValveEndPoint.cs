using Server.Database.Entities.ProjectManagements;
using Shared.Models.BudgetItems.Valves.Requests;

namespace Server.EndPoint.BudgetItems.IndividualItems.Valves.Commands
{

    public static class CreateValveEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Valves.EndPoint.Create, async (CreateValveRequest data, IRepository repository) =>
                {
                    var project = await GetProject(data.ProjectId, repository);
                    if (project == null) return Result.Fail(data.Fail);

                    int order = GetNextOrder(project);

                    var row = Valve.Create(project.Id, data.DeliverableId);
                    if (data.DeliverableId.HasValue)
                    {
                        var deliverable = await repository.GetByIdAsync<Deliverable>(data.DeliverableId.Value);
                        if (deliverable != null)
                        {
                            deliverable.ShowBudgetItems = true;
                            await repository.UpdateAsync(deliverable);
                        }
                    }
                    row.Order = order;

                    data.Map(row);
                    if (data.ShowDetails)
                    {
                        var equipmentTemplate = await GetValveTemplate(repository, data);

                        if (equipmentTemplate != null)
                        {
                            row.ValveTemplateId = equipmentTemplate.Id;


                        }
                        order = 0;
                        await CreateNozzles(repository, row, data);
                    }
                    await repository.AddAsync(row);

                    var result = await repository.Context.SaveChangesAndRemoveCacheAsync(GetCacheKeys(row));

                    return Result.EndPointResult(result, data.Succesfully, data.Fail);
                });
            }
            private string[] GetCacheKeys(BudgetItem row)
            {
                var deliverable = row.DeliverableId.HasValue ? StaticClass.Deliverables.Cache.Key(row.DeliverableId!.Value, row.ProjectId) : new[] { string.Empty };
                List<string> cacheKeys = [
                 ..StaticClass.BudgetItems.Cache.Key(row.Id, row.ProjectId),
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
                var alterations = project.BudgetItems.OfType<Valve>();
                return alterations.Any() ? alterations.Max(a => a.Order) + 1 : 1;
            }

            async Task<ValveTemplate> GetValveTemplate(IRepository Repository, CreateValveRequest Data)
            {
                Func<IQueryable<ValveTemplate>, IIncludableQueryable<ValveTemplate, object>> Includes = x => x
                .Include(x => x.BrandTemplate)
                .Include(x => x.NozzleTemplates)
                ;
                Expression<Func<ValveTemplate, bool>> Criteria = x =>
                x.Brand == Data.Brand &&
                x.Model == Data.Model &&
                x.Type == Data.Type.Name &&
                x.Material == Data.Material.Name &&
                x.Diameter == Data.Diameter.Name &&
                x.ActuatorType == Data.ActuatorType.Name &&
                x.PositionerType == Data.PositionerType.Name &&
                x.HasFeedBack == Data.HasFeedBack &&
                x.FailType == Data.FailType.Name &&
                x.SignalType == Data.SignalType.Name;
                var equipmentTemplates = await Repository.GetAllAsync(Includes: Includes);

                var equipmentTemplate = equipmentTemplates.FirstOrDefault(Criteria.Compile());

                if (equipmentTemplate == null)
                {
                    equipmentTemplate = Template.AddValveTemplate();

                    equipmentTemplate.BrandTemplateId = Data.BrandResponse!.Id;//TODO: Add Brand
                    equipmentTemplate.Model = Data.Model;
                    equipmentTemplate.Type = Data.Type.Name;



                    equipmentTemplate.Material = Data.Material.Name;
                    equipmentTemplate.Diameter = Data.Diameter.Name;
                    equipmentTemplate.ActuatorType = Data.ActuatorType.Name;
                    equipmentTemplate.PositionerType = Data.PositionerType.Name;
                    equipmentTemplate.HasFeedBack = Data.HasFeedBack;
                    equipmentTemplate.FailType = Data.FailType.Name;
                    equipmentTemplate.SignalType = Data.SignalType.Name;

                    equipmentTemplate.Value = Data.Budget;
                    foreach (var nozzle in Data.Nozzles)
                    {
                        var nozzleTemplate = NozzleTemplate.Create(equipmentTemplate.Id);
                        nozzleTemplate.ConnectionType = nozzle.ConnectionType.Name;
                        nozzleTemplate.NominalDiameter = nozzle.NominalDiameter.Name;
                        nozzleTemplate.NozzleType = nozzle.NozzleType.Name;
                        await Repository.AddAsync(nozzleTemplate);
                    }
                    await Repository.AddAsync(equipmentTemplate);
                }
                return equipmentTemplate;
            }
            async Task CreateNozzles(IRepository Repository, Valve row, CreateValveRequest Data)
            {
                int order = 0;
                foreach (var nozzleTempalte in Data.Nozzles)
                {
                    order++;
                    var nozzle = Nozzle.Create(row.Id);
                    nozzle.NozzleType = nozzleTempalte.NozzleType.Name;
                    nozzle.ConnectionType = nozzleTempalte.ConnectionType.Name;
                    nozzle.NominalDiameter = nozzleTempalte.NominalDiameter.Name;
                    nozzle.Order = order;
                    await Repository.AddAsync(nozzle);
                }
            }
        }


        static Valve Map(this CreateValveRequest request, Valve row)
        {
            row.Name = request.Name;
            row.TagLetter = request.TagLetter;
            row.TagNumber = request.TagNumber;
            row.IsExisting = request.IsExisting;
            row.Budget = request.Budget;
            row.ProvisionalTag = request.ProvisionalTag;
            return row;
        }

    }

}
