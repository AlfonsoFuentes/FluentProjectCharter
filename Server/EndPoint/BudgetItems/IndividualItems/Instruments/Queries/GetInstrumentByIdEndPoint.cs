﻿using Server.EndPoint.Brands.Queries;
using Server.EndPoint.BudgetItems.IndividualItems.Instruments.Queries;
using Server.EndPoint.BudgetItems.IndividualItems.Nozzles.Queries;
using Shared.Enums.Instruments;
using Shared.Enums.Materials;
using Shared.Enums.ValvesEnum;
using Shared.Models.BudgetItems.IndividualItems.Instruments.Records;
using Shared.Models.BudgetItems.IndividualItems.Instruments.Responses;

namespace Server.EndPoint.BudgetItems.IndividualItems.Instruments.Queries
{
    public static class GetInstrumentByIdEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Instruments.EndPoint.GetById, async (GetInstrumentByIdRequest request, IQueryRepository Repository) =>
                {
                    Func<IQueryable<Instrument>, IIncludableQueryable<Instrument, object>> Includes = x => x
                    .Include(x => x.Nozzles)
                    .Include(x => x.InstrumentTemplate!).ThenInclude(x => x.BrandTemplate!)
                    .Include(x => x.InstrumentTemplate!).ThenInclude(x => x.NozzleTemplates);
                    Expression<Func<Instrument, bool>> Criteria = x => x.Id == request.Id;

                    string CacheKey = StaticClass.Instruments.Cache.GetById(request.Id);
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
        public static InstrumentResponse Map(this Instrument row)
        {
            return new()
            {
                Id = row.Id,
                Name = row.Name,

                ProjectId = row.ProjectId,
                Nomenclatore = row.Nomenclatore,
                BudgetUSD = row.BudgetUSD,

                TagNumber = row.TagNumber,
                BrandResponse = row.InstrumentTemplate == null || row.InstrumentTemplate!.BrandTemplate == null ? new() : row.InstrumentTemplate!.BrandTemplate!.Map(),
                Model = row.InstrumentTemplate == null ? string.Empty : row.InstrumentTemplate.Model,
                Reference = row.InstrumentTemplate == null ? string.Empty : row.InstrumentTemplate.Reference,
                Material = row.InstrumentTemplate == null ? MaterialEnum.None : MaterialEnum.GetType(row.InstrumentTemplate.Material),

                Type = row.InstrumentTemplate == null ? VariableInstrumentEnum.None : VariableInstrumentEnum.GetTypeByName(row.InstrumentTemplate.Type),
                SubType = row.InstrumentTemplate == null ? ModifierVariableInstrumentEnum.None : ModifierVariableInstrumentEnum.GetTypeByName(row.InstrumentTemplate.SubType),
                ShowDetails = row.InstrumentTemplate == null ? false : true,
                Nozzles = row.Nozzles == null || row.Nozzles.Count == 0 ? new() : row.Nozzles.Select(x => x.Map()).ToList(),
                SignalType = row.InstrumentTemplate == null ? SignalTypeEnum.None : SignalTypeEnum.GetType(row.InstrumentTemplate.SignalType),
                TagLetter = row.TagLetter,
                IsExisting = row.IsExisting,
                ProvisionalTag = row.ProvisionalTag,
                ShowProvisionalTag = !string.IsNullOrWhiteSpace(row.ProvisionalTag),
                ActualUSD = row.ActualUSD,
                CommitmentUSD = row.CommitmentUSD,
                PotentialUSD = row.PotentialUSD,
            };


        }

    }
}
