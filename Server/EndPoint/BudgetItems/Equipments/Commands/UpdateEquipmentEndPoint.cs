﻿using Server.EndPoint.Nozzles.Commands;
using Shared.Models.BudgetItems.Equipments.Requests;


namespace Server.EndPoint.Equipments.Commands
{
    public static class UpdateEquipmentEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Equipments.EndPoint.Update, async (UpdateEquipmentRequest Data, IRepository Repository) =>
                {
                    Func<IQueryable<Equipment>, IIncludableQueryable<Equipment, object>> Includes = x => x
                   .Include(x => x.Nozzles)
                   ;
                    Expression<Func<Equipment, bool>> Criteria = x => x.Id == Data.Id;

                    var row = await Repository.GetAsync(Criteria: Criteria, Includes: Includes);


                    if (row == null) { return Result.Fail(Data.NotFound); }
                    await Repository.UpdateAsync(row);
                    Data.Map(row);
                    if (Data.ShowDetails)
                    {
                        var equipmentTemplate = await GetEquipmentTemplate(Repository, Data);

                        if (row.EquipmentTemplateId != null && equipmentTemplate != null && equipmentTemplate.Id != row.EquipmentTemplateId.Value)
                        {
                            row.EquipmentTemplateId = equipmentTemplate.Id;
                        }
                        await UpdateNozzles(Repository, row, Data);
                    }
                   
                
                    List<string> cache = [.. StaticClass.Projects.Cache.Key(Data.ProjectId), .. StaticClass.Equipments.Cache.Key(row.Id)];

                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(cache.ToArray());

                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);

                });
            }
            async Task<EquipmentTemplate> GetEquipmentTemplate(IRepository Repository, UpdateEquipmentRequest Data)
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

            async Task UpdateNozzles(IRepository Repository, Equipment row, UpdateEquipmentRequest Data)
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



        static Equipment Map(this UpdateEquipmentRequest request, Equipment row)
        {
            row.Name = request.Name;
            row.TagLetter = request.TagLetter;
            row.TagNumber = request.TagNumber;

            row.Budget = request.Budget;

            return row;
        }

    }

}
