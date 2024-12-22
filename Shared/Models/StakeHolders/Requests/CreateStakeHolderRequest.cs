using Shared.Models.FileResults.Generics.Request;

namespace Shared.Models.StakeHolders.Requests
{
    public class CreateStakeHolderRequest : CreateMessageResponse, IRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
     
        public string EndPointName => StaticClass.StakeHolders.EndPoint.Create;

        public override string Legend => Name;
        public string Area {  get; set; } = string.Empty;
        public override string ClassName => StaticClass.StakeHolders.ClassName;
    }

}
