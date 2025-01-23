using Shared.Models.FileResults.Generics.Request;

namespace Shared.Models.WBSs.Requests
{
    public class CreateWBSRequest : CreateMessageResponse, IRequest
    {
        public Guid CaseId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string EndPointName => StaticClass.WBSs.EndPoint.Create;
        public Guid ProjectId { get; set; }
        public override string Legend => Name;

        public override string ClassName => StaticClass.WBSs.ClassName;
    }
}
