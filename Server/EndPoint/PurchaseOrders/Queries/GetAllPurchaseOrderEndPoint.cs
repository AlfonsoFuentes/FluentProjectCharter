using Server.Database.Entities.PurchaseOrders;
using Shared.Enums.CurrencyEnums;
using Shared.Enums.PurchaseOrderStatusEnums;
using Shared.Models.PurchaseOrders.Records;
using Shared.Models.PurchaseOrders.Responses;
using Shared.Models.Qualitys.Responses;

namespace Server.EndPoint.PurchaseOrders.Queries
{
    public static class GetAllPurchaseOrderEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.PurchaseOrders.EndPoint.GetAll, async (PurchaseOrderGetAll Data, IQueryRepository Repository) =>
                {
                    Expression<Func<PurchaseOrder, bool>> Criteria = x => x.PurchaseOrderStatus == Data.Status.Id;
                    Func<IQueryable<PurchaseOrder>, IIncludableQueryable<PurchaseOrder, object>> Includes = x => x
                    .Include(x => x.Project)
                    .Include(p => p.PurchaseOrderItems).ThenInclude(x => x.BudgetItem!)
                    .Include(x => x.Supplier!);
                    var cache = Data.Status.Id == PurchaseOrderStatusEnum.Created.Id ?
                                StaticClass.PurchaseOrders.Cache.GetAllCreated :
                                Data.Status.Id == PurchaseOrderStatusEnum.Approved.Id ? StaticClass.PurchaseOrders.Cache.GetAllApproved :
                                StaticClass.PurchaseOrders.Cache.GetAllClosed;
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
                ProjectAccount = row.ProjectAccount,
                CurrencyDate = row.CurrencyDate,
                Id = row.Id,
                IsAlteration = row.IsAlteration,
                IsCapitalizedSalary = row.IsCapitalizedSalary,
                IsTaxEditable = row.IsTaxEditable,
                Name = row.PurchaseorderName,
                PurchaseOrderCurrency = row.PurchaseOrderCurrencyEnum,
                PurchaseOrderStatus = row.PurchaseOrderStatusEnum,
                CostCenter = row.CostCenterEnum,
                IsProductive = row.Project.IsProductiveAsset,
                QuoteCurrency = row.QuoteCurrencyEnum,
                PurchaseRequisition = row.PurchaseRequisition,
                QuoteNo = row.QuoteNo,
                SPL = row.SPL,
                TaxCode = row.TaxCode,
                USDCOP = row.USDCOP,
                USDEUR = row.USDEUR,
                ApprovedDate = row.ApprovedDate,
                ClosedDate = row.ClosedDate,
                ExpectedDate = row.ExpectedDate,
                PONumber = row.PONumber,
                ProjectId = row.ProjectId,
                MainBudgetItemId = row.MainBudgetItemId,
                Supplier = row.Supplier == null ? null! : new()
                {
                    Id = row.Supplier.Id,
                    Name = row.Supplier.Name,
                    NickName = row.Supplier.NickName,
                    VendorCode = row.Supplier.VendorCode,
                    TaxCodeLD = row.Supplier.TaxCodeLD,
                    SupplierCurrency = row.Supplier.SupplierCurrencyEnum,
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
                BudgetItemId = row.BudgetItemId!.Value,
                Quantity = row.Quantity,
                PurchaseOrderCurrency = row.PurchaseOrderCurrency,
                QuoteCurrency = row.QuoteCurrency,
                UnitaryQuoteCurrency = row.UnitaryValueCurrency,
                USDCOP = row.USDCOP,
                USDEUR = row.USDEUR,
                Order = row.Order,

            };
        }

    }
}
