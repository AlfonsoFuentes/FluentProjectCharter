using Shared.Models.FileResults.Generics.Request;

namespace Shared.Models.HighLevelRequirements.Validators
{
    public class ValidateHighLevelRequirementRequest : ValidateMessageResponse, IRequest
    {
        public Guid? Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Guid ProjectId { get; set; }

        public string EndPointName => StaticClass.HighLevelRequirements.EndPoint.Validate;

        public override string Legend => Name;

        public override string ClassName => StaticClass.HighLevelRequirements.ClassName;
    }
    
}
