namespace Shared.Models.BudgetItems.Responses
{
    public class BudgetItemPurchaseOrderResponse 
    {
        
        public string Nomenclatore { get; set; } = string.Empty;
        public double Budget { get; set; }
        public string sBudget { get; set; } = string.Empty;
      
        public string Tag { get; set; } = string.Empty;
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool IsAlteration { get; set; }
        public bool IsCapitalizedSalary {  get; set; }
        public bool IsTaxesMainTaxesData {  get; set; }
    }
}
