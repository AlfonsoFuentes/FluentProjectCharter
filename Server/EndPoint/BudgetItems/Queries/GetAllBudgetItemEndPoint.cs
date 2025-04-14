using Shared.Models.BudgetItems.Records;
using Shared.Models.BudgetItems.Responses;
using Server.EndPoint.BudgetItems.IndividualItems.Valves.Queries;
using Server.EndPoint.BudgetItems.IndividualItems.Alterations.Queries;
using Server.EndPoint.BudgetItems.IndividualItems.Structurals.Queries;
using Server.EndPoint.BudgetItems.IndividualItems.Equipments.Queries;
using Server.EndPoint.BudgetItems.IndividualItems.Electricals.Queries;
using Server.EndPoint.BudgetItems.IndividualItems.Pipes.Queries;
using Server.EndPoint.BudgetItems.IndividualItems.Instruments.Queries;
using Server.EndPoint.BudgetItems.IndividualItems.EHSs.Queries;
using Server.EndPoint.BudgetItems.IndividualItems.Paintings.Queries;
using Server.EndPoint.BudgetItems.IndividualItems.Taxs.Queries;
using Server.EndPoint.BudgetItems.IndividualItems.Testings.Queries;
using Server.EndPoint.BudgetItems.IndividualItems.EngineeringDesigns.Queries;
using Server.EndPoint.Communications.Queries;
using Server.EndPoint.BudgetItems.IndividualItems.Contingencys.Queries;
using Shared.Enums.CostCenter;

namespace Server.EndPoint.BudgetItems.Queries
{
    public static class GetAllBudgetItemEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.BudgetItems.EndPoint.GetAll, async (BudgetItemGetAll request, IQueryRepository repository) =>
                {
                    var row = await GetBudgetItemAsync(request, repository);

                    if (row == null)
                    {
                        return Result<BudgetItemResponseList>.Fail(
                            StaticClass.ResponseMessages.ReponseNotFound(StaticClass.BudgetItems.ClassLegend));
                    }

                    BudgetItemResponseList response = new()
                    {
                        ProjectId = request.ProjectId,
                        Alterations = row.BudgetItems == null || row.BudgetItems.Count == 0 ? new() : row.BudgetItems.OfType<Alteration>().Select(x => x.Map()).ToList(),
                        Structurals = row.BudgetItems == null || row.BudgetItems.Count == 0 ? new() : row.BudgetItems.OfType<Structural>().Select(x => x.Map()).ToList(),
                        Foundations = row.BudgetItems == null || row.BudgetItems.Count == 0 ? new() : row.BudgetItems.OfType<Foundation>().Select(x => x.Map()).ToList(),
                        Equipments = row.BudgetItems == null || row.BudgetItems.Count == 0 ? new() : row.BudgetItems.OfType<Equipment>().Select(x => x.Map()).ToList(),

                        Valves = row.BudgetItems == null || row.BudgetItems.Count == 0 ? new() : row.BudgetItems.OfType<Valve>().Select(x => x.Map()).ToList(),
                        Electricals = row.BudgetItems == null || row.BudgetItems.Count == 0 ? new() : row.BudgetItems.OfType<Electrical>().Select(x => x.Map()).ToList(),
                        Pipings = row.BudgetItems == null || row.BudgetItems.Count == 0 ? new() : row.BudgetItems.OfType<Pipe>().Select(x => x.Map()).ToList(),
                        Instruments = row.BudgetItems == null || row.BudgetItems.Count == 0 ? new() : row.BudgetItems.OfType<Instrument>().Select(x => x.Map()).ToList(),

                        EHSs = row.BudgetItems == null || row.BudgetItems.Count == 0 ? new() : row.BudgetItems.OfType<EHS>().Select(x => x.Map()).ToList(),
                        Paintings = row.BudgetItems == null || row.BudgetItems.Count == 0 ? new() : row.BudgetItems.OfType<Painting>().Select(x => x.Map()).ToList(),
                        Taxes = row.BudgetItems == null || row.BudgetItems.Count == 0 ? new() : row.BudgetItems.OfType<Tax>().Select(x => x.Map()).ToList(),
                        Testings = row.BudgetItems == null || row.BudgetItems.Count == 0 ? new() : row.BudgetItems.OfType<Testing>().Select(x => x.Map()).ToList(),

                        EngineeringDesigns = row.BudgetItems == null || row.BudgetItems.Count == 0 ? new() : row.BudgetItems.OfType<EngineeringDesign>().Select(x => x.Map()).ToList(),
                        IsProductive = row.IsProductiveAsset,

                        PercentageContingency = row.PercentageContingency,
                        PercentageEngineering = row.PercentageEngineering,
                        PercentageTaxes = row.PercentageTaxProductive,
                        CostCenter = CostCenterEnum.GetTypeByName(row.CostCenter),
                        ProjectNumber = $"CEC0000{row.ProjectNumber}",
                    };



                    return Result<BudgetItemResponseList>.Success(response);
                });
            }

            private static async Task<Project?> GetBudgetItemAsync(BudgetItemGetAll request, IQueryRepository repository)
            {
                Func<IQueryable<Project>, IIncludableQueryable<Project, object>> includes = x => x
                .Include(p => p.BudgetItems)
                .Include(p => p.BudgetItems).ThenInclude(x => (x as Instrument)!.InstrumentTemplate!)
                .Include(p => p.BudgetItems).ThenInclude(x => (x as Pipe)!.FluidCode!)
                .Include(p => p.BudgetItems).ThenInclude(x => (x as Pipe)!.PipeTemplate!)
                .Include(p => p.BudgetItems).ThenInclude(x => (x as Valve)!.ValveTemplate!)
                .Include(p => p.BudgetItems).ThenInclude(x => (x as Equipment)!.EquipmentTemplate!);

                Expression<Func<Project, bool>> criteria = x => x.Id == request.ProjectId;
                string cacheKey = StaticClass.BudgetItems.Cache.GetAll(request.ProjectId);

                return await repository.GetAsync(Cache: cacheKey, Includes: includes, Criteria: criteria);
            }


        }
    }
}