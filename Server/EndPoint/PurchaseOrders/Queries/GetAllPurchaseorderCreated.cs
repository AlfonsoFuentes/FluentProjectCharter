using Server.Database.Entities.PurchaseOrders;
using Shared.Enums.CurrencyEnums;
using Shared.Enums.PurchaseOrderStatusEnums;
using Shared.Models.PurchaseOrders.Records;
using Shared.Models.PurchaseOrders.Responses;
using Shared.Models.Qualitys.Responses;

namespace Server.EndPoint.PurchaseOrders.Queries
{
    public static class GetAllPurchaseOrderCreated
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.PurchaseOrders.EndPoint.GetAllCreated, async (PurchaseOrderCreatedGetAll Data, IQueryRepository Repository) =>
                {
                    Expression<Func<PurchaseOrder, bool>> Criteria = x => x.PurchaseOrderStatus == PurchaseOrderStatusEnum.Created.Name;
                    Func<IQueryable<PurchaseOrder>, IIncludableQueryable<PurchaseOrder, object>> Includes = x => x
                    .Include(p => p.PurchaseOrderItems).ThenInclude(x => x.BudgetItem!)
                    .Include(x => x.Supplier!);
                    var cache = StaticClass.PurchaseOrders.Cache.GetAll;
                    var rows = await Repository.GetAllAsync(cache, Criteria: Criteria, Includes: Includes);
                    if (rows == null)
                    {
                        return Result<PurchaseOrderResponseList>.Fail(
                            StaticClass.ResponseMessages.ReponseNotFound(StaticClass.PurchaseOrders.ClassLegend));
                    }
                    PurchaseOrderResponseList response = new()
                    {
                        Items = rows.Select(x => x.Map()).ToList(),
                    };

                    return Result<PurchaseOrderResponseList>.Success(response);

                });
            }
        }
        public static PurchaseOrderResponse Map(this PurchaseOrder row)
        {
            return new()
            {
                AccountAssigment = row.AccountAssigment,
                CurrencyDate = row.CurrencyDate,
                Id = row.Id,
                IsAlteration = row.IsAlteration,
                IsCapitalizedSalary = row.IsCapitalizedSalary,
                IsTaxEditable = row.IsTaxEditable,
                Name = row.PurchaseorderName,
                PurchaseOrderCurrency = CurrencyEnum.GetType(row.PurchaseOrderCurrency),
                PurchaseOrderStatus = PurchaseOrderStatusEnum.Created.Name,
                QuoteCurrency = CurrencyEnum.GetType(row.QuoteCurrency),
                PurchaseRequisition = row.PurchaseRequisition,
                QuoteNo = row.QuoteNo,
                SPL = row.SPL,
                TaxCode = row.TaxCode,
                USDCOP = row.USDCOP,
                USDEUR = row.USDEUR,
                Supplier = row.Supplier == null ? null! : new()
                {
                    Id = row.Supplier.Id,
                    Name = row.Supplier.Name,
                    NickName = row.Supplier.NickName,
                    VendorCode = row.Supplier.VendorCode,
                    TaxCodeLD = row.Supplier.TaxCodeLD,
                    SupplierCurrency = CurrencyEnum.GetType(row.Supplier.SupplierCurrency),
                },

                PurchaseOrderItems = row.PurchaseOrderItems.Select(x => x.Map()).ToList(),
            };
        }
        public static PurchaseOrderItemResponse Map(this PurchaseOrderItem row)
        {
            return new()
            {
                Id = row.Id,
                Name = row.Name,
                BudgetItemId = row.BudgetItemId,
                Quantity = row.Quantity,
                PurchaseOrderCurrency = row.PurchaseOrderCurrency,
                QuoteCurrency = row.QuoteCurrency,
                UnitaryValueCurrency = row.UnitaryValueCurrency,
                USDCOP = row.USDCOP,
                USDEUR = row.USDEUR,

            };
        }
    }
}
