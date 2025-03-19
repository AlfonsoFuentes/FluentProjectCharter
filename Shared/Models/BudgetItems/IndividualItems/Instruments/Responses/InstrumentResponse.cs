using Shared.Enums.Instruments;
using Shared.Enums.Materials;
using Shared.Enums.ValvesEnum;
using Shared.Models.Brands.Responses;
using Shared.Models.BudgetItems.IndividualItems.Nozzles.Responses;
using Shared.Models.BudgetItems.Responses;

namespace Shared.Models.BudgetItems.IndividualItems.Instruments.Responses
{
    public class InstrumentResponse : BudgetItemWithPurchaseOrdersResponse
    {

        public Guid DeliverableId { get; set; }



        public override string UpadtePageName { get; set; } = StaticClass.Instruments.PageName.Update;

        public string TagNumber { get; set; } = string.Empty;
        public SignalTypeEnum SignalType { get; set; } = SignalTypeEnum.None;

        public string Model { get; set; } = string.Empty;
        public MaterialEnum Material { get; set; } = MaterialEnum.None;

        public string Reference { get; set; } = string.Empty;

        public VariableInstrumentEnum Type { get; set; } = VariableInstrumentEnum.None;
        public ModifierVariableInstrumentEnum SubType { get; set; } = ModifierVariableInstrumentEnum.None;
        public BrandResponse BrandResponse { get; set; } = new();
        public string Brand => BrandResponse == null ? string.Empty : BrandResponse.Name;
        public List<NozzleResponse> Nozzles { get; set; } = new();
        public string TagLetter { get; set; } = string.Empty;
        public override string Tag => ShowProvisionalTag ? ProvisionalTag : !string.IsNullOrEmpty(TagLetter)
                  && !string.IsNullOrEmpty(TagNumber) ? $"{TagLetter}-{TagNumber}" : string.Empty;

        public bool ShowDetails { get; set; }
        public bool IsExisting { get; set; }
        public string ProvisionalTag { get; set; } = string.Empty;
        public bool ShowProvisionalTag { get; set; } = false;

      
    }
}
