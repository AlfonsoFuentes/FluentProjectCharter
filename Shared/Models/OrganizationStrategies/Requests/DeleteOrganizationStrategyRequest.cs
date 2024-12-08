using Shared.Models.FileResults.Generics.Request;

namespace Shared.Models.OrganizationStrategies.Requests
{
    public class DeleteOrganizationStrategyRequest : DeleteMessageResponse, IRequest
    {
        public string Name { get; set; } = string.Empty;
        public override string Legend => Name;

        public override string ClassName => StaticClass.OrganizationStrategys.ClassName;

        public Guid Id { get; set; }

        public string EndPointName => StaticClass.OrganizationStrategys.EndPoint.Delete;
    }
}
