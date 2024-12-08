using Shared.Models.FileResults.Generics.Request;

namespace Shared.Models.DecissionCriterias.Requests
{
    public class CreateDecissionCriteriaRequest : CreateMessageResponse, IRequest
    {
        public Guid CaseId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string EndPointName => StaticClass.DecissionCriterias.EndPoint.Create;
        public Guid ProjectId { get; set; }
        public override string Legend => Name;

        public override string ClassName => StaticClass.DecissionCriterias.ClassName;
    }
}
