using Shared.Enums.StakeHolderTypes;
using Shared.Models.FileResults.Generics.Request;
using Shared.Models.StakeHolders.Responses;

namespace Shared.Models.StakeHolderInsideProjects.Requests
{
    public class CreateStakeHolderInsideProjectRequest : CreateMessageResponse, IRequest
    {
        public Guid? StartId { get; set; }
        public Guid? PlanningId { get; set; }
        public Guid ProjectId { get; set; }


        public StakeHolderResponse StakeHolder { get; set; } = null!;

        public StakeHolderRoleEnum Role { get; set; } = StakeHolderRoleEnum.None;
        public string EndPointName => StaticClass.StakeHolderInsideProjects.EndPoint.Create;

        public override string Legend => StakeHolder == null ? string.Empty : StakeHolder.Name;

        public override string ClassName => StaticClass.StakeHolders.ClassName;
    }

}
