using Shared.Enums.CostCenter;
using Shared.Models.FileResults.Generics.Request;

namespace Shared.Models.BudgetItems.IndividualItems.Foundations.Requests
{
    public class CreateFoundationRequest : CreateMessageResponse, IRequest
    {

        public Guid ProjectId { get; set; }
        public string Name { get; set; } = string.Empty;

        public string EndPointName => StaticClass.Foundations.EndPoint.Create;

        public override string Legend => Name;

        public override string ClassName => StaticClass.Foundations.ClassName;

        public CostCenterEnum CostCenter { get; set; } = CostCenterEnum.None;
        public double UnitaryCost { get; set; }
        public double Quantity { get; set; }
        public double Budget => UnitaryCost * Quantity;

        public string sUnitaryCost => string.Format(new CultureInfo("en-US"), "{0:C0}", UnitaryCost);
        public string sQuantity => $"{Quantity}";
        public string sBudget => string.Format(new CultureInfo("en-US"), "{0:C0}", Budget);
    }
}
