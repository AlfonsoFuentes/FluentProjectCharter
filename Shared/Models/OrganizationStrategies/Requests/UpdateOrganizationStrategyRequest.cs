using Shared.Models.FileResults.Generics.Request;

namespace Shared.Models.OrganizationStrategies.Requests
{
    public class UpdateOrganizationStrategyRequest : UpdateMessageResponse, IRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public string EndPointName => StaticClass.OrganizationStrategys.EndPoint.Update;

        public override string Legend => Name;

        public override string ClassName => StaticClass.OrganizationStrategys.ClassName;
    }
}
