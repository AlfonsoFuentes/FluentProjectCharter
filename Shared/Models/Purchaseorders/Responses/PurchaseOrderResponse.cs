using Shared.Enums.CurrencyEnums;
using Shared.Models.Deliverables.Responses;
using Shared.Models.Projects.Reponses;
using Shared.Models.Suppliers.Responses;

namespace Shared.Models.PurchaseOrders.Responses
{
    public class PurchaseOrderResponse : BaseResponse
    {
      
        public SupplierResponse Supplier { get; set; } = null!;     
        public CurrencyEnum PurchaseOrderCurrency { get; set; } = CurrencyEnum.None;
      
        public string QuoteNo { get; set; } = "";
        public CurrencyEnum QuoteCurrency { get; set; } = CurrencyEnum.None;  
        public string PurchaseOrderStatus { get; set; } = string.Empty;
        public string PurchaseRequisition { get; set; } = "";
        public DateTime? ApprovedDate { get; set; }
        public DateTime? ExpectedDate { get; set; }
        public DateTime? ClosedDate { get; set; }
        public string PONumber { get; set; } = "";
        public string SPL { get; set; } = "";
        public string TaxCode { get; set; } = "";
        public double USDCOP { get; set; }
        public double USDEUR { get; set; }
        public DateTime CurrencyDate { get; set; }
        public string AccountAssigment { get; set; } = ""; 
        public bool IsAlteration { get; set; } = false;
        public bool IsCapitalizedSalary { get; set; } = false;
        public bool IsTaxEditable { get; set; } = false;

        public List<PurchaseOrderItemResponse> PurchaseOrderItems { get; set; } = new();
    }
}
