using Shared.Enums.DiameterEnum;
using Shared.Enums.Materials;
using Shared.Models.Brands.Responses;
using Shared.Models.FileResults.Generics.Request;
using Shared.Models.Templates.NozzleTemplates;

namespace Shared.Models.Templates.Pipings.Requests
{
    public class CreatePipeTemplateRequest : CreateMessageResponse, IRequest
    {
        public string Name { get; set; } = string.Empty;
        public string EndPointName => StaticClass.PipeTemplates.EndPoint.Create;

        public override string Legend => Name;

        public override string ClassName => StaticClass.PipeTemplates.ClassName;
        public string sEquivalentLenghPrice => string.Format(new CultureInfo("en-US"), "{0:C0}", EquivalentLenghPrice);
        public string sLaborDayPrice => string.Format(new CultureInfo("en-US"), "{0:C0}", LaborDayPrice);
        public double EquivalentLenghPrice { get; set; }
        public MaterialEnum Material { get; set; } = MaterialEnum.None;
        public double LaborDayPrice { get; set; }
        public NominalDiameterEnum Diameter { get; set; } = NominalDiameterEnum.None;
        public PipeClassEnum Class { get; set; } = PipeClassEnum.None;
        public bool Insulation { get; set; }
        public BrandResponse BrandResponse { get; set; } = new();
        public string Brand => BrandResponse == null ? string.Empty : BrandResponse.Name;
    }
}
