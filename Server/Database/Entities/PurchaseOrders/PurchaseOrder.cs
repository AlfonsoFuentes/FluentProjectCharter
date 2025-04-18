using DocumentFormat.OpenXml.Office.CoverPageProps;
using Server.Database.Entities.ProjectManagements;
using Shared.Enums.CostCenter;
using Shared.Enums.CurrencyEnums;
using Shared.Enums.PurchaseOrderStatusEnums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Database.Entities.PurchaseOrders
{
    public class PurchaseOrder : AuditableEntity<Guid>, ITenantEntity
    {
        public string TenantId { get; set; } = string.Empty;
        public Project Project { get; set; } = null!;
        public Guid ProjectId { get; set; }
        public Guid? SupplierId { get; set; }
        public Supplier? Supplier { get; set; } = null!;
        public ICollection<PurchaseOrderItem> PurchaseOrderItems { get; set; } = new List<PurchaseOrderItem>();
        public static PurchaseOrder Create(Guid projectid)
        {
            return new()
            {
                ProjectId = projectid,
                Id = Guid.NewGuid(),

            };
            
        }

       
        public string QuoteNo { get; set; } = "";
      
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
        public string PurchaseorderName { get; set; } = string.Empty;
        public bool IsAlteration { get; set; } = false;
        public bool IsCapitalizedSalary { get; set; } = false;
        public bool IsTaxEditable { get; set; } = false;
        public int QuoteCurrency { get; set; }
        public int PurchaseOrderCurrency { get; set; }
        public int PurchaseOrderStatus { get; set; }
        public int CostCenter { get; set; }
        public Guid MainBudgetItemId { get; set; } = Guid.Empty;
        [NotMapped]
        public CostCenterEnum CostCenterEnum => CostCenterEnum.GetType(CostCenter);
        [NotMapped]
        public PurchaseOrderStatusEnum PurchaseOrderStatusEnum => PurchaseOrderStatusEnum.GetType(PurchaseOrderStatus);
        [NotMapped]
        public CurrencyEnum PurchaseOrderCurrencyEnum => CurrencyEnum.GetType(PurchaseOrderCurrency);
        [NotMapped]
        public CurrencyEnum QuoteCurrencyEnum => CurrencyEnum.GetType(QuoteCurrency);

        [NotMapped]
        public double ActualCurrency => PurchaseOrderItems == null || PurchaseOrderItems.Count == 0 ? 0 :
            PurchaseOrderItems.Sum(x => x.POItemActualCurrency);
        [NotMapped]
        public double ActualUSD => PurchaseOrderItems == null || PurchaseOrderItems.Count == 0 ? 0 :
            PurchaseOrderItems.Sum(x => x.ActualUSD);

        [NotMapped]
        public double AssignedUSD => PurchaseOrderItems == null || PurchaseOrderItems.Count == 0 ? 0 :
            PurchaseOrderItems.Sum(x => x.POItemValueUSD);
        [NotMapped]
        public double ApprovedUSD => PurchaseOrderItems == null || PurchaseOrderItems.Count == 0 ? 0 :
            PurchaseOrderItems.Sum(x => x.ApprovedUSD);
        [NotMapped]
        public double PotentialCommitmentUSD => PurchaseOrderItems == null || PurchaseOrderItems.Count == 0 ? 0 :
            PurchaseOrderItems.Sum(x => x.PotentialCommitmentUSD);

        [NotMapped]
        public double CommitmentUSD => PurchaseOrderItems == null || PurchaseOrderItems.Count == 0 ? 0 :
            PurchaseOrderItems.Sum(x => x.CommitmentUSD);
        [NotMapped]
        public double CommitmentCurrency => PurchaseOrderItems == null || PurchaseOrderItems.Count == 0 ? 0 :
            PurchaseOrderItems.Sum(x => x.CommitmentCurrency);
        [NotMapped]
        public double QuoteValueCurrency => PurchaseOrderItems == null || PurchaseOrderItems.Count == 0 ? 0 :
            PurchaseOrderItems.Sum(x => x.POItemQuoteValueCurrency);
        [NotMapped]
        public string SupplierName => Supplier == null ? string.Empty : Supplier.Name;
        [NotMapped]
        public string SupplierNickName => Supplier == null ? string.Empty : Supplier.NickName;
    }
}
