using Shared.Enums.BudgetItemTypes;
using Shared.Models.FileResults.Generics.Reponses;

namespace Shared.Models.BudgetItems
{
    public interface IBudgetItemResponse : IResponse
    {
        string Nomenclatore { get; set; }
        double Budget { get; }
        string sBudget { get; }
        string UpadtePageName { get; set; }
        string Tag { get; }
    }
}
