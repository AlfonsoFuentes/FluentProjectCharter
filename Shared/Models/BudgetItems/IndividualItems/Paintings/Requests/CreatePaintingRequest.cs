using Shared.Enums.CostCenter;
using Shared.Models.FileResults.Generics.Request;

namespace Shared.Models.BudgetItems.IndividualItems.Paintings.Requests
{
    public class CreatePaintingRequest : CreateMessageResponse, IRequest
    {
        public Guid? GanttTaskId { get; set; }
        public Guid ProjectId { get; set; }
        public string Name { get; set; } = string.Empty;

        public string EndPointName => StaticClass.Paintings.EndPoint.Create;

        public override string Legend => Name;

        public override string ClassName => StaticClass.Paintings.ClassName;

        public CostCenterEnum CostCenter { get; set; } = CostCenterEnum.None;
        public double UnitaryCost { get; set; }
        public double Quantity { get; set; }
        public double Budget => UnitaryCost * Quantity;

        public string sUnitaryCost => string.Format(new CultureInfo("en-US"), "{0:C0}", UnitaryCost);
        public string sQuantity => $"{Quantity}";
        public string sBudget => string.Format(new CultureInfo("en-US"), "{0:C0}", Budget);
    }
}
