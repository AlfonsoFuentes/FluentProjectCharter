using Shared.Models.FileResults.Generics.Reponses;

namespace Shared.Models.Scopes.Responses
{
    public class ScopeResponse : BaseResponse, IUpdateStateResponse
    {
        public string EndPointName => StaticClass.Scopes.EndPoint.UpdateState;
        public Guid ProjectId { get; set; }

        public Guid? StartId { get; set; }
        public Guid? PlanningId { get; set; }


    }
}
