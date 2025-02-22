using Shared.Enums.CostCenter;
using Shared.Models.FileResults.Generics.Request;

namespace Shared.Models.BudgetItems.IndividualItems.EHSs.Requests
{
    public class UpdateEHSRequest : UpdateMessageResponse, IRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Guid? DeliverableId { get; set; }
        public Guid ProjectId { get; set; }
        public string EndPointName => StaticClass.EHSs.EndPoint.Update;

        public override string Legend => Name;

        public override string ClassName => StaticClass.EHSs.ClassName;

        public double UnitaryCost { get; set; }
        public double Quantity { get; set; }
        public double Budget => UnitaryCost * Quantity;

        public string sUnitaryCost => string.Format(new CultureInfo("en-US"), "{0:C0}", UnitaryCost);
        public string sQuantity => $"{Quantity}";
        public string sBudget => string.Format(new CultureInfo("en-US"), "{0:C0}", Budget);
    }
}
