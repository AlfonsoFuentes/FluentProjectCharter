﻿using Shared.Enums.CostCenter;
using Shared.Enums.Instruments;
using Shared.Enums.Materials;
using Shared.Enums.ValvesEnum;
using Shared.Models.Brands.Responses;
using Shared.Models.BudgetItems.IndividualItems.Nozzles.Responses;
using Shared.Models.FileResults.Generics.Request;

namespace Shared.Models.BudgetItems.IndividualItems.Instruments.Requests
{
    public class UpdateInstrumentRequest : UpdateMessageResponse, IRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Guid? GanttTaskId { get; set; }
        public Guid ProjectId { get; set; }
        public string EndPointName => StaticClass.Instruments.EndPoint.Update;

        public override string Legend => Name;

        public override string ClassName => StaticClass.Instruments.ClassName;
        public CostCenterEnum CostCenter { get; set; } = CostCenterEnum.None;

        public double BudgetUSD { get; set; }

        public string sBudget => string.Format(new CultureInfo("en-US"), "{0:C0}", BudgetUSD);
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
        public bool ShowDetails { get; set; }
        public string Tag => $"{TagLetter}-{TagNumber}";
        public string ProvisionalTag { get; set; } = string.Empty;
        public bool ShowProvisionalTag { get; set; } = false;
        public bool IsExisting { get; set; }
    }
}
