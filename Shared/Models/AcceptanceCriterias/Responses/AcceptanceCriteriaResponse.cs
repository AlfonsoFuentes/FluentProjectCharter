namespace Shared.Models.AcceptanceCriterias.Responses
{
    public class AcceptanceCriteriaResponse : BaseResponse
    {

        //public Guid? SubDeliverableId { get; set; }
        public Guid DeliverableId { get; set; }
        public Guid ProjectId { get; set; }

    }
}
