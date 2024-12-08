using Shared.Models.FileResults.Generics.Request;

namespace Shared.Models.SucessfullFactors.Requests
{
    public class DeleteSucessfullFactorRequest : DeleteMessageResponse, IRequest
    {
        public string Name { get; set; } = string.Empty;
        public override string Legend => Name;
        public Guid ProjectId { get; set; }
        public override string ClassName => StaticClass.SucessfullFactors.ClassName;

        public Guid Id { get; set; }

        public string EndPointName => StaticClass.SucessfullFactors.EndPoint.Delete;
    }
}
