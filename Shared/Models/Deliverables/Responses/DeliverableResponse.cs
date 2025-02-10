using Shared.Models.FileResults.Generics.Reponses;
namespace Shared.Models.Deliverables.Responses
{
    public class DeliverableResponse : BaseResponse, IUpdateStateResponse
    {
     
        public Guid ProjectId { get; set; }
        public Guid? StartId { get; set; }
        public Guid? PlanningId { get; set; }


        public string EndPointName => StaticClass.Deliverables.EndPoint.UpdateState;
    }
}
