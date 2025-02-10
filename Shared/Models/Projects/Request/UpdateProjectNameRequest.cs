using Shared.Models.FileResults.Generics.Request;

namespace Shared.Models.Projects.Request
{
    public class UpdateProjectNameRequest : UpdateMessageResponse, IRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string EndPointName => StaticClass.Projects.EndPoint.UpdateName;
        public override string Legend => Name;
        public override string ClassName => StaticClass.Projects.ClassName;
    }
}
