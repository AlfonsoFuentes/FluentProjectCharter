using Shared.Models.FileResults.Generics.Request;

namespace Shared.Models.Milestones.Requests
{
    public class DeleteMilestoneRequest : DeleteMessageResponse, IRequest
    {
        public string Name { get; set; } = string.Empty;
        public override string Legend => Name;

        public override string ClassName => StaticClass.Milestones.ClassName;

        public Guid ProjectId { get; set; }
        public Guid Id { get; set; }

        public string EndPointName => StaticClass.Milestones.EndPoint.Delete;
    }
}
