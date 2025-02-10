using Shared.Models.FileResults.Generics.Request;

namespace Shared.Models.Milestones.Requests
{
    public class ChangeMilestoneOrderUpRequest : UpdateMessageResponse, IRequest
    {
      
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
  
        public string EndPointName => StaticClass.Milestones.EndPoint.UpdateUp;
        public Guid ProjectId { get; set; } 
        public override string Legend => Name;

        public override string ClassName => StaticClass.Milestones.ClassName;
    }
}
