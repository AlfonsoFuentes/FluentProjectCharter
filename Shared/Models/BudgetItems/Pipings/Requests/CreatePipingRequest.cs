using Shared.Enums.CostCenter;
using Shared.Models.FileResults.Generics.Request;

namespace Shared.Models.BudgetItems.Pipings.Requests
{
    public class CreatePipingRequest : CreateMessageResponse, IRequest
    {
        public Guid DeliverableId { get; set; }
        public string Name { get; set; } = string.Empty;
        public Guid ProjectId { get; set; }
        public string EndPointName => StaticClass.Pipings.EndPoint.Create;

        public override string Legend => Name;

        public override string ClassName => StaticClass.Pipings.ClassName;

        public CostCenterEnum CostCenter { get; set; } = CostCenterEnum.None;

        public double Budget => MaterialUnitaryCost * MaterialQuantity + LaborUnitaryCost * LaborQuantity;


        public string sBudget => string.Format(new CultureInfo("en-US"), "{0:C0}", Budget);
        public double MaterialUnitaryCost { get; set; }
        public double MaterialQuantity { get; set; }
        public double LaborUnitaryCost { get; set; }
        public double LaborQuantity { get; set; }
        public string Diameter { get; set; } = string.Empty;
        public string Material { get; set; } = string.Empty;
        public bool Insulation { get; set; }
        public string FluidCodeName { get; set; } = string.Empty;
        public string TagNumber { get; set; } = string.Empty;
    }
}
