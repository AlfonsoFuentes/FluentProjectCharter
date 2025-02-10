using Shared.Models.FileResults.Generics.Request;

namespace Shared.Models.Milestones.Requests
{
    public class UpdateMilestoneRequest : UpdateMessageResponse, IRequest
    {
    
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Guid ProjectId { get; set; }
        public Guid DeliverableId { get; set; }
        public string EndPointName => StaticClass.Milestones.EndPoint.Update;

        public override string Legend => Name;

        public override string ClassName => StaticClass.Milestones.ClassName;

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public string Duration { get; set; } = string.Empty;

    }
}
