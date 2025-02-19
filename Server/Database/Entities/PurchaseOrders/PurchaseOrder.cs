using DocumentFormat.OpenXml.Office.CoverPageProps;
using Server.Database.Entities.ProjectManagements;
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

        public Guid? DeliverableId { get; set; }
        public Deliverable? Deliverable { get; set; } = null!;
        public ICollection<PurchaseOrderItem> PurchaseOrderItems { get; set; } = new List<PurchaseOrderItem>();
        public static PurchaseOrder Create(Guid projectid)
        {
            return new()
            {
                ProjectId = projectid,
                Id = Guid.NewGuid(),

            };
            
        }

        public PurchaseOrderItem AddPurchaseOrderItem(Guid mwobudgetitemid)
        {
            var row = PurchaseOrderItem.Create(Id, mwobudgetitemid);
            return row;
        }
        public PurchaseOrderItem AddPurchaseOrderItemForNoProductiveTax(Guid mwobudgetitemid)
        {
            var row = PurchaseOrderItem.Create(Id, mwobudgetitemid);
            row.IsTaxNoProductive = true;
            return row;
        }
        public PurchaseOrderItem AddPurchaseOrderItemForAlteration(Guid mwobudgetitemid)
        {
            var row = PurchaseOrderItem.Create(Id, mwobudgetitemid);
            row.IsTaxAlteration = true;
            return row;
        }
        public string QuoteNo { get; set; } = "";
        public string QuoteCurrency { get; set; } = string.Empty;
        public string PurchaseOrderCurrency { get; set; } = string.Empty;
        public string PurchaseOrderStatus { get; set; }=string.Empty;
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

        public string PurchaseorderName { get; set; } = string.Empty;

        public bool IsAlteration { get; set; } = false;
        public bool IsCapitalizedSalary { get; set; } = false;

        public bool IsTaxEditable { get; set; } = false;
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
