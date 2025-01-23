using Shared.Models.FileResults.Generics.Request;

namespace Shared.Models.WBSs.Requests
{
    public class DeleteWBSRequest : DeleteMessageResponse, IRequest
    {
        public string Name { get; set; } = string.Empty;
        public override string Legend => Name;
        public Guid ProjectId { get; set; }
        public override string ClassName => StaticClass.WBSs.ClassName;

        public Guid Id { get; set; }

        public string EndPointName => StaticClass.WBSs.EndPoint.Delete;
    }
}
