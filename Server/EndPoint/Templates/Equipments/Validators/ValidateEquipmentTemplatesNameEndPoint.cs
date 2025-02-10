﻿using Shared.Models.Templates.Equipments.Validators;

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
                     ;


                    Expression<Func<EquipmentTemplate, bool>> CriteriaId = null!;
                    Func<EquipmentTemplate, bool> CriteriaExist = x => Data.Id == null ?
                    x.InternalMaterial.Equals(Data.InternalMaterial) &&
                    x.ExternalMaterial.Equals(Data.ExternalMaterial) &&
                    x.Brand.Equals(Data.Brand) &&
                    x.Model.Equals(Data.Model) &&
                    x.Type.Equals(Data.Type) &&
                    x.SubType.Equals(Data.SubType) &&
                    x.TagLetter.Equals(Data.TagLetter) &&
                    x.Reference.Equals(Data.Reference)
                    : x.Id != Data.Id.Value &&
                    x.InternalMaterial.Equals(Data.InternalMaterial) &&
                    x.ExternalMaterial.Equals(Data.ExternalMaterial) &&
                    x.Brand.Equals(Data.Brand) &&
                    x.Model.Equals(Data.Model) &&
                    x.Type.Equals(Data.Type) &&
                    x.SubType.Equals(Data.SubType) &&
                    x.TagLetter.Equals(Data.TagLetter) &&
                    x.Reference.Equals(Data.Reference);

                    string CacheKey = StaticClass.EquipmentTemplates.Cache.GetAll;

                    return await Repository.AnyAsync(Cache: CacheKey, CriteriaExist: CriteriaExist, CriteriaId: CriteriaId, Includes: Includes);
                });


            }
        }



    }

}
