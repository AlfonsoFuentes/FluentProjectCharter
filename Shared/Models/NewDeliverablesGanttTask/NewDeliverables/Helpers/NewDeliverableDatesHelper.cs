using Shared.Models.NewDeliverablesGanttTask.Interfaces;
using Shared.Models.NewDeliverablesGanttTask.NewDeliverables.Responses;
using Shared.Models.NewDeliverablesGanttTask.NewGanttTask.Responses;

namespace Shared.Models.NewDeliverablesGanttTask.NewDeliverables.Helpers
{
    public static class NewDeliverableDatesHelper
    {
        public static DateTime? GetMinStartDateFromDependencies(this NewDeliverablesResponseList response, List<PublisherObserverResponse> Dependencies)
        {
            List<DateTime?> dates = new();
            foreach (var dependency in Dependencies)
            {
                var task = response.TaskFlatenList.FirstOrDefault(x => x.Task.Id == dependency.PublisherId);
                if (task != null)
                {
                    dates.Add(task.Task.PlannedStartDate);
                }
            }
            return dates.Min();
        }
        public static DateTime? GetMaxEndDateFromDependencies(this NewDeliverablesResponseList response, List<PublisherObserverResponse> Dependencies)
        {
            List<DateTime?> dates = new();
            foreach (var dependency in Dependencies)
            {
                var task = response.TaskFlatenList.FirstOrDefault(x => x.Task.Id == dependency.PublisherId);
                if (task != null)
                {
                    dates.Add(task.Task.PlannedEndDate);
                }
            }
            return dates.Max();
        }
        public static NewDeliverableGanttTaskRow GetTaskResponseFromDependencies(this NewDeliverablesResponseList response,
            PublisherObserverResponse _dependency)
        {
            return response.TaskFlatenList.FirstOrDefault(x => x.Task.Id == _dependency.PublisherId) ?? null!;

        }
        public static void RemoveObserver(this NewDeliverablesResponseList response, NewDeliverableGanttTaskRow currerow)
        {
            var dependencies = currerow.Task.GetDependencies();

            foreach (var dependency in dependencies)
            {
                // Buscar la tarea "observada" (de donde viene la dependencia)
                var observedTasks = response.TaskFlatenList
                    .Where(x => x.Task.Id == dependency.PublisherId)
                    .Select(x => x.Task)
                    .ToList();

                foreach (var task in observedTasks)
                {
                   task.RemoveObservers(x => x.ObservedId == currerow.Task.Id);
                   
                   
                }
            }
        }

        public static void CreatePublisherObserved(this NewDeliverablesResponseList response, NewDeliverableGanttTaskRow observer, NewDeliverableGanttTaskRow publisher)
        {
            PublisherObserverResponse taskDependencyResponse = new PublisherObserverResponse()
            {
                PublisherId = publisher.Task.Id,
                ObservedId = observer.Task.Id,
                PlannedStartDate = publisher.Task.PlannedStartDate,
                PlannedEndDate = publisher.Task.PlannedEndDate,

            };
            //response.PublisherObserverResponses.Add(taskDependencyResponse);
            publisher.Task.AddObserver(taskDependencyResponse);
            // Agregar la dependencia válida a la lista de dependientes
            observer.Task.AddDependency(taskDependencyResponse);
        }
        public static void RemovePublisherObserved(this NewDeliverablesResponseList response, NewDeliverableGanttTaskRow observer)
        {
            //response.PublisherObserverResponses.RemoveAll(x => x.ObservedId == observer.Task.Id);
            observer.Task.RemoveAllDependencies();
        }
    }
}
