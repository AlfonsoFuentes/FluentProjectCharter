using Shared.Models.FileResults.Generics.Request;

namespace Shared.Models.OrganizationStrategies.Requests
{
    public class DeleteEngineeringFluidCodeRequest : DeleteMessageResponse, IRequest
    {
        public string Name { get; set; } = string.Empty;
        public override string Legend => Name;

        public override string ClassName => StaticClass.EngineeringFluidCodes.ClassName;

        public Guid Id { get; set; }

        public string EndPointName => StaticClass.EngineeringFluidCodes.EndPoint.Delete;
    }
}
