using Server.EndPoint.Brands.Queries;
using Server.EndPoint.BudgetItems.IndividualItems.Nozzles.Queries;
using Shared.Enums.DiameterEnum;
using Shared.Enums.Materials;
using Shared.Enums.ValvesEnum;
using Shared.Models.BudgetItems.IndividualItems.Valves.Records;
using Shared.Models.BudgetItems.IndividualItems.Valves.Responses;

namespace Server.EndPoint.BudgetItems.IndividualItems.Valves.Queries
{
    public static class GetValveByIdEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Valves.EndPoint.GetById, async (GetValveByIdRequest request, IQueryRepository Repository) =>
                {
                    Func<IQueryable<Valve>, IIncludableQueryable<Valve, object>> Includes = x => x

                    .Include(x => x.Nozzles)
                    .Include(x => x.ValveTemplate!).ThenInclude(x => x.BrandTemplate!)
                    .Include(x => x.ValveTemplate!).ThenInclude(x => x.NozzleTemplates!);

                    Expression<Func<Valve, bool>> Criteria = x => x.Id == request.Id;

                    string CacheKey = StaticClass.Valves.Cache.GetById(request.Id);
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
        public static ValveResponse Map(this Valve row)
        {
            ValveResponse result = new()
            {
                Id = row.Id,
                Name = row.Name,

                ProjectId = row.ProjectId,
                Nomenclatore = row.Nomenclatore,
                BudgetUSD = row.BudgetUSD,

                TagNumber = row.TagNumber,
                BrandResponse = row.ValveTemplate == null || row.ValveTemplate.BrandTemplate == null ? new() : row.ValveTemplate.BrandTemplate!.Map(),
                Model = row.ValveTemplate == null ? string.Empty : row.ValveTemplate.Model,
                Type = row.ValveTemplate == null ? ValveTypesEnum.None : ValveTypesEnum.GetTypeByName(row.ValveTemplate.Type),
                Material = row.ValveTemplate == null ? MaterialEnum.None : MaterialEnum.GetType(row.ValveTemplate.Material),
                ActuatorType = row.ValveTemplate == null ? ActuatorTypeEnum.None : ActuatorTypeEnum.GetType(row.ValveTemplate.ActuatorType),
                PositionerType = row.ValveTemplate == null ? PositionerTypeEnum.None : PositionerTypeEnum.GetType(row.ValveTemplate.PositionerType),
                HasFeedBack = row.ValveTemplate == null ? false : row.ValveTemplate.HasFeedBack,
                Diameter = row.ValveTemplate == null ? NominalDiameterEnum.None : NominalDiameterEnum.GetType(row.ValveTemplate.Diameter),
                FailType = row.ValveTemplate == null ? FailTypeEnum.None : FailTypeEnum.GetType(row.ValveTemplate.FailType),
                SignalType = row.ValveTemplate == null ? SignalTypeEnum.None : SignalTypeEnum.GetType(row.ValveTemplate.SignalType),
                TagLetter = row.TagLetter,
                ShowDetails = row.ValveTemplate != null,
                Nozzles = row.Nozzles == null || row.Nozzles.Count == 0 ? new() : row.Nozzles.Select(x => x.Map()).ToList(),
                IsExisting = row.IsExisting,
                ProvisionalTag = row.ProvisionalTag,
                ShowProvisionalTag = !string.IsNullOrWhiteSpace(row.ProvisionalTag),
                ActualUSD = row.ActualUSD,
                CommitmentUSD = row.CommitmentUSD,
                PotentialUSD = row.PotentialUSD,

            };

            return result;
        }


    }
}
