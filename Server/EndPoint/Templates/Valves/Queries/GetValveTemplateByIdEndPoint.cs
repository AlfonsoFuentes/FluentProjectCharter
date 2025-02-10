

using Server.EndPoint.Brands.Queries;
using Shared.Enums.ConnectionTypes;
using Shared.Enums.DiameterEnum;
using Shared.Enums.Materials;
using Shared.Enums.NozzleTypes;
using Shared.Models.Templates.Valves.Records;
using Shared.Models.Templates.Valves.Responses;
using Shared.Models.Templates.NozzleTemplates;
using Shared.Enums.ValvesEnum;
using Server.EndPoint.Templates.Valves.Queries;

namespace Server.EndPoint.Templates.Valves.Queries
{
    public static class GetValveTemplateByIdEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.ValveTemplates.EndPoint.GetById,
                    async (GetValveTemplateByIdRequest request, IQueryRepository Repository) =>
                {
                    Func<IQueryable<ValveTemplate>, IIncludableQueryable<ValveTemplate, object>> Includes = x =>
                    x.Include(x => x.NozzleTemplates)
                    .Include(x => x.BrandTemplate!);
                    ;

                    Expression<Func<ValveTemplate, bool>> Criteria = x => x.Id == request.Id;

                    string CacheKey = StaticClass.ValveTemplates.Cache.GetById(request.Id);
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


        public static ValveTemplateResponse Map(this ValveTemplate row)
        {
            return new()
            {
                Id = row.Id,
                BrandResponse = row.BrandTemplate == null ? new() : row.BrandTemplate.Map(),
                Material = MaterialEnum.GetType(row.Material),
                Model = row.Model,
                ActuatorType = ActuatorTypeEnum.GetType(row.ActuatorType),
                Type = ValveTypesEnum.GetTypeByName(row.Type),
                Diameter = NominalDiameterEnum.GetType(row.Diameter),
                FailType = FailTypeEnum.GetType(row.FailType),
                HasFeedBack = row.HasFeedBack,
                SignalType = SignalTypeEnum.GetType(row.SignalType),
                TagLetter = row.TagLetter,
                PositionerType = PositionerTypeEnum.GetType(row.PositionerType),


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
