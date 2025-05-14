using Shared.Models.NewDeliverablesGanttTask.NewGanttTask.Responses;

namespace Shared.Models.NewDeliverablesGanttTask.Interfaces
{
    public interface IPublisher
    {
        void AddObserver(PublisherObserverResponse observador);
        void RemoveObserver(PublisherObserverResponse observador);
        void NotifyToObservers();
    }
}
