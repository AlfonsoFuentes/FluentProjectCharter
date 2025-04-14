using Shared.Models.Deliverables.Requests;
using Shared.Models.Deliverables.Responses;

namespace Shared.Models.Deliverables.Mappers
{
    public static class DeliverableMapper
    {
        public static ChangeDeliverableOrderDowmRequest ToDown(this DeliverableResponse response)
        {
            return new()
            {
                Id = response.Id,
                Name = response.Name,
                ProjectId = response.ProjectId,
                Order = response.Order,


            };
        }
        public static ChangeDeliverableOrderUpRequest ToUp(this DeliverableResponse response)
        {
            return new()
            {
                ProjectId = response.ProjectId,
                Id = response.Id,
                Name = response.Name,
                Order = response.Order,
            };
        }
        
    }

}
