using Shared.Enums.BudgetItemTypes;
using Shared.Models.FileResults.Generics.Reponses;

namespace Shared.Models.BudgetItems.Responses
{
    public interface IBudgetItemResponse : IResponse
    {
        bool Selected { get; set; }
        string Nomenclatore { get; set; }
        double BudgetUSD { get; }

        string Tag { get; }
        bool IsAlteration { get; set; }
        bool IsTaxes { get; set; }
        bool ShowDetails { get; set; }
    }
    public interface IBudgetItemWithPurchaseOrderResponse : IBudgetItemResponse
    {

        string NomenclatoreName { get; }
        double ActualUSD { get; set; }
        double CommitmentUSD { get; set; }
        double PotentialUSD { get; set; }
        double AssignedUSD { get; }
        double ToCommitUSD { get; }
    
        
        
    }
}
