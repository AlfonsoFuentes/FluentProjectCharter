using Shared.Models.FileResults.Generics.Request;

namespace Shared.Models.Milestones.Validators
{
   
    public class ValidateMilestoneRequest : ValidateMessageResponse, IRequest
    {
        public Guid? Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Guid ProjectId { get; set; }

        public string EndPointName => StaticClass.Milestones.EndPoint.Validate;

        public override string Legend => Name;

        public override string ClassName => StaticClass.Milestones.ClassName;
    }
}
