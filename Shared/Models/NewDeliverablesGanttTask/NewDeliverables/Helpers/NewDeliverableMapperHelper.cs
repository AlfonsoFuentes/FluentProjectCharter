using Shared.Models.NewDeliverablesGanttTask.NewDeliverables.Responses;
using Shared.Models.NewDeliverablesGanttTask.NewGanttTask.Responses;

namespace Shared.Models.NewDeliverablesGanttTask.NewDeliverables.Helpers
{
    public static class NewDeliverableMapperHelper
    {
        public static NewDeliverableGanttTaskRow Map(this NewDeliverableResponse deliverable)
        {
            return new()
            {
                Deliverable = deliverable,
                Task = null!,
                ProjectId = deliverable.ProjectId,
                


            };
        }
        public static NewDeliverableGanttTaskRow Map(this NewTaskResponse task, NewDeliverableResponse deliverable)
        {
            return new()
            {
                Task = task,
                Deliverable = deliverable,
                ProjectId = task.ProjectId, 

            };
        }
        
    }
}
