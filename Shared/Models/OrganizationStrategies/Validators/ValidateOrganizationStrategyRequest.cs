using Shared.Models.FileResults.Generics.Request;

namespace Shared.Models.OrganizationStrategies.Validators
{
    public class ValidateOrganizationStrategyRequest : ValidateMessageResponse, IRequest
    {
        public Guid? Id { get; set; }
        public string Name { get; set; } = string.Empty;


        public string EndPointName => StaticClass.OrganizationStrategys.EndPoint.Validate;

        public override string Legend => Name;

        public override string ClassName => StaticClass.OrganizationStrategys.ClassName;
    }

}
