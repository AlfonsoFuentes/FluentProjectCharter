using Server.Database.Entities.PurchaseOrders;
using Shared.Enums.PurchaseOrderStatusEnums;
using Shared.Models.PurchaseOrders.Requests;

namespace Server.EndPoint.PurchaseOrders.Commands
{

    public static class CreatePurchaseOrderEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.PurchaseOrders.EndPoint.Create, async (CreatePurchaseOrderRequest Data, IRepository Repository) =>
                {

                    var row = PurchaseOrder.Create(Data.ProjectId);


                    await Repository.AddAsync(row);
                    Data.Map(row);
                    foreach (var item in Data.PurchaseOrderItems)
                    {
                        var rowitem = PurchaseOrderItem.Create(row.Id, item.BudgetItemId);
                        rowitem.Name = item.Name;
                        rowitem.UnitaryValueCurrency = item.UnitaryQuoteCurrency;
                        rowitem.Quantity = item.Quantity;


                        await Repository.AddAsync(rowitem);
                    }

                    List<string> cache = [.. StaticClass.PurchaseOrders.Cache.Key(row.Id, row.ProjectId)];

                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(cache.ToArray());

                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);


                });


            }
        }


        static PurchaseOrder Map(this CreatePurchaseOrderRequest request, PurchaseOrder row)
        {
            row.SupplierId = request.SupplierId;
            row.QuoteCurrency = request.QuoteCurrency.Name;
            row.PurchaseOrderCurrency = request.PurchaseOrderCurrency.Name;
            row.PurchaseorderName = request.Name;
            row.PurchaseOrderStatus = PurchaseOrderStatusEnum.Created.Name;
            row.PurchaseRequisition = request.PurchaseRequisition;
            row.QuoteNo = request.QuoteNo;
            row.CurrencyDate = request.CurrencyDate!.Value;
            row.AccountAssigment = request.ProjectAccount;
            row.IsAlteration = request.IsAlteration;
            row.IsCapitalizedSalary = request.IsCapitalizedSalary;
            row.SPL = request.SPL;
            row.USDCOP = request.USDCOP;
            row.USDEUR = request.USDEUR;
            row.TaxCode = request.TaxCode;

            return row;
        }

    }

}
