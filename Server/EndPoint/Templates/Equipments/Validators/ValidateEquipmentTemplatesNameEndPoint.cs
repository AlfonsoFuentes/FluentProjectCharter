﻿using Server.ExtensionsMethods.EquipmentTemplateMapper;
using Server.ExtensionsMethods.InstrumentTemplateMapper;
using Shared.Models.BudgetItems.IndividualItems.Nozzles.Responses;
using Shared.Models.Templates.Equipments.Validators;
using Shared.Models.Templates.NozzleTemplates;
using static Shared.StaticClasses.StaticClass;

namespace Server.EndPoint.Templates.Equipments.Validators
{
    public static class ValidateEquipmentTemplatesNameEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.EquipmentTemplates.EndPoint.Validate, async (ValidateEquipmentTemplateRequest Data, IQueryRepository Repository) =>
                {
                    Func<IQueryable<EquipmentTemplate>, IIncludableQueryable<EquipmentTemplate, object>> Includes = x => x

                    .Include(x => x.BrandTemplate!)
                    .Include(x => x.NozzleTemplates)
                     ;

                    Expression<Func<EquipmentTemplate, bool>> CriteriaExist = x =>
                    x.InternalMaterial == Data.InternalMaterial &&
                    x.ExternalMaterial == Data.ExternalMaterial &&
                    x.BrandName.Equals(Data.Brand) &&
                    x.Model.Equals(Data.Model) &&
                    x.Type.Equals(Data.Type) &&
                    x.SubType.Equals(Data.SubType) &&
                    x.TagLetter.Equals(Data.TagLetter) &&
                    x.Reference.Equals(Data.Reference);

                    string CacheKey = StaticClass.EquipmentTemplates.Cache.GetAll;
                    var equipmentTemplates = await Repository.GetAllAsync(Cache: CacheKey, Includes: Includes, Criteria: CriteriaExist);
                    if (equipmentTemplates == null || !equipmentTemplates.Any())
                    {
                        return false;
                    }
                    // Validar las boquillas para cada template coincidente
                    equipmentTemplates = Data.Id.HasValue ? equipmentTemplates.Where(x => x.Id != Data.Id.Value).ToList() : equipmentTemplates;
                    foreach (var equipmentTemplate in equipmentTemplates)
                    {

                        if (equipmentTemplate.NozzleTemplates.ValidateNozzles(Data.NozzleTemplates))
                        {
                            return true; // Si todas las boquillas coinciden, retornar true
                        }
                    }
                    return false;


                });
            }

        }


    }
}






