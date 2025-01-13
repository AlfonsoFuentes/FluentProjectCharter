using Shared.Models.FileResults.Generics.Request;

namespace Shared.Models.AcceptanceCriterias.Requests
{
    public class UpdateAcceptanceCriteriaRequest : UpdateMessageResponse, IRequest
    {

        public Guid DeliverableId { get; set; }
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Guid ProjectId { get; set; }
        public string EndPointName => StaticClass.AcceptanceCriterias.EndPoint.Update;

        public override string Legend => Name;

        public override string ClassName => StaticClass.AcceptanceCriterias.ClassName;
    }
}
