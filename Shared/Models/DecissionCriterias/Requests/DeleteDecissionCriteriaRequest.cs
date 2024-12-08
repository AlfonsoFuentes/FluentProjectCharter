using Shared.Models.FileResults.Generics.Request;

namespace Shared.Models.DecissionCriterias.Requests
{
    public class DeleteDecissionCriteriaRequest : DeleteMessageResponse, IRequest
    {
        public string Name { get; set; } = string.Empty;
        public override string Legend => Name;
        public Guid ProjectId { get; set; }
        public override string ClassName => StaticClass.DecissionCriterias.ClassName;

        public Guid Id { get; set; }

        public string EndPointName => StaticClass.DecissionCriterias.EndPoint.Delete;
    }
}
