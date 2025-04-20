using Shared.Models.BudgetItems.Records;
using Shared.Models.BudgetItems.Responses;
using Server.EndPoint.Communications.Queries;
using Server.EndPoint.BudgetItems.IndividualItems.Contingencys.Queries;
using Server.EndPoint.BudgetItems.IndividualItems.Engineerings.Queries;
using Shared.Enums.CostCenter;
using Server.EndPoint.PurchaseOrders.Queries;

namespace Server.EndPoint.BudgetItems.Queries
{
    public static class GetAllBudgetItemWithPurchaseOrdersEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.BudgetItems.EndPoint.GetAllWithPurchaseorder, async (BudgetItemWithPurchaseOrderGetAll request, IQueryRepository repository) =>
                {
                    var row = await GetBudgetItemAsync(request, repository);

                    if (row == null)
                    {
                        return Result<BudgetItemWithPurchaseOrderResponseList>.Fail(
                            StaticClass.ResponseMessages.ReponseNotFound(StaticClass.BudgetItems.ClassLegend));
                    }

                    BudgetItemWithPurchaseOrderResponseList response = new()
                    {
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
                        Engineerings = row.BudgetItems == null || row.BudgetItems.Count == 0 ? new() : row.BudgetItems.OfType<Engineering>().Select(x => x.Map()).ToList(),
                        Contingencies = row.BudgetItems == null || row.BudgetItems.Count == 0 ? new() : row.BudgetItems.OfType<Contingency>().Select(x => x.Map()).ToList(),
                        EngineeringDesigns = row.BudgetItems == null || row.BudgetItems.Count == 0 ? new() : row.BudgetItems.OfType<EngineeringDesign>().Select(x => x.Map()).ToList(),
                        IsProductiveAsset = row.IsProductiveAsset,

                        PercentageContingency = row.PercentageContingency,
                        PercentageEngineering = row.PercentageEngineering,
                        PercentageTaxes = row.PercentageTaxProductive,
                        CostCenter = CostCenterEnum.GetType(row.CostCenter),
                        ProjectId = row.Id,
                        ProjectNumber = $"CEC0000{row.ProjectNumber}",


                    };
                   


                    return Result<BudgetItemWithPurchaseOrderResponseList>.Success(response);
                });
            }

            private static async Task<Project?> GetBudgetItemAsync(BudgetItemWithPurchaseOrderGetAll request, IQueryRepository repository)
            {
                Func<IQueryable<Project>, IIncludableQueryable<Project, object>> includes = x => x
                .Include(p => p.BudgetItems).ThenInclude(x => x.PurchaseOrderItems).ThenInclude(x => x.PurchaseOrder).ThenInclude(x => x.Supplier)
                .Include(p => p.BudgetItems).ThenInclude(x => x.PurchaseOrderItems).ThenInclude(x => x.PurchaseOrderReceiveds);
                Expression<Func<Project, bool>> criteria = x => x.Id == request.ProjectId;
                string cacheKey = StaticClass.BudgetItems.Cache.GetAllWithPurchaseOrder(request.ProjectId);

                return await repository.GetAsync(Cache: cacheKey, Includes: includes, Criteria: criteria);
            }


        }
    }
}