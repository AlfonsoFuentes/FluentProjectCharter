using Shared.Models.PurchaseOrders.Requests;
using Shared.Models.PurchaseOrders.Responses;

namespace Shared.Models.PurchaseOrders.Mappers
{
    public static class PurchaseOrderMappers
    {
        public static EditPurchaseApprovedOrderRequest ToEditApproved(this PurchaseOrderResponse response)
        {
            return new()
            {

                CostCenter = response.CostCenter,
                CurrencyDate = response.CurrencyDate,
                IsAlteration = response.IsAlteration,
                IsCapitalizedSalary = response.IsCapitalizedSalary,
                IsProductive = response.IsProductive,
                MainBudgetItemId = response.MainBudgetItemId,
                Name = response.Name,
                PurchaseRequisition = response.PurchaseRequisition,
                ProjectAccount = response.ProjectAccount,
                ProjectId = response.ProjectId,
                PurchaseOrderCurrency = response.PurchaseOrderCurrency,
                Id = response.Id,
                PurchaseOrderItems = response.PurchaseOrderItems.Select(x => x.Map()).ToList(),
                QuoteCurrency = response.QuoteCurrency,
                QuoteNo = response.QuoteNo,
                Supplier = response.Supplier,
                TaxCodeLD = response.Supplier.TaxCodeLD,
                TaxCodeLP = response.Supplier.TaxCodeLP,
                USDCOP = response.USDCOP,
                USDEUR = response.USDEUR,
                PONumber = response.PONumber,
                ExpectedDate = response.ExpectedDate

            };
        }
        public static ApprovePurchaseOrderRequest ToApprove(this PurchaseOrderResponse response)
        {
            return new()
            {

                CostCenter = response.CostCenter,
                CurrencyDate = response.CurrencyDate,
                IsAlteration = response.IsAlteration,
                IsCapitalizedSalary = response.IsCapitalizedSalary,
                IsProductive = response.IsProductive,
                MainBudgetItemId = response.MainBudgetItemId,
                Name = response.Name,
                PurchaseRequisition = response.PurchaseRequisition,
                ProjectAccount = response.ProjectAccount,
                ProjectId = response.ProjectId,
                PurchaseOrderCurrency = response.PurchaseOrderCurrency,
                Id = response.Id,
                PurchaseOrderItems = response.PurchaseOrderItems.Select(x => x.Map()).ToList(),
                QuoteCurrency = response.QuoteCurrency,
                QuoteNo = response.QuoteNo,
                Supplier = response.Supplier,
                TaxCodeLD = response.Supplier.TaxCodeLD,
                TaxCodeLP = response.Supplier.TaxCodeLP,
                USDCOP = response.USDCOP,
                USDEUR = response.USDEUR,

            };
        }
        public static EditPurchaseOrderCreatedRequest ToEditCreated(this PurchaseOrderResponse response)
        {
            return new()
            {
                CostCenter = response.CostCenter,
                CurrencyDate = response.CurrencyDate,
                IsAlteration = response.IsAlteration,
                IsCapitalizedSalary = response.IsCapitalizedSalary,
                IsProductive = response.IsProductive,
                MainBudgetItemId = response.MainBudgetItemId,
                Name = response.Name,
                PurchaseRequisition = response.PurchaseRequisition,
                ProjectAccount = response.ProjectAccount,
                ProjectId = response.ProjectId,
                PurchaseOrderCurrency = response.PurchaseOrderCurrency,
                Id = response.Id,
                PurchaseOrderItems = response.PurchaseOrderItems.Select(x => x.Map()).ToList(),
                QuoteCurrency = response.QuoteCurrency,
                QuoteNo = response.QuoteNo,
                Supplier = response.Supplier,
                TaxCodeLD = response.Supplier.TaxCodeLD,
                TaxCodeLP = response.Supplier.TaxCodeLP,
                USDCOP = response.USDCOP,
                USDEUR = response.USDEUR,



            };
        }
        public static PurchaseOrderItemRequest Map(this PurchaseOrderItemResponse response)
        {
            return new()
            {
                PurchaseOrderCurrency = response.PurchaseOrderCurrency,
                Id = response.Id,
                Name = response.Name,
                Quantity = response.Quantity,
                QuoteCurrency = response.QuoteCurrency,
                UnitaryQuoteCurrency = response.UnitaryQuoteCurrency,
                USDCOP = response.USDCOP,
                USDEUR = response.USDEUR,
                BudgetItemId = response.BudgetItemId,
                Order = response.Order,


            };
        }
    }
}
