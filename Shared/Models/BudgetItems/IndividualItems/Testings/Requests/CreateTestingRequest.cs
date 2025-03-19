using Shared.Enums.CostCenter;
using Shared.Models.FileResults.Generics.Request;

namespace Shared.Models.BudgetItems.IndividualItems.Testings.Requests
{
    public class CreateTestingRequest : CreateMessageResponse, IRequest
    {
        public Guid? GanttTaskId { get; set; }
        public Guid ProjectId { get; set; }
        public string Name { get; set; } = string.Empty;
  
        public string EndPointName => StaticClass.Testings.EndPoint.Create;

        public override string Legend => Name;

        public override string ClassName => StaticClass.Testings.ClassName;

        public CostCenterEnum CostCenter { get; set; } = CostCenterEnum.None;
        public double UnitaryCost { get; set; }
        public double Quantity { get; set; }
        public double BudgetUSD => UnitaryCost * Quantity;

        public string sUnitaryCost => string.Format(new CultureInfo("en-US"), "{0:C0}", UnitaryCost);
        public string sQuantity => $"{Quantity}";
        public string sBudget => string.Format(new CultureInfo("en-US"), "{0:C0}", BudgetUSD);
    }
}
