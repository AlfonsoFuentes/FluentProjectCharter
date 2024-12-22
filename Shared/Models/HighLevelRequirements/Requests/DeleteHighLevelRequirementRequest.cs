using Shared.Models.FileResults.Generics.Request;

namespace Shared.Models.HighLevelRequirements.Requests
{
    public class DeleteHighLevelRequirementRequest : DeleteMessageResponse, IRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public override string Legend => Name;
        public Guid ProjectId { get; set; }
        public override string ClassName => StaticClass.HighLevelRequirements.ClassName;



        public string EndPointName => StaticClass.HighLevelRequirements.EndPoint.Delete;
    }
}
