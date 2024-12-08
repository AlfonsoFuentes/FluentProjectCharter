using Shared.Commons;
using Shared.Models.FileResults.Generics.Request;
using Shared.Models.Projects.Reponses;
using Shared.StaticClasses;

namespace Shared.Models.Projects.Request
{
    public class CreateProjectRequest : CreateMessageResponse, IRequest
    {
        public string Name { get; set; } = string.Empty;

        public string EndPointName => StaticClass.Projects.EndPoint.Create;

        public override string Legend => Name;

        public override string ClassName => StaticClass.Projects.ClassName;
    }
}
