using Shared.Models.Deliverables.Requests;
using Shared.Models.Deliverables.Responses;
using Shared.Models.Deliverables.Responses.NewResponses;

namespace Shared.Models.Deliverables.Mappers
{
    public static class DeliverableMapper
    {

        public static CreateDeliverableRequest ToCreate(this DeliverableResponse response, Guid? startid=null, Guid? planid = null)
        {
            return new()
            {

                Name = response.Name,
                ProjectId = response.ProjectId,
                StartId = startid,
                PlanningId = planid,
                Order = response.Order,
                WBS = response.WBS,
                StartDate = response.StartDate,
                DependencyType = response.DependencyType.Name,
                Duration = response.Duration,
                EndDate = response.EndDate,
                LabelOrder = response.LabelOrder,
                Lag = response.Lag,



            };
        }
        public static DeliverableResponseListToUpdate ToUpdate(this DeliverableResponseList responseList )
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
