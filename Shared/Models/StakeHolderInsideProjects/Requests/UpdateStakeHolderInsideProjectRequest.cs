using Shared.Enums.StakeHolderTypes;
using Shared.Models.FileResults.Generics.Request;
using Shared.Models.StakeHolders.Responses;

namespace Shared.Models.StakeHolderInsideProjects.Requests
{
    public class UpdateStakeHolderInsideProjectRequest : UpdateMessageResponse, IRequest
    {
        public Guid Id { get; set; }
        public StakeHolderResponse StakeHolder { get; set; } = new();
        public Guid ProjectId { get; set; }

        public string EndPointName => StaticClass.StakeHolderInsideProjects.EndPoint.Update;

        public StakeHolderRoleEnum Role { get; set; } = StakeHolderRoleEnum.None;
        public override string Legend => StakeHolder == null ? string.Empty : StakeHolder.Name;

        public override string ClassName => StaticClass.StakeHolderInsideProjects.ClassName;
    }
}
