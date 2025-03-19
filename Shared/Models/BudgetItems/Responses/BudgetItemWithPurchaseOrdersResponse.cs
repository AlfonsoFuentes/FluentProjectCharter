using Shared.Models.PurchaseOrders.Responses;

namespace Shared.Models.BudgetItems.Responses
{
    public class BudgetItemWithPurchaseOrdersResponse : IBudgetItemWithPurchaseOrderResponse
    {
        public Guid ProjectId { get; set; }
        public bool Selected { get; set; }
        public string Nomenclatore { get; set; } = string.Empty;
        public virtual bool IsAlteration { get; set; } = false;
        public virtual bool IsTaxes { get; set; } = false;

        public virtual string UpadtePageName { get; set; } = string.Empty;
        public virtual string Tag { get;  } = string.Empty;
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<PurchaseOrderResponse> PurchaseOrders { get; set; } = new();
        public double ActualUSD { get; set; }
        public double CommitmentUSD { get; set; }
        public double PotentialUSD { get; set; }
        public double BudgetUSD { get; set; }
        public double AssignedUSD => ActualUSD + CommitmentUSD + PotentialUSD;
        public double ToCommitUSD => BudgetUSD - AssignedUSD;
        public string sBudgetUSD => string.Format(new CultureInfo("en-US"), "{0:C0}", BudgetUSD);
        public string sActualUSD => string.Format(new CultureInfo("en-US"), "{0:C0}", ActualUSD);
        public string sCommitmentUSD => string.Format(new CultureInfo("en-US"), "{0:C0}", CommitmentUSD);
        public string sPotentialUSD => string.Format(new CultureInfo("en-US"), "{0:C0}", PotentialUSD);
        public string sAssignedUSD => string.Format(new CultureInfo("en-US"), "{0:C0}", AssignedUSD);
        public string sToCommitUSD => string.Format(new CultureInfo("en-US"), "{0:C0}", ToCommitUSD);
    }
}
