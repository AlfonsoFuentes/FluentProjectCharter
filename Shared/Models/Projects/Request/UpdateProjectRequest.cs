using Shared.Models.FileResults.Generics.Request;
using Shared.StaticClasses;

namespace Shared.Models.Projects.Request
{
    public class UpdateProjectRequest : UpdateMessageResponse, IRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public string EndPointName => StaticClass.Projects.EndPoint.Update;

        public override string Legend => Name;

        public override string ClassName => StaticClass.Projects.ClassName;
    }
}
