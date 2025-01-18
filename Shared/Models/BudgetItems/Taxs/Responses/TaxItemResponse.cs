namespace Shared.Models.BudgetItems.Taxs.Requests
{
    public class TaxItemResponse : BaseResponse
    {
        public Guid? BudgetItemId { get; set; }
        public string Nomenclatore { get; set; } = string.Empty;

        public double Budget { set; get; }
        public bool Selected {  get; set; }
    }
}
