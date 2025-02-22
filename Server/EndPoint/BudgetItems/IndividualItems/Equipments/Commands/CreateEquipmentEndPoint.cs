using Server.Database.Entities.ProjectManagements;
using Shared.Models.BudgetItems.IndividualItems.Equipments.Requests;

namespace Server.EndPoint.BudgetItems.IndividualItems.Equipments.Commands
{

    public static class CreateEquipmentEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Equipments.EndPoint.Create, async (CreateEquipmentRequest data, IRepository repository) =>
                {
                    var project = await GetProject(data.ProjectId, repository);
                    if (project == null) return Result.Fail(data.Fail);

                    int order = GetNextOrder(project);

                    var row = Equipment.Create(project.Id, data.DeliverableId);
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
                    await repository.AddAsync(row);
                    if (data.ShowDetails)
                    {
                        var equipmentTemplate = await GetEquipmentTemplate(repository, data);

                        if (equipmentTemplate != null)
                        {
                            row.EquipmentTemplateId = equipmentTemplate.Id;


                        }
                        order = 0;
                        await CreateNozzles(repository, row, data);
                    }

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
                var rows = project.BudgetItems.OfType<Equipment>();
                return rows.Any() ? rows.Max(a => a.Order) + 1 : 1;
            }
            async Task<EquipmentTemplate> GetEquipmentTemplate(IRepository Repository, CreateEquipmentRequest Data)
            {
                Func<IQueryable<EquipmentTemplate>, IIncludableQueryable<EquipmentTemplate, object>> Includes = x => x
                .Include(x => x.BrandTemplate)
                .Include(x => x.NozzleTemplates)
                ;
                Expression<Func<EquipmentTemplate, bool>> Criteria = x =>
                x.Brand == Data.Brand &&
                x.Model == Data.Model &&
                x.Type == Data.Type &&
                x.TagLetter == Data.TagLetter &&
                x.SubType == Data.SubType &&
                x.Reference == Data.Reference &&
                x.ExternalMaterial == Data.ExternalMaterial.Name &&
                x.InternalMaterial == Data.InternalMaterial.Name

                ;
                var equipmentTemplates = await Repository.GetAllAsync(Includes: Includes);

                var equipmentTemplate = equipmentTemplates.FirstOrDefault(Criteria.Compile());

                if (equipmentTemplate == null)
                {
                    equipmentTemplate = Template.AddEquipmentTemplate();

                    equipmentTemplate.BrandTemplateId = Data.BrandResponse.Id;//TODO: Add Brand
                    equipmentTemplate.Model = Data.Model;
                    equipmentTemplate.Type = Data.Type;
                    equipmentTemplate.TagLetter = Data.TagLetter;
                    equipmentTemplate.SubType = Data.SubType;
                    equipmentTemplate.Reference = Data.Reference;
                    equipmentTemplate.ExternalMaterial = Data.ExternalMaterial.Name;
                    equipmentTemplate.InternalMaterial = Data.InternalMaterial.Name;
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
            async Task CreateNozzles(IRepository Repository, Equipment row, CreateEquipmentRequest Data)
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



        static Equipment Map(this CreateEquipmentRequest request, Equipment row)
        {
            row.Name = request.Name;
            row.TagLetter = request.ShowDetails ? request.TagLetter : string.Empty;
            row.TagNumber = request.TagNumber;
            row.IsExisting = request.IsExisting;
            row.Budget = request.Budget;
            row.ProvisionalTag = request.ProvisionalTag;
            return row;
        }

    }

}
