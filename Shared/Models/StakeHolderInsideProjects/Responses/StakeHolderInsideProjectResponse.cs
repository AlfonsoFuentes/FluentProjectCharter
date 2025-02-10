using Shared.Enums.StakeHolderTypes;
using Shared.Models.StakeHolders.Responses;

namespace Shared.Models.StakeHolderInsideProjects.Responses
{
    public class StakeHolderInsideProjectResponse : BaseResponse
    {
        public StakeHolderResponse StakeHolder { get; set; } = null!;

        public Guid ProjectId { get; set; }
        public StakeHolderRoleEnum Role { get; set; } = StakeHolderRoleEnum.None;
    }
}
