namespace Shared.Models.AppStates.Records
{
    public class GetAppStateByIdDeliverableRequest 
    {

        public Guid Id { get; set; }
        public Guid ProjectId { get; set; }
        public string DeliverableAcordionActive { get; set; } = string.Empty;
  
    }

}
