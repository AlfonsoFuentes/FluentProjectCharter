using Shared.Enums.BudgetItemTypes;
using Shared.Enums.CostCenter;
using Shared.Models.BudgetItems.Responses;
using System.Globalization;

namespace Shared.Models.BudgetItems.IndividualItems.Taxs.Responses
{
    public class TaxResponse : BudgetItemWithPurchaseOrdersResponse
    {

        public Guid DeliverableId { get; set; }



        public override string UpadtePageName { get; set; } = StaticClass.Taxs.PageName.Update;
        public double Percentage { get; set; }
      
        public List<TaxItemResponse> TaxItems { get; set; } = new List<TaxItemResponse>();
        public double BudgetTaxItem => TaxItems.Count == 0 ? 0 : TaxItems.Sum(x => x.Budget);

        double BudgetCalculated => BudgetTaxItem * Percentage / 100;

       
    }
}
