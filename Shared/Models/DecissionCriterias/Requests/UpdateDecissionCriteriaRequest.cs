using Shared.Models.FileResults.Generics.Request;

namespace Shared.Models.DecissionCriterias.Requests
{
    public class UpdateDecissionCriteriaRequest : UpdateMessageResponse, IRequest
    {
        public Guid CaseId { get; set; }
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Guid ProjectId { get; set; }
        public string EndPointName => StaticClass.DecissionCriterias.EndPoint.Update;

        public override string Legend => Name;

        public override string ClassName => StaticClass.DecissionCriterias.ClassName;
    }
}
