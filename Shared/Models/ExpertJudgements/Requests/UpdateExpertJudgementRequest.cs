using Shared.Models.FileResults.Generics.Request;
using Shared.Models.StakeHolders.Responses;

namespace Shared.Models.ExpertJudgements.Requests
{
    public class UpdateExpertJudgementRequest : UpdateMessageResponse, IRequest
    {
        public Guid CaseId { get; set; }
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public StakeHolderResponse Expert { get; set; } = null!;
        public string EndPointName => StaticClass.ExpertJudgements.EndPoint.Update;
        public Guid ProjectId { get; set; }
        public override string Legend => Name;

        public override string ClassName => StaticClass.ExpertJudgements.ClassName;
    }
}
