using Shared.Models.Deliverables.Requests;
using Shared.Models.Deliverables.Responses;

namespace Shared.Models.Deliverables.Mappers
{
    public static class DeliverableMapper
    {
        public static UpdateDeliverableExpandRequest ToExpand(this DeliverableResponse response)
        {
            return new()
            {
                ProjectId = response.ProjectId,
                Expanded = response.IsExpanded,
                Id = response.Id,
                Name = response.Name,
            };
        }
        public static CreateDeliverableRequest ToCreate(this DeliverableResponse response)
        {
            return new()
            {

                Name = response.Name,
                ProjectId = response.ProjectId,
                Order = response.Order,
                WBS = response.WBS,
                StartDate = response.StartDate,
                DependencyType = response.DependencyType.Name,
                Duration = response.Duration,
                EndDate = response.EndDate,
                LabelOrder = response.LabelOrder,
                Lag = response.Lag,
                ParentDeliverableId = response.ParentDeliverableId,
                Dependants = response.Dependants,

            };
        }
        public static DeliverableResponseListToUpdate ToUpdate(this DeliverableResponseList responseList)
        {
            return new()
            {
                ProjectId = responseList.ProjectId,
                FlatOrderedItems = responseList.FlatOrderedItems,
            };

        }
        public static DeliverableResponseList ToResponse(this DeliverableResponseListToUpdate responseList)
        {
            return new()
            {
                ProjectId = responseList.ProjectId,
                Items = responseList.Items,
            };

        }
    }

}
