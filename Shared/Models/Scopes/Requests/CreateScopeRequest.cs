using Shared.Models.FileResults.Generics.Request;

namespace Shared.Models.Scopes.Requests
{
    public class CreateScopeRequest : CreateMessageResponse, IRequest
    {
        
        
     
        public string Name { get; set; } = string.Empty;
        public string EndPointName => StaticClass.Scopes.EndPoint.Create;
        public Guid ProjectId { get; set; }
        public override string Legend => Name;

        public override string ClassName => StaticClass.Scopes.ClassName;
    }
}
