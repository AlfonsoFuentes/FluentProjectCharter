using Shared.Enums.BudgetItemTypes;
using Shared.Models.FileResults.Generics.Reponses;

namespace Shared.Models.BudgetItems.Responses
{
    public interface IBudgetItemResponse : IResponse
    {
        bool Selected { get; set; }
        string Nomenclatore { get; set; }
        double BudgetUSD { get; }
        string sBudgetUSD { get; }
        string UpadtePageName { get; set; }
        string Tag { get; }
        bool IsAlteration { get; set; }
        bool IsTaxes { get; set; }
    }
    public interface IBudgetItemWithPurchaseOrderResponse : IBudgetItemResponse
    {
        double ActualUSD { get; set; }
        double CommitmentUSD { get; set; }
        double PotentialUSD { get; set; }
        double AssignedUSD { get; }
        double ToCommitUSD { get; }
        string sActualUSD { get; }
        string sCommitmentUSD { get; }
        string sPotentialUSD { get; }
        string sAssignedUSD { get; }
        string sToCommitUSD { get; }
    }
}
