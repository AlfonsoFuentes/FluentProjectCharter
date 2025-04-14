using Shared.Enums.CostCenter;
using Shared.Enums.CurrencyEnums;
using Shared.Models.FileResults.Generics.Request;
using Shared.Models.Suppliers.Responses;

namespace Shared.Models.PurchaseOrders.Requests
{
    public abstract class PurchaseOrderRequest : CreateMessageResponse
    {
        public Guid MainBudgetItemId { get; set; }
        public SupplierResponse Supplier { get; set; } = null!;
        public Guid? SupplierId => Supplier == null ? Guid.Empty : Supplier.Id;
        public string VendorCode => Supplier == null ? string.Empty : Supplier.VendorCode;
        public string TaxCodeLD { get; set; } = "751545";
        public string TaxCodeLP { get; set; } = "721031";
        public string QuoteNo { get; set; } = string.Empty;
        public string SPL => IsAlteration ? "0735015000" : "151605000";
        public bool IsAlteration { get; set; }
        public bool IsProductive { get; set; }
        public bool IsCapitalizedSalary { get; set; }
        public CostCenterEnum CostCenter { get; set; } = CostCenterEnum.None;
        public string TaxCode => IsAlteration || !IsProductive ? TaxCodeLP : TaxCodeLD;
        public DateTime? CurrencyDate { get; set; } = DateTime.UtcNow;
        public string CurrencyDateString => CurrencyDate == null ? string.Empty : CurrencyDate.Value.ToString("d");
        public string PurchaseRequisition { get; set; } = string.Empty;
        public Guid ProjectId { get; set; } = Guid.Empty;
        public string ProjectAccount { get; set; } = string.Empty;
        CurrencyEnum _PurchaseOrderCurrency = CurrencyEnum.None;
        public CurrencyEnum PurchaseOrderCurrency
        {
            get { return _PurchaseOrderCurrency; }
            set
            {
                _PurchaseOrderCurrency = value;
                foreach (var item in PurchaseOrderItems)
                {
                    item.PurchaseOrderCurrency = _PurchaseOrderCurrency;
                }

            }
        }

        CurrencyEnum _QuoteCurrency = CurrencyEnum.None;
        public CurrencyEnum QuoteCurrency
        {
            get { return _QuoteCurrency; }
            set
            {
                _QuoteCurrency = value;
                foreach (var item in PurchaseOrderItems)
                {
                    item.QuoteCurrency = _QuoteCurrency;
                }

            }
        }

        double _USDCOP = 0;
        public double USDCOP
        {
            get { return _USDCOP; }
            set
            {
                _USDCOP = value;
                foreach (var item in PurchaseOrderItems)
                {
                    item.USDCOP = _USDCOP;
                }

            }
        }
        double _USDEUR = 0;
        public double USDEUR
        {
            get { return _USDEUR; }
            set
            {
                _USDEUR = value;
                foreach (var item in PurchaseOrderItems)
                {
                    item.USDEUR = _USDEUR;
                }

            }
        }
        public string Name { set; get; } = string.Empty;

        public virtual List<PurchaseOrderItemRequest> PurchaseOrderItems { set; get; } = new();
        public void AddItem(PurchaseOrderItemRequest item)
        {
            PurchaseOrderItems.Add(item);
            item.PurchaseOrderCurrency = PurchaseOrderCurrency;
            item.USDCOP = USDCOP;
            item.USDEUR = USDEUR;
            item.QuoteCurrency = QuoteCurrency;
        }
        public bool IsAnyValueNotDefined => PurchaseOrderItems.Any(x => x.TotalQuoteCurrency <= 0);
        public bool IsAnyNameEmpty => PurchaseOrderItems.Any(x => string.IsNullOrEmpty(x.Name));
        public double TotalQuoteCurrency => PurchaseOrderItems.Count == 0 ? 0 : PurchaseOrderItems.Sum(x => x.TotalQuoteCurrency);
        public double TotalPurchaseOrderCurrency => PurchaseOrderItems.Count == 0 ? 0 : PurchaseOrderItems.Sum(x => x.TotalPurchaseOrderCurrency);
        public double TotalUSD => PurchaseOrderItems.Count == 0 ? 0 : PurchaseOrderItems.Sum(x => x.TotalUSD);
        public string sTotalQuoteCurrency => string.Format(new CultureInfo("en-US"), "{0:C0}", TotalQuoteCurrency);
        public string sTotalPurchaseOrderCurrency => string.Format(new CultureInfo("en-US"), "{0:C0}", TotalPurchaseOrderCurrency);
        public string sTotalUSD => string.Format(new CultureInfo("en-US"), "{0:C0}", TotalUSD);

    }
}
