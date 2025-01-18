﻿using Shared.Enums.Materials;
using Shared.Enums.NozzleTypes;
using Shared.Models.Brands.Responses;
using Shared.Models.BudgetItems.Nozzles.Responses;

namespace Shared.Models.BudgetItems.Instruments.Responses
{
    public class InstrumentResponse : BaseResponse, IBudgetItemResponse
    {

        public Guid DeliverableId { get; set; }
        public Guid ProjectId { get; set; }


        public double Budget { get; set; }


        public string sBudget => string.Format(new CultureInfo("en-US"), "{0:C0}", Budget);

        public string Nomenclatore { get; set; } = string.Empty;

        public string UpadtePageName { get; set; } = StaticClass.Instruments.PageName.Update;

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
        public string Tag => $"{TagLetter}-{TagNumber}";
        public bool ShowDetails { get; set; }
    }
}
