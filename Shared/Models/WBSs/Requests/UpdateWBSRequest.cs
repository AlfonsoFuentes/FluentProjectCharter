using Shared.Models.FileResults.Generics.Request;

namespace Shared.Models.WBSs.Requests
{
    public class UpdateWBSRequest : UpdateMessageResponse, IRequest
    {
        public Guid CaseId { get; set; }

        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Guid ProjectId { get; set; }
        public string EndPointName => StaticClass.WBSs.EndPoint.Update;

        public override string Legend => Name;

        public override string ClassName => StaticClass.WBSs.ClassName;
    }
}
