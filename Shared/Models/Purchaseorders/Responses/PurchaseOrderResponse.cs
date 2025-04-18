using Shared.Enums.CostCenter;
using Shared.Enums.CurrencyEnums;
using Shared.Enums.PurchaseOrderStatusEnums;
using Shared.Models.Deliverables.Responses;
using Shared.Models.Projects.Reponses;
using Shared.Models.Suppliers.Responses;

namespace Shared.Models.PurchaseOrders.Responses
{
    public class PurchaseOrderResponse : BaseResponse
    {

        public string SupplierName => Supplier == null ? string.Empty : Supplier.NickName;
        public SupplierResponse Supplier { get; set; } = null!;
        public CurrencyEnum PurchaseOrderCurrency { get; set; } = CurrencyEnum.None;
        public CostCenterEnum CostCenter { get; set; } = CostCenterEnum.None;

        public string QuoteNo { get; set; } = string.Empty;
        public CurrencyEnum QuoteCurrency { get; set; } = CurrencyEnum.None;
        public PurchaseOrderStatusEnum PurchaseOrderStatus { get; set; } = PurchaseOrderStatusEnum.None;
        public string PurchaseRequisition { get; set; } = string.Empty;
        public DateTime? ApprovedDate { get; set; }
        public DateTime? ExpectedDate { get; set; }
        public DateTime? ClosedDate { get; set; }
        public string PONumber { get; set; } = string.Empty;
        public string SPL { get; set; } = string.Empty;
        public string TaxCode { get; set; } = string.Empty;
        public double USDCOP { get; set; }
        public double USDEUR { get; set; }
        public DateTime CurrencyDate { get; set; }
        public string ProjectAccount { get; set; } = string.Empty;
        public bool IsAlteration { get; set; } = false;
        public bool IsCapitalizedSalary { get; set; } = false;
        public bool IsTaxEditable { get; set; } = false;
        public bool IsProductive { get; set; } = false;
        public Guid MainBudgetItemId { get; set; } = Guid.Empty;
        public Guid ProjectId { get; set; } = Guid.Empty;
        public List<PurchaseOrderItemResponse> PurchaseOrderItems { get; set; } = new();
        public double TotalUSD => PurchaseOrderItems.Count == 0 ? 0 : PurchaseOrderItems.Sum(x => x.TotalUSD);
        public string sTotalUSD => string.Format(new CultureInfo("en-US"), "{0:C0}", TotalUSD);
    }
}
