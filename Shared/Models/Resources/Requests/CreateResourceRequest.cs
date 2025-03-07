using Shared.Models.FileResults.Generics.Request;

namespace Shared.Models.Resources.Requests
{
    public class CreateResourceRequest : CreateMessageResponse, IRequest
    {
        
        
     
        public Guid ProjectId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string EndPointName => StaticClass.Resources.EndPoint.Create;
     
        public override string Legend => Name;

        public override string ClassName => StaticClass.Resources.ClassName;
    }
}
