using Shared.Enums.BudgetItemTypes;
using Shared.Enums.CostCenter;
using Shared.Models.BudgetItems;
using System.Globalization;

namespace Shared.Models.BudgetItems.Paintings.Responses
{
    public class PaintingResponse : BaseResponse, IBudgetItemResponse
    {

        public Guid DeliverableId { get; set; }
        public Guid ProjectId { get; set; }

        public CostCenterEnum CostCenter { get; set; } = CostCenterEnum.None;
        public double UnitaryCost { get; set; }
        public double Quantity { get; set; }
        public double Budget => UnitaryCost * Quantity;

        public string sUnitaryCost => string.Format(new CultureInfo("en-US"), "{0:C0}", UnitaryCost);
        public string sQuantity => $"{Quantity}";
        public string sBudget => string.Format(new CultureInfo("en-US"), "{0:C0}", Budget);

        public string Nomenclatore { get; set; } = string.Empty;

        public string UpadtePageName { get; set; } = StaticClass.Paintings.PageName.Update;
        public string Tag { get; set; } = string.Empty;
    }
}
