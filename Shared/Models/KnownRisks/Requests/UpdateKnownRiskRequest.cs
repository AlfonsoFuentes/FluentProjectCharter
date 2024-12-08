using Shared.Models.FileResults.Generics.Request;

namespace Shared.Models.KnownRisks.Requests
{
    public class UpdateKnownRiskRequest : UpdateMessageResponse, IRequest
    {
        public Guid CaseId { get; set; }
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public string EndPointName => StaticClass.KnownRisks.EndPoint.Update;
        public Guid ProjectId { get; set; }
        public override string Legend => Name;

        public override string ClassName => StaticClass.KnownRisks.ClassName;
    }
}
