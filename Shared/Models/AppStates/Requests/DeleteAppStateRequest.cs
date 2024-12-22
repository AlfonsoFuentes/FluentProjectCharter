using Shared.Models.FileResults.Generics.Request;

namespace Shared.Models.AppStates.Requests
{
    public class DeleteAppStateRequest : DeleteMessageResponse, IRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public override string Legend => Name;
        public Guid ProjectId { get; set; }
        public override string ClassName => StaticClass.AppStates.ClassName;



        public string EndPointName => StaticClass.AppStates.EndPoint.Delete;
    }
}
