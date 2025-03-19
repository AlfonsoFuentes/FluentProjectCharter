using Shared.Models.BudgetItems.Records;
using Shared.Models.BudgetItems.Responses;

namespace Server.EndPoint.BudgetItems.Queries
{
    public static class GetAllBudgetItemWithPurchaseOrdersByIdEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.BudgetItems.EndPoint.GetWithPurchaseorderById, async (BudgetItemWithPurchaseOrderGetById request, IQueryRepository repository) =>
                {
                    Func<IQueryable<BudgetItem>, IIncludableQueryable<BudgetItem, object>> includes = x =>
                    x.Include(p => p.PurchaseOrderItems).ThenInclude(x => x.PurchaseOrderReceiveds)
                    ;

                    Expression<Func<BudgetItem, bool>> criteria = x => x.Id == request.Id;
                    string cacheKey = StaticClass.BudgetItems.Cache.GetByIdWithPurchaseOrder(request.Id);

                    var row = await repository.GetAsync(Cache: cacheKey, Includes: includes, Criteria: criteria);

                    if (row == null)
                    {
                        return Result<BudgetItemWithPurchaseOrdersResponse>.Fail(
                            StaticClass.ResponseMessages.ReponseNotFound(StaticClass.BudgetItems.ClassLegend));
                    }

                    BudgetItemWithPurchaseOrdersResponse response = new()
                    {
                        Id = row.Id,
                        ActualUSD = row.ActualUSD,
                        BudgetUSD = row.BudgetUSD,
                        CommitmentUSD = row.CommitmentUSD,
                        Name = row.Name,
                        Nomenclatore = row.Nomenclatore,
                        PotentialUSD = row.PotentialUSD,
                        ProjectId = row.ProjectId,
                        IsAlteration = row.IsAlteration,
                        IsTaxes = row.IsTaxes,



                    };



                    return Result<BudgetItemWithPurchaseOrdersResponse>.Success(response);
                });
            }



        }
    }
}