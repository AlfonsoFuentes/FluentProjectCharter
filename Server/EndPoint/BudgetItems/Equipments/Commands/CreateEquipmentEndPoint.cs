using Shared.Models.BudgetItems.Equipments.Requests;

namespace Server.EndPoint.Equipments.Commands
{

    public static class CreateEquipmentEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Equipments.EndPoint.Create, async (CreateEquipmentRequest Data, IRepository Repository) =>
                {
                    Func<IQueryable<Project>, IIncludableQueryable<Project, object>> Includes = x => x.Include(x => x.BudgetItems);
                    Expression<Func<Project, bool>> Criteria = x => x.Id == Data.ProjectId;

                    var project = await Repository.GetAsync(Criteria: Criteria, Includes: Includes);
                    int order = 1;
                    if (project != null)
                    {
                        order = project.BudgetItems.OfType<Equipment>() == null ||
                        project.BudgetItems.OfType<Equipment>().Count() == 0 ?
                        order : project.BudgetItems.OfType<Equipment>().MaxBy(x => x.Order)!.Order + 1;
                    }

                    var row = Equipment.Create(Data.ProjectId, Data.DeliverableId);

                    await Repository.AddAsync(row);

                    row.Order = order;

                    Data.Map(row);
                    if (Data.ShowDetails)
                    {
                        var equipmentTemplate = await GetEquipmentTemplate(Repository, Data);

                        if (equipmentTemplate != null)
                        {
                            row.EquipmentTemplateId = equipmentTemplate.Id;

                           
                        }
                        order = 0;
                        await CreateNozzles(Repository, row, Data);
                    }
                    

                    List<string> cache = [.. StaticClass.Projects.Cache.Key(Data.ProjectId),
                        .. StaticClass.Equipments.Cache.Key(row.Id),StaticClass.EquipmentTemplates.Cache.GetAll];

                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(cache.ToArray());
                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);


                });


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
