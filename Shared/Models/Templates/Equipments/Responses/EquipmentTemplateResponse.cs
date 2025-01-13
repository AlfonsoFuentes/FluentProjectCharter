using Shared.Enums.Materials;
using Shared.Models.Brands.Responses;
using Shared.Models.Templates.NozzleTemplates;

namespace Shared.Models.Templates.Equipments.Responses
{
    public class EquipmentTemplateResponse : BaseResponse
    {
        public string Reference { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public MaterialEnum InternalMaterial { get; set; } = MaterialEnum.None;
        public MaterialEnum ExternalMaterial { get; set; } = MaterialEnum.None;
        public double Value { get; set; }
        public string sValue => string.Format(new CultureInfo("en-US"), "{0:C0}", Value);
        public string Type { get; set; } = string.Empty;
        public string SubType { get; set; } = string.Empty;
        public string TagLetter { get; set; } = string.Empty;
        public BrandResponse BrandResponse { get; set; } = new();
        public string Brand => BrandResponse.Name;
        public List<NozzleTemplateResponse> Nozzles { get; set; } = new();

    }
}
