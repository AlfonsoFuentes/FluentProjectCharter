﻿using Shared.Enums.CostCenter;
using Shared.Enums.Materials;
using Shared.Enums.NozzleTypes;
using Shared.Models.Brands.Responses;
using Shared.Models.BudgetItems.Nozzles.Responses;
using Shared.Models.FileResults.Generics.Request;
using Shared.Models.Templates.NozzleTemplates;

namespace Shared.Models.BudgetItems.Instruments.Requests
{
    public class CreateInstrumentRequest : CreateMessageResponse, IRequest
    {
        public Guid DeliverableId { get; set; }
        public string Name { get; set; } = string.Empty;
        public Guid ProjectId { get; set; }
        public string EndPointName => StaticClass.Instruments.EndPoint.Create;

        public override string Legend => Name;

        public override string ClassName => StaticClass.Instruments.ClassName;

  
        public double Budget { get; set; }
        public string sBudget => string.Format(new CultureInfo("en-US"), "{0:C0}", Budget);
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
        public string TagLetter => $"{Type.Letter}{SubType.Letter}";
        public bool ShowDetails {  get; set; }
        public string Tag => $"{TagLetter}-{TagNumber}";
        public string ProvisionalTag { get; set; } = string.Empty;
        public bool ShowProvisionalTag { get; set; } = false;
        public bool IsExisting { get; set; }

    }
}
