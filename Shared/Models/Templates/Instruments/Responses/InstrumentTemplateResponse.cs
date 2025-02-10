using Shared.Enums.Instruments;
using Shared.Enums.Materials;
using Shared.Enums.ValvesEnum;
using Shared.Models.Brands.Responses;
using Shared.Models.Templates.NozzleTemplates;

namespace Shared.Models.Templates.Instruments.Responses
{
    public class InstrumentTemplateResponse : BaseResponse
    {
        
        public double Value { get; set; }
        public string sValue => string.Format(new CultureInfo("en-US"), "{0:C0}", Value);
        public SignalTypeEnum SignalType { get; set; } = SignalTypeEnum.None;

        public string Model { get; set; } = string.Empty;
        public MaterialEnum Material { get; set; } = MaterialEnum.None;

        public string Reference { get; set; } = string.Empty;
  
        public VariableInstrumentEnum Type { get; set; } = VariableInstrumentEnum.None;
        public ModifierVariableInstrumentEnum SubType { get; set; } = ModifierVariableInstrumentEnum.None;
        public BrandResponse BrandResponse { get; set; } = new();
        public string Brand => BrandResponse == null ? string.Empty : BrandResponse.Name;
        public List<NozzleTemplateResponse> Nozzles { get; set; } = new();
        public string TagLetter => $"{Type.Letter}{SubType.Letter}";

      

    }
}
