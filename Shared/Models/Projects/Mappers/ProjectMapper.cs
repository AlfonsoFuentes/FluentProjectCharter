using Shared.Models.Projects.Reponses;

namespace Shared.Models.Projects.Mappers
{
    public static class ProjectMapper
    {
        public static ChangeProjectOrderDowmRequest ToDown(this ProjectResponse response)
        {
            return new()
            {
                Id = response.Id,
                Name = response.Name,
         
                Order = response.Order,


            };
        }
        public static ChangeProjectOrderUpRequest ToUp(this ProjectResponse response)
        {
            return new()
            {
              
                Id = response.Id,
                Name = response.Name,
                Order = response.Order,
            };
        }
        public static UpdateProjectNameRequest ToUpdateName(this ProjectResponse response)
        {
            return new()
            {
                Id = response.Id,
                Name = response.Name,
               
            };
        }
        public static CreateProjectRequest ToCreate(this ProjectResponse response)
        {
            return new()
            {
                Name = response.Name,

            };
        }
    }

}
