using Shared.Models.FileResults.Generics.Request;

namespace Shared.Models.AppStates.Validators
{
    public class ValidateAppStateRequest : ValidateMessageResponse, IRequest
    {
        public Guid? Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Guid ProjectId { get; set; }

        public string EndPointName => StaticClass.AppStates.EndPoint.Validate;

        public override string Legend => Name;

        public override string ClassName => StaticClass.AppStates.ClassName;
    }
    
}
