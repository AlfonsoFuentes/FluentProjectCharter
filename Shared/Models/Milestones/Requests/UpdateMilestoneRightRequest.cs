using Shared.Models.FileResults.Generics.Request;

namespace Shared.Models.Milestones.Requests
{
    public class UpdateMilestoneRightRequest : UpdateMessageResponse, IRequest
    {

        public Guid ParentId { get; set; }
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Guid ProjectId { get; set; }
      
        public string EndPointName => StaticClass.Milestones.EndPoint.UpdateRight;

        public override string Legend => Name;

        public override string ClassName => StaticClass.Milestones.ClassName;

    }
}
