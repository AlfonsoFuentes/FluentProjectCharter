using Shared.Enums.CostCenter;
using Shared.Models.BudgetItems.IndividualItems.Taxs.Responses;
using Shared.Models.FileResults.Generics.Request;

namespace Shared.Models.BudgetItems.IndividualItems.Taxs.Requests
{
    public class CreateTaxRequest : CreateMessageResponse, IRequest
    {
        public Guid? GanttTaskId { get; set; }
        public Guid ProjectId { get; set; }
        public string Name { get; set; } = string.Empty;
 
        public string EndPointName => StaticClass.Taxs.EndPoint.Create;

        public override string Legend => Name;

        public override string ClassName => StaticClass.Taxs.ClassName;
        public double BudgetUSD => BudgetCalculated;
        public double Percentage { get; set; }
        public string sBudget => string.Format(new CultureInfo("en-US"), "{0:C0}", BudgetUSD);
        public List<TaxItemResponse> TaxItems { get; set; } = new List<TaxItemResponse>();
        public double BudgetTaxItem => TaxItems.Count == 0 ? 0 : TaxItems.Sum(x => x.Budget);

        double BudgetCalculated => BudgetTaxItem * Percentage / 100;
    }
}
