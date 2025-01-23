﻿using Server.EndPoint.Brands.Queries;
using Shared.Models.BudgetItems.Pipes.Records;
using Shared.Models.BudgetItems.Pipes.Responses;
using Server.EndPoint.PipeTemplates.Queries;
using Server.EndPoint.Nozzles.Queries;
using Shared.Enums.Materials;
using Shared.Enums.DiameterEnum;
using Server.EndPoint.EngineeringFluidCodes.Queries;
namespace Server.EndPoint.Pipes.Queries
{
    public static class GetPipeByIdEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Pipes.EndPoint.GetById, async (GetPipeByIdRequest request, IQueryRepository Repository) =>
                {
                    Func<IQueryable<Pipe>, IIncludableQueryable<Pipe, object>> Includes = x => x
                    .Include(x => x.Nozzles)
                    .Include(x => x.FluidCode)
                    .Include(x => x.PipeTemplate!).ThenInclude(x => x.BrandTemplate!)
                    .Include(x => x.PipeTemplate!).ThenInclude(x => x.NozzleTemplates);
                    Expression<Func<Pipe, bool>> Criteria = x => x.Id == request.Id;

                    string CacheKey = StaticClass.Pipes.Cache.GetById(request.Id);
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
        public static PipeResponse Map(this Pipe row)
        {
            PipeResponse result = new()
            {
                Id = row.Id,
                Name = row.Name,
                DeliverableId = row.DeliverableId == null ? Guid.Empty : row.DeliverableId.Value,
                ProjectId = row.ProjectId,
                Nomenclatore = row.Nomenclatore,

                TagNumber = row.TagNumber,
                BrandResponse = row.PipeTemplate == null || row.PipeTemplate!.BrandTemplate == null ? new() : row.PipeTemplate!.BrandTemplate!.Map(),

                ShowDetails = row.PipeTemplate == null ? false : true,
                Nozzles = row.Nozzles == null || row.Nozzles.Count == 0 ? new() : row.Nozzles.Select(x => x.Map()).ToList(),
                Diameter = NominalDiameterEnum.GetType(row.Diameter),
                EquivalentLenghPrice = row.PipeTemplate == null ? row.EquivalentLenghPrice : row.PipeTemplate.EquivalentLenghPrice,
                Insulation = row.Insulation,
                LaborDayPrice = row.PipeTemplate == null ? row.LaborDayPrice : row.PipeTemplate.LaborDayPrice,
                Material = MaterialEnum.GetType(row.Material),
                LaborQuantity = row.LaborQuantity,
                MaterialQuantity = row.MaterialQuantity,
                FluidCode = row.FluidCode == null ? null : row.FluidCode.Map(),
                PipeClass = row.PipeTemplate == null ? PipeClassEnum.None : PipeClassEnum.GetType(row.PipeTemplate.Class),
                FluidCodefromDB = row.FluidCodeCode,
                Budget=row.Budget,
                IsExisting=row.IsExisting,


            };
            return result;

        }

    }
}
