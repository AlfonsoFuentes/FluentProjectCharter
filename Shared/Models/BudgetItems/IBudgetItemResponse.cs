using Shared.Enums.BudgetItemTypes;

namespace Shared.Models.BudgetItems
{
    public interface IBudgetItemResponse
    {
        Guid Id { get; set; }
        string Name { get; set; }
        string Nomenclatore { get; set; }
        double Budget { get; }
        string sBudget { get; }
        string UpadtePageName { get; set; }
        string Tag { get; }
    }
}
