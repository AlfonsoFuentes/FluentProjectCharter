using Shared.Models.FileResults.Generics.Request;

namespace Shared.Models.SucessfullFactors.Requests
{
    public class CreateSucessfullFactorRequest : CreateMessageResponse, IRequest
    {
        public Guid CaseId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string EndPointName => StaticClass.SucessfullFactors.EndPoint.Create;
        public Guid ProjectId { get; set; }
        public override string Legend => Name;

        public override string ClassName => StaticClass.SucessfullFactors.ClassName;
    }
}
