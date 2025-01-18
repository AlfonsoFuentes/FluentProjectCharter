using Shared.Enums.Materials;
using Shared.Enums.NozzleTypes;
using Shared.Models.Brands.Responses;
using Shared.Models.FileResults.Generics.Request;
using Shared.Models.Templates.NozzleTemplates;

namespace Shared.Models.Templates.Instruments.Requests
{
    public class UpdateInstrumentTemplateRequest : UpdateMessageResponse, IRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public string EndPointName => StaticClass.InstrumentTemplates.EndPoint.Update;

        public override string Legend => Name;

        public override string ClassName => StaticClass.InstrumentTemplates.ClassName;


        public double Value { get; set; }
        public string sValue => string.Format(new CultureInfo("en-US"), "{0:C0}", Value);
        public SignalTypeEnum SignalType { get; set; } = SignalTypeEnum.None;

        public string Model { get; set; } = string.Empty;
        public MaterialEnum Material { get; set; } = MaterialEnum.None;
        public string TagLetter => $"{Type.Letter}{SubType.Letter}";
        public string Reference { get; set; } = string.Empty;

        public VariableInstrumentEnum Type { get; set; } = VariableInstrumentEnum.None;
        public ModifierVariableInstrumentEnum SubType { get; set; } = ModifierVariableInstrumentEnum.None;
        public BrandResponse? BrandResponse { get; set; }
        public string Brand => BrandResponse == null ? string.Empty : BrandResponse.Name;
        public List<NozzleTemplateResponse> Nozzles { get; set; } = new();
    }
}
