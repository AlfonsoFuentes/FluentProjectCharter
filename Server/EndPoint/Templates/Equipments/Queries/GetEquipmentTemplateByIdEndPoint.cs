using Server.EndPoint.Brands.Queries;
using Server.EndPoint.Templates.Equipments.Queries;
using Shared.Enums.ConnectionTypes;
using Shared.Enums.DiameterEnum;
using Shared.Enums.Materials;
using Shared.Enums.NozzleTypes;
using Shared.Models.Templates.Equipments.Records;
using Shared.Models.Templates.Equipments.Responses;
using Shared.Models.Templates.NozzleTemplates;

namespace Server.EndPoint.Templates.Equipments.Queries
{
    public static class GetEquipmentTemplateByIdEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.EquipmentTemplates.EndPoint.GetById,
                    async (GetEquipmentTemplateByIdRequest request, IQueryRepository Repository) =>
                {
                    Func<IQueryable<EquipmentTemplate>, IIncludableQueryable<EquipmentTemplate, object>> Includes = x =>
                    x.Include(x => x.NozzleTemplates)
                    .Include(x => x.BrandTemplate!);
                    ;

                    Expression<Func<EquipmentTemplate, bool>> Criteria = x => x.Id == request.Id;

                    string CacheKey = StaticClass.EquipmentTemplates.Cache.GetById(request.Id);
                    var row = await Repository.GetAsync(Cache: CacheKey, Criteria: Criteria, Includes: Includes);

                    if (row == null)
                    {
                        return Result.Fail(request.NotFound);
                    }

                    var response = row.Map();
                    return Result.Success(response);

                });
            }
        }


        public static EquipmentTemplateResponse Map(this EquipmentTemplate row)
        {
            return new()
            {
                Id = row.Id,
                BrandResponse = row.BrandTemplate == null ? new() : row.BrandTemplate.Map(),
                ExternalMaterial = MaterialEnum.GetType(row.ExternalMaterial),
                InternalMaterial = MaterialEnum.GetType(row.InternalMaterial),
                Model = row.Model,
                Reference = row.Reference,
                SubType = row.SubType,
                TagLetter = row.TagLetter,
                Type = row.Type,
                Value = row.Value,
                Nozzles = row.NozzleTemplates.Count == 0 ? new() : row.NozzleTemplates.Select(x => x.Map()).ToList(),

            };
        }
        static NozzleTemplateResponse Map(this NozzleTemplate row)
        {

            return new()
            {
                Id = row.Id,
                ConnectionType = ConnectionTypeEnum.GetType(row.ConnectionType),
                NominalDiameter = NominalDiameterEnum.GetType(row.NominalDiameter),
                NozzleType = NozzleTypeEnum.GetType(row.NozzleType),

            };
        }
    }
}
