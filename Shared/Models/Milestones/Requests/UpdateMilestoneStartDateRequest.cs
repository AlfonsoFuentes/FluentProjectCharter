using Shared.Models.FileResults.Generics.Request;
using Shared.Models.Milestones.Responses;

namespace Shared.Models.Milestones.Requests
{
    public class UpdateMilestoneAllDatesRequest : UpdateMessageResponse, IRequest
    {

      
        public List<MilestoneResponse> Items { get; set; } = new();
        public Guid ProjectId { get; set; }
    

        public string EndPointName => StaticClass.Milestones.EndPoint.UpdateAllDates;

    
        public override string Legend => $"Milestones";

        public override string ClassName => StaticClass.Milestones.ClassName;

    }
}
