namespace Shared.Models.BudgetItems.Responses
{
    public class BudgetItemResponse : IBudgetItemResponse
    {
        public bool Selected { get; set; }
        public string Nomenclatore { get; set; } = string.Empty;

        public double Budget { get; set; }

        public string sBudget { get; set; } = string.Empty;

        public string UpadtePageName { get; set; } = string.Empty;

        public string Tag { get; set; } = string.Empty;

        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
