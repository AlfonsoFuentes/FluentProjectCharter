using Shared.Models.FileResults.Generics.Request;

namespace Shared.Models.StakeHolders.Requests
{
    public class CreateStakeHolderRequest : CreateMessageResponse, IRequest
    {
        public Guid ProjectId { get; set; }
        public Guid CaseId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string EndPointName => StaticClass.StakeHolders.EndPoint.Create;

        public override string Legend => Name;

        public override string ClassName => StaticClass.StakeHolders.ClassName;
    }
}
