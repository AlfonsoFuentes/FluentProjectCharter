using Shared.Enums.Materials;
using Shared.Models.Brands.Responses;
using Shared.Models.FileResults.Generics.Request;

namespace Shared.Models.Templates.Valves.Validators
{
    public class ValidateValveTemplateRequest : ValidateMessageResponse, IRequest
    {
        public Guid? Id { get; set; }
        public string Name { get; set; } = string.Empty;


        public string EndPointName => StaticClass.ValveTemplates.EndPoint.Validate;

        public override string Legend => Name;

        public override string ClassName => StaticClass.ValveTemplates.ClassName;

        public string Model { get; set; } = string.Empty;
        public string Brand { get; set; } = string.Empty;

        public string Type { get; set; } = string.Empty;
  
        public string TagLetter { get; set; } = string.Empty;
      
        public string Material { get; set; } = string.Empty;
        public string ActuadorType { get; set; } = string.Empty;
        public string PositionerType { get; set; } = string.Empty;
        public bool HasFeedBack { get; set; }
        public string Diameter { get; set; } = string.Empty;
        public string FailType { get; set; } = string.Empty;
        public string SignalType { get; set; } = string.Empty;

    }

}
