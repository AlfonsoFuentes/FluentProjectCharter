using Shared.Models.Communications.Requests;
using Shared.Models.Communications.Responses;

namespace Shared.Models.Communications.Mappers
{
    public static class CommunicationMapper
    {
        public static ChangeCommunicationOrderDowmRequest ToDown(this CommunicationResponse response)
        {
            return new()
            {
                Id = response.Id,
                Name = response.Name,
                ProjectId = response.ProjectId,
                Order = response.Order,


            };
        }
        public static ChangeCommunicationOrderUpRequest ToUp(this CommunicationResponse response)
        {
            return new()
            {
                ProjectId = response.ProjectId,
                Id = response.Id,
                Name = response.Name,
                Order = response.Order,
            };
        }
        public static UpdateCommunicationRequest ToUpdate(this CommunicationResponse response)
        {
            return new()
            {
                Id = response.Id,
                Name = response.Name,
                ProjectId = response.ProjectId,
            };
        }
        public static CreateCommunicationRequest ToCreate(this CommunicationResponse response, Guid? startid, Guid? planid)
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
