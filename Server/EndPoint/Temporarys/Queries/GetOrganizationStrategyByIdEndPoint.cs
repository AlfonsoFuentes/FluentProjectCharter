

using Shared.Enums.Materials;
using Shared.Models.Temporarys.Records;
using Shared.Models.Temporarys.Responses;

namespace Server.EndPoint.Temporarys.Queries
{
    public static class GetTemporaryByIdEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Temporarys.EndPoint.GetById,
                    async (GetTemporaryByIdRequest request, IQueryRepository Repository) =>
                {
                    //Func<IQueryable<Temporary>, IIncludableQueryable<Temporary, object>> Includes = x =>
                    //x.Include(x => x.Cases).ThenInclude(x => x.Project);
                    //;

                    Expression<Func<Temporary, bool>> Criteria = x => x.Id == request.Id;

                    string CacheKey = StaticClass.Temporarys.Cache.GetById(request.Id);
                    var row = await Repository.GetAsync(Cache: CacheKey, Criteria: Criteria);

                    if (row == null)
                    {
                        return Result.Fail(request.NotFound);
                    }

                    var response = row.Map();
                    return Result.Success(response);

                });
            }
        }


        public static TemporaryResponse Map(this Temporary row)
        {
            return new()
            {
                Id = row.Id,
                BrandTemplateId = row.BrandTemplateId,
                EquipmentId = row.EquipmentId,
                EquipmentTemplateId = row.EquipmentTemplateId,
                ExternalMaterial = MaterialEnum.GetType(row.ExternalMaterial),
                InternalMaterial = MaterialEnum.GetType(row.InternalMaterial),
                Material = MaterialEnum.GetType(row.Material),
                Model = row.Model,
                Reference = row.Reference,
                Equipment = row.Equipment,
                EquipmentTemplate = row.EquipmentTemplate,
                InstrumentId = row.InstrumentId,
                InstrumentTemplateId = row.InstrumentTemplateId,
                PipingId = row.PipingId,
                PipingTemplateId = row.PipingTemplateId,
                SubType = row.SubType,
                TagLetter = row.TagLetter,
                Type = row.Type,
                Value = row.Value,
                ValveId = row.ValveId,
                ValveTemplateId = row.ValveTemplateId,
                 
                
            };
        }



    }
}
