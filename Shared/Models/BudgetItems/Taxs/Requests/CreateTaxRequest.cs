using Shared.Enums.CostCenter;
using Shared.Models.FileResults.Generics.Request;

namespace Shared.Models.BudgetItems.Taxs.Requests
{
    public class CreateTaxRequest : CreateMessageResponse, IRequest
    {
        public Guid DeliverableId { get; set; }
        public string Name { get; set; } = string.Empty;
        public Guid ProjectId { get; set; }
        public string EndPointName => StaticClass.Taxs.EndPoint.Create;

        public override string Legend => Name;

        public override string ClassName => StaticClass.Taxs.ClassName;


        public double Budget { get; set; }

       
        public string sBudget => string.Format(new CultureInfo("en-US"), "{0:C0}", Budget);
    }
}
