using Shared.Enums.ConnectionTypes;
using Shared.Enums.DiameterEnum;
using Shared.Enums.Instruments;
using Shared.Enums.Materials;
using Shared.Enums.ValvesEnum;
using Shared.Models.Brands.Responses;
using Shared.Models.FileResults.Generics.Request;
using Shared.Models.Templates.NozzleTemplates;

namespace Shared.Models.Templates.Instruments.Responses
{
    public class InstrumentTemplateResponse : BaseResponse, IMessageResponse, IRequest
    {
        
        public double Value { get; set; }
        public string sValue => string.Format(new CultureInfo("en-US"), "{0:C0}", Value);
        public SignalTypeEnum SignalType { get; set; } = SignalTypeEnum.None;
    
        public ConnectionTypeEnum ConnectionType { get; set; } = ConnectionTypeEnum.None;
        public string Model { get; set; } = string.Empty;
        public MaterialEnum Material { get; set; } = MaterialEnum.None;

        public string Reference { get; set; } = string.Empty;
  
        public VariableInstrumentEnum VariableInstrument { get; set; } = VariableInstrumentEnum.None;
        public ModifierVariableInstrumentEnum ModifierVariable { get; set; } = ModifierVariableInstrumentEnum.None;
        public BrandResponse? Brand { get; set; }
        public string BrandName => Brand == null ? string.Empty : Brand.Name;
        public List<NozzleTemplateResponse> Nozzles { get; set; } = new();
        public string TagLetter => $"{VariableInstrument.Letter}{ModifierVariable.Letter}";

        public string Legend => $"{VariableInstrument} {Model}";
        public string EndPointName => Id == Guid.Empty ? StaticClass.InstrumentTemplates.EndPoint.Create : StaticClass.InstrumentTemplates.EndPoint.Update;
        public string ActionType => Id == Guid.Empty ? "created" : "updated";
        public string ClassName => StaticClass.InstrumentTemplates.ClassName;
        public string Succesfully => StaticClass.ResponseMessages.ReponseSuccesfullyMessage(Legend, ClassName, ActionType);
        public string Fail => StaticClass.ResponseMessages.ReponseFailMessage(Legend, ClassName, ActionType);
        public string NotFound => StaticClass.ResponseMessages.ReponseNotFound(ClassName);

    }
}
