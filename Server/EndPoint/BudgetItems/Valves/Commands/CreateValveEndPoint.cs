using Server.Database.Entities.BudgetItems.Commons;
using Server.Database.Entities.BudgetItems.ProcessFlowDiagrams.Enums;
using Shared.Models.BudgetItems.Equipments.Requests;
using Shared.Models.BudgetItems.Valves.Requests;

namespace Server.EndPoint.Valves.Commands
{

    public static class CreateValveEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Valves.EndPoint.Create, async (CreateValveRequest Data, IRepository Repository) =>
                {
                    Func<IQueryable<Project>, IIncludableQueryable<Project, object>> Includes = x => x
                    .Include(x => x.BudgetItems);
                    Expression<Func<Project, bool>> Criteria = x => x.Id == Data.ProjectId;

                    var project = await Repository.GetAsync(Criteria: Criteria, Includes: Includes);
                    int order = 1;
                    if (project != null)
                    {
                        order = project.BudgetItems.OfType<Equipment>() == null ||
                        project.BudgetItems.OfType<Equipment>().Count() == 0 ?
                        order : project.BudgetItems.OfType<Equipment>().MaxBy(x => x.Order)!.Order + 1;
                    }

                    var row = Valve.Create(Data.ProjectId, Data.DeliverableId);

                    await Repository.AddAsync(row);


                    row.Order = order;

                    Data.Map(row);
                
                    if (Data.ShowDetails)
                    {
                        var equipmentTemplate = await GetValveTemplate(Repository, Data);

                        if (equipmentTemplate != null)
                        {
                            row.ValveTemplateId = equipmentTemplate.Id;


                        }
                        order = 0;
                        await CreateNozzles(Repository, row, Data);
                    }

                   

                    List<string> cache = [.. StaticClass.Projects.Cache.Key(Data.ProjectId), .. StaticClass.Valves.Cache.Key(row.Id)];

                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(cache.ToArray());
                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);


                });


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
                x.SignalType == Data.SignalType.Name ;
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
