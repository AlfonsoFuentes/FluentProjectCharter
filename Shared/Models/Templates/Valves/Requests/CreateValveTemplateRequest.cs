using Shared.Enums.DiameterEnum;
using Shared.Enums.Materials;
using Shared.Enums.ValvesEnum;
using Shared.Models.Brands.Responses;
using Shared.Models.FileResults.Generics.Request;
using Shared.Models.Templates.NozzleTemplates;

namespace Shared.Models.Templates.Valves.Requests
{
    public class CreateValveTemplateRequest : CreateMessageResponse, IRequest
    {
        public string Name { get; set; } = string.Empty;
        public string EndPointName => StaticClass.ValveTemplates.EndPoint.Create;

        public override string Legend => Name;

        public override string ClassName => StaticClass.ValveTemplates.ClassName;
        public string Model { get; set; } = string.Empty;
        public ValveTypesEnum Type { get; set; } = ValveTypesEnum.None;
        public MaterialEnum Material { get; set; } = MaterialEnum.None;
        public ActuatorTypeEnum ActuatorType { get; set; } = ActuatorTypeEnum.None;
        public PositionerTypeEnum PositionerType { get; set; } = PositionerTypeEnum.None;
        public NominalDiameterEnum Diameter { get; set; } = NominalDiameterEnum.None;
        public FailTypeEnum FailType { get; set; } = FailTypeEnum.None;
        public SignalTypeEnum SignalType { get; set; } = SignalTypeEnum.None;
        public bool HasFeedBack { get; set; }
        public BrandResponse? BrandResponse { get; set; }
        public string Brand => BrandResponse?.Name ?? string.Empty;
        public List<NozzleTemplateResponse> Nozzles { get; set; } = new();
        public double Value { get; set; }
        public string sValue => string.Format(new CultureInfo("en-US"), "{0:C0}", Value);

        public string TagLetter => $"{PositionerType.Letter}{Type.Letter}";
        
    }
}
