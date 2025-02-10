using Shared.Models.FileResults.Generics.Reponses;

namespace Shared.Models.Objectives.Responses
{
    public class ObjectiveResponse : BaseResponse, IUpdateStateResponse
    {
        public string EndPointName => StaticClass.Objectives.EndPoint.UpdateState;
        public Guid ProjectId { get; set; }
        public Guid? StartId { get; set; }
        public Guid? PlanningId { get; set; }
    }
}
