using Shared.Models.FileResults.Generics.Request;
using Shared.Models.StakeHolders.Responses;

namespace Shared.Models.ExpertJudgements.Requests
{
    public class CreateExpertJudgementRequest : CreateMessageResponse, IRequest
    {
        public Guid CaseId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string EndPointName => StaticClass.ExpertJudgements.EndPoint.Create;
        public Guid ProjectId { get; set; }
        public override string Legend => Name;
        public StakeHolderResponse? Expert { get; set; }
        public override string ClassName => StaticClass.ExpertJudgements.ClassName;
    }
}
