using Shared.Models.FileResults.Generics.Request;

namespace Shared.Models.Bennefits.Requests
{
    public class CreateBennefitRequest : CreateMessageResponse, IRequest
    {
      
        public Guid ScopeId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string EndPointName => StaticClass.Bennefits.EndPoint.Create;
        public Guid ProjectId { get; set; }
        public override string Legend => Name;

        public override string ClassName => StaticClass.Bennefits.ClassName;
    }
}
