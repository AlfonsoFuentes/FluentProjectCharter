using DocumentFormat.OpenXml.Office.CoverPageProps;
using Server.Database.Contracts;
using Server.Database.Entities.BudgetItems.Taxes;
using Server.Database.Entities.ProjectManagements;
using Server.Database.Entities.PurchaseOrders;
using Shared.Models.BudgetItems;
using System.ComponentModel.DataAnnotations.Schema;
using static Shared.StaticClasses.StaticClass;

namespace Server.Database.Entities.BudgetItems
{
    public abstract class BudgetItem : AuditableEntity<Guid>, ITenantEntity
    {
        public string TenantId { get; set; } = string.Empty;

        public virtual string Letter { get; set; } = string.Empty;
        public virtual double BudgetUSD { get; set; }
        public bool IsAlteration { get; set; } = false;
        public bool IsTaxes { get; set; } = false;
        public Project Project { get; set; } = null!;
        public Guid ProjectId { get; set; }


        [ForeignKey("SelectedId")]
        public List<TaxesItem> TaxesSelecteds { get; set; } = new();
        [NotMapped]
        public string Nomenclatore => $"{Letter}{Order}";
        public string Name { get; set; } = string.Empty;

        //public GanttTask? GanttTask { get; set; } = null!;
        //public Guid? GanttTaskId { get; set; }

        [ForeignKey("BudgetItemId")]
        public List<PurchaseOrderItem> PurchaseOrderItems { get; set; } = new();

        [NotMapped]
        public string NomenclatoreName => $"{Nomenclatore}-{Name}";
        [NotMapped]
        public double ActualUSD => PurchaseOrderItems == null || PurchaseOrderItems.Count == 0 ? 0 : PurchaseOrderItems.Sum(x => x.ActualItemUSD);
        [NotMapped]
        public double CommitmentUSD => PurchaseOrderItems == null || PurchaseOrderItems.Count == 0 ? 0 : PurchaseOrderItems.Sum(x => x.CommitmentItemUSD);
        [NotMapped]
        public double PotentialUSD => PurchaseOrderItems == null || PurchaseOrderItems.Count == 0 ? 0 : PurchaseOrderItems.Sum(x => x.PotentialItemUSD);
        [NotMapped]
        public double AssignedUSD => ActualUSD + CommitmentUSD + PotentialUSD;
        [NotMapped]
        public double ToCommitUSD => BudgetUSD - AssignedUSD;

        [NotMapped]
        public PurchaseOrder? PurchaseOrder => PurchaseOrderItems == null || PurchaseOrderItems.Count == 0 ? null : PurchaseOrderItems.Select(x => x.PurchaseOrder).First();
        //[NotMapped]
        //public DateTime? PlannedDate => GanttTask == null ? null : GanttTask.PlannedEndDate;
        [NotMapped]
        public DateTime? ExpectedDate => PurchaseOrder == null ? null : PurchaseOrder.ExpectedDate;
        [NotMapped]
        public DateTime? ClosedDate => PurchaseOrder == null ? null : PurchaseOrder.ClosedDate;

        //[NotMapped]
        //public DateTime? ExpenseToolDate => ClosedDate != null ? ClosedDate : ExpectedDate != null ? ExpectedDate :
        //    PlannedDate != null ? PlannedDate : null;
    }

}
