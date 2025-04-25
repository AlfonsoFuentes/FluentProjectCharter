namespace Shared.Models.BudgetItems.Responses
{
    public abstract class BudgetItemBaseResponse : BaseResponse
    {
        public bool Selected { get; set; }
        public virtual double BudgetUSD { get; set; }
        public string Nomenclatore { get; set; } = string.Empty;
       
      

    }
}
