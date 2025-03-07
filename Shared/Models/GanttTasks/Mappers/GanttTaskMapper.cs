using Shared.Models.GanttTasks.Requests;
using Shared.Models.GanttTasks.Responses;

namespace Shared.Models.GanttTasks.Mappers
{
    public static class GanttTaskMapper
    {
        public static UpdateDeliverableExpandRequest ToExpand(this DeliverableWithGanttTaskResponse response)
        {
            return new UpdateDeliverableExpandRequest()
            {
                IsExpanded = response.IsExpanded,
                Id = response.DeliverableId,
                Name = response.Name,
                ProjectId = response.ProjectId,

            };
        }
        public static UpdateGanttTaskExpandRequest ToExpand(this GanttTaskResponse response)
        {
            return new()
            {
                DeliverableId = response.DeliverableId,
                IsExpanded = response.IsExpanded,
                Id = response.Id,
                Name = response.Name,
            };
        }

        public static DeliverableWithGanttTaskResponseToUpdate ToUpdate(this DeliverableWithGanttTaskResponse responseList)
        {
            return new()
            {
                DeliverableId = responseList.DeliverableId,
                Name = responseList.Name,
                FlatOrderedItems = responseList.FlatOrderedItems,
                IsExpanded = responseList.IsExpanded,
            };

        }
        public static DeliverableWithGanttTaskResponse ToResponse(this DeliverableWithGanttTaskResponseToUpdate responseList)
        {
            return new()
            {
                DeliverableId = responseList.DeliverableId,
                Name = responseList.Name,
                Items = responseList.Items,
                LabelOrder = responseList.LabelOrder,
                WBS = responseList.WBS,
                IsExpanded = responseList.IsExpanded,
            };

        }
        public static DeliverableWithGanttTaskResponseList ToReponse(this DeliverableWithGanttTaskResponseListToUpdate response)
        {
            return new()
            {
                ProjectId = response.ProjectId,
                Deliverables = response.Deliverables.Select(x => x.ToResponse()).ToList(),
                ProjectName = response.ProjectName,

            };

        }
        public static DeliverableWithGanttTaskResponseListToUpdate ToUpdate(this DeliverableWithGanttTaskResponseList request)
        {
            return new()
            {
                ProjectId = request.ProjectId,
                Deliverables = request.Deliverables.Select(x => x.ToUpdate()).ToList(),
            };
        }
    }

}
