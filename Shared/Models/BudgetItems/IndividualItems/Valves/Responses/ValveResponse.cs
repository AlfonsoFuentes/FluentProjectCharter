using Shared.Enums.BudgetItemTypes;
using Shared.Enums.CostCenter;
using Shared.Enums.DiameterEnum;
using Shared.Enums.Materials;
using Shared.Enums.ValvesEnum;
using Shared.Models.Brands.Responses;
using Shared.Models.BudgetItems.IndividualItems.Nozzles.Responses;
using Shared.Models.BudgetItems.Responses;
using System.Globalization;

namespace Shared.Models.BudgetItems.IndividualItems.Valves.Responses
{
    public class ValveResponse : BaseResponse, IBudgetItemResponse
    {
        public bool Selected { get; set; }
        public Guid DeliverableId { get; set; }
        public Guid ProjectId { get; set; }

        public double Budget { get; set; }

        public string sBudget => string.Format(new CultureInfo("en-US"), "{0:C0}", Budget);

        public string Nomenclatore { get; set; } = string.Empty;

        public string UpadtePageName { get; set; } = StaticClass.Valves.PageName.Update;

        public string Model { get; set; } = string.Empty;
        public MaterialEnum Material { get; set; } = MaterialEnum.None;
        public ActuatorTypeEnum ActuatorType { get; set; } = ActuatorTypeEnum.None;
        public PositionerTypeEnum PositionerType { get; set; } = PositionerTypeEnum.None;
        public bool HasFeedBack { get; set; }
        public NominalDiameterEnum Diameter { get; set; } = NominalDiameterEnum.None;
        public FailTypeEnum FailType { get; set; } = FailTypeEnum.None;
        public SignalTypeEnum SignalType { get; set; } = SignalTypeEnum.None;

        public ValveTypesEnum Type { get; set; } = ValveTypesEnum.None;

        public string TagNumber { get; set; } = string.Empty;
        public string Tag => ShowProvisionalTag ? ProvisionalTag : !string.IsNullOrEmpty(TagLetter)
               && !string.IsNullOrEmpty(TagNumber) ? $"{TagLetter}-{TagNumber}" : string.Empty;



        public string TagLetter { get; set; } = string.Empty;
        public BrandResponse BrandResponse { get; set; } = new();
        public string Brand => BrandResponse == null ? string.Empty : BrandResponse.Name;
        public List<NozzleResponse> Nozzles { get; set; } = new();
        public bool ShowDetails { get; set; } = false;
        public bool IsExisting { get; set; }
        public string ProvisionalTag {  get; set; } = string.Empty;
        public bool ShowProvisionalTag { get; set; } = false;
    }
}
