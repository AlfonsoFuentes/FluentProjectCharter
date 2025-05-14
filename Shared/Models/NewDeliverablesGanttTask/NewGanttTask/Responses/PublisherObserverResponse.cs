namespace Shared.Models.NewDeliverablesGanttTask.NewGanttTask.Responses
{
    public class PublisherObserverResponse 
    {
        public Guid ObservedId { get; set; }
        public Guid PublisherId { get; set; }
        public DateTime? PlannedStartDate { get; set; }
        public DateTime? PlannedEndDate { get; set; }
        public int MainOrder { get; set; }

        public void Update(int mainorder, DateTime? startdate, DateTime? enddate)
        {
            MainOrder = mainorder;
            PlannedStartDate = startdate;
            PlannedEndDate = enddate;
        }

       
    }
}
