using Shared.Models.BudgetItems.Valves.Requests;
using Server.EndPoint.Nozzles.Commands;


namespace Server.EndPoint.Valves.Commands
{
    public static class UpdateValveEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Valves.EndPoint.Update, async (UpdateValveRequest Data, IRepository Repository) =>
                {
                    Func<IQueryable<Valve>, IIncludableQueryable<Valve, object>> Includes = x => x
                  .Include(x => x.Nozzles)
                  ;
                    Expression<Func<Valve, bool>> Criteria = x => x.Id == Data.Id;

                    var row = await Repository.GetAsync(Criteria: Criteria, Includes: Includes);

                    if (row == null) { return Result.Fail(Data.NotFound); }
                    await Repository.UpdateAsync(row);
                    Data.Map(row);
                    if (Data.ShowDetails)
                    {
                        var equipmentTemplate = await GetValveTemplate(Repository, Data);

                        if (row.ValveTemplateId != null && equipmentTemplate != null && equipmentTemplate.Id != row.ValveTemplateId.Value)
                        {
                            row.ValveTemplateId = equipmentTemplate.Id;
                        }
                        await UpdateNozzles(Repository, row, Data);
                    }

                   
                    List<string> cache = [.. StaticClass.Projects.Cache.Key(Data.ProjectId), .. StaticClass.Valves.Cache.Key(row.Id)];

                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(cache.ToArray());

                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);

                });
            }
            async Task<ValveTemplate> GetValveTemplate(IRepository Repository, UpdateValveRequest Data)
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
            async Task UpdateNozzles(IRepository Repository, Valve row, UpdateValveRequest Data)
            {
                if (Data.Nozzles != null)
                {
                    var nozzles = row.Nozzles.ToList();
                    foreach (var nozzle in Data.Nozzles)
                    {
                        var nozzleRow = nozzles.FirstOrDefault(x => x.Id == nozzle.Id);
                        if (nozzleRow == null)
                        {
                            nozzleRow = Nozzle.Create(row.Id);
                            nozzle.Map(nozzleRow);

                            await Repository.AddAsync(nozzleRow);
                        }
                        else
                        {
                            nozzle.Map(nozzleRow);
                            await Repository.UpdateAsync(nozzleRow);
                        }
                    }
                    var nozzlesToRemove = nozzles.Where(x => !Data.Nozzles.Select(x => x.Id).Contains(x.Id)).ToList();
                    await Repository.RemoveRangeAsync(nozzlesToRemove);
                }
                else
                {
                    await Repository.RemoveRangeAsync(row.Nozzles.ToList());
                }
            }
        }


        static Valve Map(this UpdateValveRequest request, Valve row)
        {
            row.Name = request.Name;
            row.TagLetter = request.TagLetter;
            row.TagNumber = request.TagNumber;
        
            row.Budget = request.Budget;

            return row;
        }

    }

}
