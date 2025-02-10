using Shared.Models.Assumptions.Requests;
using Shared.Models.Assumptions.Responses;

namespace Shared.Models.Assumptions.Mappers
{
    public static class AssumptionMapper
    {
        public static ChangeAssumptionOrderDowmRequest ToDown(this AssumptionResponse response)
        {
            return new()
            {
                Id = response.Id,
                Name = response.Name,
                ProjectId = response.ProjectId,
                Order = response.Order,


            };
        }
        public static ChangeAssumptionOrderUpRequest ToUp(this AssumptionResponse response)
        {
            return new()
            {
                ProjectId = response.ProjectId,
                Id = response.Id,
                Name = response.Name,
                Order = response.Order,
            };
        }
        public static UpdateAssumptionRequest ToUpdate(this AssumptionResponse response)
        {
            return new()
            {
                Id = response.Id,
                Name = response.Name,
                ProjectId = response.ProjectId,
            };
        }
        public static CreateAssumptionRequest ToCreate(this AssumptionResponse response, Guid? startid, Guid? planid)
        {
            return new()
            {

                Name = response.Name,
                ProjectId = response.ProjectId,
                StartId = startid,
                PlanningId = planid
            };
        }
    }

}
