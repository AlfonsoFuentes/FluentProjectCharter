using Shared.Models.FileResults.Generics.Request;

namespace Shared.Models.Scopes.Requests
{
    public class UpdateScopeRequest : UpdateMessageResponse, IRequest
    {

        public Guid CaseId { get; set; }
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Guid ProjectId { get; set; }
        public string EndPointName => StaticClass.Scopes.EndPoint.Update;

        public override string Legend => Name;

        public override string ClassName => StaticClass.Scopes.ClassName;
    }
}
