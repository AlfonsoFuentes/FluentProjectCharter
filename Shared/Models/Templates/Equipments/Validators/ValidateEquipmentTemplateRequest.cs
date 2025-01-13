using Shared.Enums.Materials;
using Shared.Models.Brands.Responses;
using Shared.Models.FileResults.Generics.Request;

namespace Shared.Models.Templates.Equipments.Validators
{
    public class ValidateEquipmentTemplateRequest : ValidateMessageResponse, IRequest
    {
        public Guid? Id { get; set; }
        public string Name { get; set; } = string.Empty;


        public string EndPointName => StaticClass.EquipmentTemplates.EndPoint.Validate;

        public override string Legend => Name;

        public override string ClassName => StaticClass.EquipmentTemplates.ClassName;

        public string Model { get; set; } = string.Empty;
        public string InternalMaterial { get; set; } = string.Empty;
        public string ExternalMaterial { get; set; } = string.Empty;

        public string Type { get; set; } = string.Empty;
        public string SubType { get; set; } = string.Empty;
        public string TagLetter { get; set; } = string.Empty;
        public string Brand { get; set; } = string.Empty;
        public string Reference { get; set; } = string.Empty;
    }

}
