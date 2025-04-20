using Server.Database.Entities.PurchaseOrders;
using Shared.Enums.PurchaseOrderStatusEnums;
using Shared.Models.PurchaseOrders.Requests;
using static Shared.StaticClasses.StaticClass;

namespace Server.EndPoint.PurchaseOrders.Commands
{
    public static class ReceivePurchaseOrderApprovedEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.PurchaseOrders.EndPoint.Receive, async (ReceivePurchaseOrderApprovedRequest Data, IRepository Repository) =>
                {
                    Func<IQueryable<PurchaseOrder>, IIncludableQueryable<PurchaseOrder, object>> Includes = x => x
                     .Include(x => x.PurchaseOrderItems)
                    ;

                    Expression<Func<PurchaseOrder, bool>> Criteria = x => x.Id == Data.Id;

                    var row = await Repository.GetAsync(Criteria: Criteria, Includes: Includes);
                    if (row == null) { return Result.Fail(Data.Fail); }
                 
                    row.PurchaseOrderStatus = Data.IsCompletedReceived ? PurchaseOrderStatusEnum.Closed.Id : PurchaseOrderStatusEnum.Receiving.Id;
                    row.ClosedDate = Data.IsCompletedReceived ? DateTime.UtcNow : null;
                    int order = 1;
                    foreach (var item in Data.PurchaseOrderItems)
                    {
                        var purchaseorderitem = await Repository.GetByIdAsync<PurchaseOrderItem>(item.Id);
                        if (purchaseorderitem != null)
                        {
                            var Received = purchaseorderitem.AddPurchaseOrderReceived();
                            Received.CurrencyDate = Data.ReceivingDate;
                            Received.USDEUR = Data.ReceivingUSDEUR;
                            Received.USDCOP = Data.ReceivingUSDCOP;
                            Received.ValueReceivedCurrency = item.ReceivingValueCurrency;
                            Received.Order = order;
                            await Repository.AddAsync(Received);
                            order++;
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
