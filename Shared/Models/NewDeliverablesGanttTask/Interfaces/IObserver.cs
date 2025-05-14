namespace Shared.Models.NewDeliverablesGanttTask.Interfaces
{
    public interface IPublisherObserver
    {
        Guid ObservedId { get; set; }
        Guid PublisherId { get; set; }
        int MainOrder { get; set; }
        DateTime? PlannedStartDate { get; set; }
        DateTime? PlannedEndDate { get; set; }
        void Update(int mainorder,DateTime? startdate,DateTime? enddate );
    }
}
