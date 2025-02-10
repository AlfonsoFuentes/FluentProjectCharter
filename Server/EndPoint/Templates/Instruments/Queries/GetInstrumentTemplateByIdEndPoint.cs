using Server.EndPoint.Brands.Queries;
using Server.EndPoint.Templates.Instruments.Queries;
using Shared.Enums.ConnectionTypes;
using Shared.Enums.DiameterEnum;
using Shared.Enums.Instruments;
using Shared.Enums.Materials;
using Shared.Enums.NozzleTypes;
using Shared.Enums.ValvesEnum;
using Shared.Models.Templates.Instruments.Records;
using Shared.Models.Templates.Instruments.Responses;
using Shared.Models.Templates.NozzleTemplates;

namespace Server.EndPoint.Templates.Instruments.Queries
{
    public static class GetInstrumentTemplateByIdEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.InstrumentTemplates.EndPoint.GetById,
                    async (GetInstrumentTemplateByIdRequest request, IQueryRepository Repository) =>
                {
                    Func<IQueryable<InstrumentTemplate>, IIncludableQueryable<InstrumentTemplate, object>> Includes = x =>
                    x.Include(x => x.NozzleTemplates)
                    .Include(x => x.BrandTemplate!);
                    ;

                    Expression<Func<InstrumentTemplate, bool>> Criteria = x => x.Id == request.Id;

                    string CacheKey = StaticClass.InstrumentTemplates.Cache.GetById(request.Id);
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


        public static InstrumentTemplateResponse Map(this InstrumentTemplate row)
        {
            return new()
            {
                Id = row.Id,
                BrandResponse = row.BrandTemplate == null ? new() : row.BrandTemplate.Map(),
                Material = MaterialEnum.GetType(row.Material),

                Model = row.Model,
                Reference = row.Reference,
                SubType = ModifierVariableInstrumentEnum.GetTypeByName(row.SubType),

                Type = VariableInstrumentEnum.GetTypeByName(row.Type),
                Value = row.Value,
                SignalType = SignalTypeEnum.GetType(row.SignalType),
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
