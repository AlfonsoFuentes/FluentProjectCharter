using Shared.Enums.ProjectNeedTypes;
using Shared.Models.FileResults.Generics.Request;
using Shared.Models.StakeHolders.Responses;

namespace Shared.Models.Projects.Request
{
    public class ApproveProjectRequest : UpdateMessageResponse, IRequest
    {
        public string EndPointName => StaticClass.Projects.EndPoint.Approve;
        public override string ClassName => StaticClass.Projects.ClassName;
        public override string Legend => Name;
        public string ProjectNumber { get; set; } = string.Empty;
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public StakeHolderResponse Manager { get; set; } = null!;
        public StakeHolderResponse Sponsor { get; set; } = null!;
        public DateTime? InitialProjectDate { get; set; } = DateTime.Today;
        public double PercentageTaxProductive { get; set; }
        public bool IsProductiveAsset { get; set; } = true;
        public ProjectNeedTypeEnum ProjectNeedType { get; set; } = ProjectNeedTypeEnum.None;
        public double PercentageEngineering { get; set; }
        public double PercentageContingency { get; set; }
    }
}
