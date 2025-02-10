using Shared.Enums.BudgetItemTypes;
using Shared.Models.FileResults.Generics.Reponses;

namespace Shared.Models.BudgetItems.Responses
{
    public interface IBudgetItemResponse : IResponse
    {
        bool Selected { get; set; }
        string Nomenclatore { get; set; }
        double Budget { get; }
        string sBudget { get; }
        string UpadtePageName { get; set; }
        string Tag { get; }
    }
}
