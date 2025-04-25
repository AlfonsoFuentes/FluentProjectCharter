using Shared.Models.PurchaseOrders.Responses;

namespace Shared.Models.BudgetItems.Responses
{
    public class BudgetItemWithPurchaseOrdersResponse : IBudgetItemWithPurchaseOrderResponse
    {
      
        public bool ShowDetails { get; set; } = false;  
        public Guid ProjectId { get; set; }
        public bool Selected { get; set; }
        public string Nomenclatore { get; set; } = string.Empty;
        public virtual bool IsAlteration { get; set; } = false;
        public virtual bool IsTaxes { get; set; } = false;
        public string NomenclatoreName => $"{Nomenclatore}-{Name}";

        public virtual string Tag { get; init; } = string.Empty;
        public Guid Id { get; set; }=Guid.Empty;    
        public string Name { get; set; } = string.Empty;
        public List<PurchaseOrderResponse> PurchaseOrders { get; set; } = new();
        public double ActualUSD { get; set; }
        public double CommitmentUSD { get; set; }
        public double PotentialUSD { get; set; }
        public double BudgetUSD { get; set; }
        public double AssignedUSD => ActualUSD + CommitmentUSD + PotentialUSD;
        public double ToCommitUSD => BudgetUSD - AssignedUSD;

      

       
    }
}
