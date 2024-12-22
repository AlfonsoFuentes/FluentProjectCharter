using Shared.Models.FileResults.Generics.Request;

namespace Shared.Models.StakeHolders.Requests
{
    public class UpdateStakeHolderRequest : UpdateMessageResponse, IRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Area { get; set; } = string.Empty;

        public string EndPointName => StaticClass.StakeHolders.EndPoint.Update;

        public override string Legend => Name;

        public override string ClassName => StaticClass.StakeHolders.ClassName;
    }
}
