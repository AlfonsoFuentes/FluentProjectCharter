using Shared.Models.FileResults.Generics.Request;

namespace Shared.Models.Constrainsts.Requests
{
    public class CreateConstrainstRequest : CreateMessageResponse, IRequest
    {
        public Guid? ScopeId { get; set; }
        public string Name { get; set; } = string.Empty;
        public Guid ProjectId { get; set; }
        public string EndPointName => StaticClass.Constrainsts.EndPoint.Create;

        public override string Legend => Name;

        public override string ClassName => StaticClass.Constrainsts.ClassName;
    }
}
