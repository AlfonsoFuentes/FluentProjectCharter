using Shared.Enums.Materials;
using Shared.Models.Brands.Responses;
using Shared.Models.FileResults.Generics.Request;
using Shared.Models.Templates.NozzleTemplates;

namespace Shared.Models.Templates.Equipments.Requests
{
    public class CreateEquipmentTemplateRequest : CreateMessageResponse, IRequest
    {
        public string Name { get; set; } = string.Empty;
        public string EndPointName => StaticClass.EquipmentTemplates.EndPoint.Create;

        public override string Legend => Name;

        public override string ClassName => StaticClass.EquipmentTemplates.ClassName;
        public string Reference { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public MaterialEnum InternalMaterial { get; set; } = MaterialEnum.None;
        public MaterialEnum ExternalMaterial { get; set; } = MaterialEnum.None;
        public double Value { get; set; }
        public string Type { get; set; } = string.Empty;
        public string SubType { get; set; } = string.Empty;
        public string TagLetter { get; set; } = string.Empty;
        public BrandResponse? BrandResponse { get; set; }
        public string Brand => BrandResponse?.Name ?? string.Empty;
        public List<NozzleTemplateResponse> Nozzles { get; set; } = new();
    }
}
