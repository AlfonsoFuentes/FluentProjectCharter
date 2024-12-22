using Shared.Enums.StakeHolderTypes;

namespace Shared.Models.StakeHolders.Responses
{
    public class StakeHolderInsideProjectResponse : BaseResponse
    {
        public StakeHolderResponse StakeHolder { get; set; } = null!;
      
        public Guid ProjectId { get; set; }
        public StakeHolderRoleEnum Role { get; set; } = StakeHolderRoleEnum.None;
    }
}
