using Shared.Enums.CostCenter;

namespace Shared.Models.BudgetItems.Pipings.Responses
{
    public class PipingResponse : BaseResponse, IBudgetItemResponse
    {

        public Guid DeliverableId { get; set; }
        public Guid ProjectId { get; set; }

        public CostCenterEnum CostCenter { get; set; } = CostCenterEnum.None;
      
        public double Budget => MaterialUnitaryCost * MaterialQuantity + LaborUnitaryCost * LaborQuantity;


        public string sBudget => string.Format(new CultureInfo("en-US"), "{0:C0}", Budget);

        public string Nomenclatore { get; set; } = string.Empty;
      
        public string UpadtePageName { get; set; } = StaticClass.Pipings.PageName.Update;
        public string Tag { get; set; } = string.Empty;
        public string TagNumber { get; set; } = string.Empty;
        public double MaterialUnitaryCost { get; set; }
        public double MaterialQuantity { get; set; }
        public double LaborUnitaryCost { get; set; }
        public double LaborQuantity { get; set; }
        public string Diameter { get; set; } = string.Empty;
        public string Material { get; set; } = string.Empty;
        public bool Insulation { get; set; }
        public string FluidCodeName { get; set; } = string.Empty;
    }
}
