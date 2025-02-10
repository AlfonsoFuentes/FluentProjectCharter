using Shared.Models.Resources.Requests;
using Shared.Models.Resources.Responses;

namespace Shared.Models.Resources.Mappers
{
    public static class ResourceMapper
    {
        public static ChangeResourceOrderDowmRequest ToDown(this ResourceResponse response)
        {
            return new()
            {
                Id = response.Id,
                Name = response.Name,
                ProjectId = response.ProjectId,
                Order = response.Order,


            };
        }
        public static ChangeResourceOrderUpRequest ToUp(this ResourceResponse response)
        {
            return new()
            {
                ProjectId = response.ProjectId,
                Id = response.Id,
                Name = response.Name,
                Order = response.Order,
            };
        }
        public static UpdateResourceRequest ToUpdate(this ResourceResponse response)
        {
            return new()
            {
                Id = response.Id,
                Name = response.Name,
                ProjectId = response.ProjectId,
            };
        }
        public static CreateResourceRequest ToCreate(this ResourceResponse response, Guid? startid, Guid? planid)
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
