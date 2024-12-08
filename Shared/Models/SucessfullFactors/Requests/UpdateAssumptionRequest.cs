using Shared.Models.FileResults.Generics.Request;

namespace Shared.Models.SucessfullFactors.Requests
{
    public class UpdateSucessfullFactorRequest : UpdateMessageResponse, IRequest
    {
        public Guid CaseId { get; set; }

        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Guid ProjectId { get; set; }
        public string EndPointName => StaticClass.SucessfullFactors.EndPoint.Update;

        public override string Legend => Name;

        public override string ClassName => StaticClass.SucessfullFactors.ClassName;
    }
}
