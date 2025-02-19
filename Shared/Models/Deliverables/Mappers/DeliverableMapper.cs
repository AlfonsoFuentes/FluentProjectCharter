using Shared.Models.Deliverables.Requests;
using Shared.Models.Deliverables.Responses;
using Shared.Models.Deliverables.Responses.NewResponses;

namespace Shared.Models.Deliverables.Mappers
{
    public static class DeliverableMapper
    {

        public static CreateDeliverableRequest ToCreate(this DeliverableResponse response, Guid? startid, Guid? planid)
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
                 
                


            };
        }
    }

}
