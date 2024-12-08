namespace Shared.Models.DecissionCriterias.Responses
{
    public class DecissionCriteriaResponse : BaseResponse
    {

        public Guid CaseId { get; set; }
        public Guid ProjectId { get; set; }
    }
}
