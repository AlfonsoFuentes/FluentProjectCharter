using Shared.Models.FileResults.Generics.Request;

namespace Shared.Models.OrganizationStrategies.Requests
{
    public class CreateOrganizationStrategyRequest : CreateMessageResponse, IRequest
    {
        public string Name { get; set; } = string.Empty;
        public string EndPointName => StaticClass.OrganizationStrategys.EndPoint.Create;

        public override string Legend => Name;

        public override string ClassName => StaticClass.OrganizationStrategys.ClassName;
    }
}
