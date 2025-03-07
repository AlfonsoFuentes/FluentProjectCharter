using Shared.Enums.BudgetItemTypes;
using Shared.Enums.CostCenter;
using Shared.Models.BudgetItems.Responses;
using System.Globalization;

namespace Shared.Models.BudgetItems.IndividualItems.Taxs.Responses
{
    public class TaxResponse : BaseResponse, IBudgetItemResponse
    {
        public bool Selected { get; set; }
        public Guid DeliverableId { get; set; }
        public Guid ProjectId { get; set; }



        public string Nomenclatore { get; set; } = string.Empty;

        public string UpadtePageName { get; set; } = StaticClass.Taxs.PageName.Update;
        public string Tag { get; set; } = string.Empty;

        public double Budget { get; set; }
        public double Percentage { get; set; }
        public string sBudget => string.Format(new CultureInfo("en-US"), "{0:C0}", Budget);
        public List<TaxItemResponse> TaxItems { get; set; } = new List<TaxItemResponse>();
        public double BudgetTaxItem => TaxItems.Count == 0 ? 0 : TaxItems.Sum(x => x.Budget);

        double BudgetCalculated => BudgetTaxItem * Percentage / 100;
    }
}
