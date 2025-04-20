using Server.Database.Entities.PurchaseOrders;
using Shared.Enums.PurchaseOrderStatusEnums;
using Shared.Models.PurchaseOrders.Requests;

namespace Server.EndPoint.PurchaseOrders.Commands
{
    public static class EditPurchaseOrderClosedApprovedEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.PurchaseOrders.EndPoint.EditClosed, async (EditPurchaseOrderClosedRequest Data, IRepository Repository) =>
                {
                    Func<IQueryable<PurchaseOrder>, IIncludableQueryable<PurchaseOrder, object>> Includes = x => x
                     .Include(x => x.PurchaseOrderItems)
                    ;

                    Expression<Func<PurchaseOrder, bool>> Criteria = x => x.Id == Data.Id;

                    var row = await Repository.GetAsync(Criteria: Criteria, Includes: Includes);
                    if (row == null) { return Result.Fail(Data.Fail); }

                    row.PurchaseOrderStatus = Data.IsCompletedReceived ? PurchaseOrderStatusEnum.Closed.Id : PurchaseOrderStatusEnum.Receiving.Id;
                    row.ClosedDate = Data.IsCompletedReceived ? DateTime.UtcNow : null;
                    foreach (var item in Data.PurchaseOrderItemReceiveds)
                    {
                        var received = await Repository.GetByIdAsync<PurchaseOrderItemReceived>(item.Id);
                        if (received != null)
                        {

                            received.CurrencyDate = item.CurrencyDate;
                            received.USDEUR = item.USDEUR;
                            received.USDCOP = item.USDCOP;
                            received.ValueReceivedCurrency = item.ValueReceivedCurrency;
                            await Repository.UpdateAsync(received);
                        }

                    }

                    await Repository.UpdateAsync(row);
                    List<string> cache = [.. StaticClass.PurchaseOrders.Cache.KeyClosed(row.Id, row.ProjectId)];

                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(cache.ToArray());

                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);


                });


            }
        }


       

    }
}
