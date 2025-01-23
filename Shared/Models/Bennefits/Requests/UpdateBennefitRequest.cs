using Shared.Models.FileResults.Generics.Request;

namespace Shared.Models.Bennefits.Requests
{
    public class UpdateBennefitRequest : UpdateMessageResponse, IRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Guid ScopeId { get; set; }
        public string EndPointName => StaticClass.Bennefits.EndPoint.Update;
        public Guid ProjectId { get; set; }
        public override string Legend => Name;

        public override string ClassName => StaticClass.Bennefits.ClassName;
    }
}
