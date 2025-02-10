using Shared.Models.FileResults.Generics.Request;
using System.ComponentModel.DataAnnotations;

namespace Shared.Models.Milestones.Requests
{
    public class CreateMilestoneRequest : CreateMessageResponse, IRequest
    {
        public Guid? StartId { get; set; }
        public Guid? PlanningId { get; set; }
        
        public string Name { get; set; } = string.Empty;
        public string EndPointName => StaticClass.Milestones.EndPoint.Create;
        public Guid ProjectId { get; set; }
        public override string Legend => Name;

        public override string ClassName => StaticClass.Milestones.ClassName;
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public string Duration { get; set; } = string.Empty;

    
      


    }
}
