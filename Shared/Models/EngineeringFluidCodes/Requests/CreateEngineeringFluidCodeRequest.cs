using Shared.Models.FileResults.Generics.Request;

namespace Shared.Models.OrganizationStrategies.Requests
{
    public class CreateEngineeringFluidCodeRequest : CreateMessageResponse, IRequest
    {
        public string Name { get; set; } = string.Empty;
        public string EndPointName => StaticClass.EngineeringFluidCodes.EndPoint.Create;

        public override string Legend => Name;

        public override string ClassName => StaticClass.EngineeringFluidCodes.ClassName;
        public string Code { get; set; } = string.Empty;
    }
}
