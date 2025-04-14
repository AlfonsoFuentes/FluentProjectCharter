using Shared.Enums.ConnectionTypes;
using Shared.Enums.DiameterEnum;
using Shared.Enums.Instruments;
using Shared.Enums.Materials;
using Shared.Enums.ValvesEnum;
using Shared.Models.Brands.Responses;
using Shared.Models.BudgetItems.IndividualItems.Nozzles.Responses;
using Shared.Models.BudgetItems.Responses;
using Shared.Models.FileResults.Generics.Request;

namespace Shared.Models.BudgetItems.IndividualItems.Instruments.Responses
{
    public class InstrumentResponse : BudgetItemWithPurchaseOrdersResponse, IMessageResponse, IRequest
    {


        public string EndPointName => StaticClass.Instruments.EndPoint.CreateUpdate;

        public string Legend => Name;

        public string ActionType => Id == Guid.Empty ? "created" : "updated";
        public string ClassName => StaticClass.Instruments.ClassName;
        public string Succesfully => StaticClass.ResponseMessages.ReponseSuccesfullyMessage(Legend, ClassName, ActionType);
        public string Fail => StaticClass.ResponseMessages.ReponseFailMessage(Legend, ClassName, ActionType);
        public string NotFound => StaticClass.ResponseMessages.ReponseNotFound(ClassName);

        public Guid? GanttTaskId { get; set; }

        public Guid? InstrumentTemplatedId { get; set; }


        public string TagNumber { get; set; } = string.Empty;
        public SignalTypeEnum SignalType { get; set; } = SignalTypeEnum.None;

        public ConnectionTypeEnum ConnectionType { get; set; } = ConnectionTypeEnum.None;
        public string Model { get; set; } = string.Empty;
        public MaterialEnum Material { get; set; } = MaterialEnum.None;
        public string Reference { get; set; } = string.Empty;
        public VariableInstrumentEnum VariableInstrument { get; set; } = VariableInstrumentEnum.None;
        public ModifierVariableInstrumentEnum ModifierVariable { get; set; } = ModifierVariableInstrumentEnum.None;
        public BrandResponse? Brand { get; set; }
        public string BrandName => Brand == null ? string.Empty : Brand.Name;
        public Guid? BrandId => Brand == null ? null : Brand.Id;
        public List<NozzleResponse> Nozzles { get; set; } = new();
        public string TagLetter => $"{VariableInstrument.Letter}{ModifierVariable.Letter}";
        string ShowTagNumber => string.IsNullOrWhiteSpace(TagNumber) ? string.Empty : $"-{TagNumber}";
        public override string Tag => ShowProvisionalTag ? ProvisionalTag : $"{TagLetter}{ShowTagNumber}";

        public bool IsExisting { get; set; }
        public string ProvisionalTag { get; set; } = string.Empty;
        public bool ShowProvisionalTag { get; set; } = false;


    }
}
