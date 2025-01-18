using Shared.Enums.Materials;
using Shared.Enums.NozzleTypes;
using Shared.Models.Brands.Responses;
using Shared.Models.FileResults.Generics.Request;
using Shared.Models.Templates.NozzleTemplates;

namespace Shared.Models.Templates.Instruments.Validators
{
    public class ValidateInstrumentTemplateRequest : ValidateMessageResponse, IRequest
    {
        public Guid? Id { get; set; }
        public string Name { get; set; } = string.Empty;


        public string EndPointName => StaticClass.InstrumentTemplates.EndPoint.Validate;

        public override string Legend => Name;

        public override string ClassName => StaticClass.InstrumentTemplates.ClassName;

    
        public string SignalType { get; set; } = string.Empty;

        public string Model { get; set; } = string.Empty;
        public string Material { get; set; } = string.Empty;

        public string Reference { get; set; } = string.Empty;

        public string Type { get; set; } = string.Empty;
        public string SubType { get; set; } = string.Empty;
        public string TagLetter { get; set; } = string.Empty;
        public string Brand { get; set; } = string.Empty;
      
    }

}
