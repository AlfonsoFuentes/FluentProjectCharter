using Shared.Commons;
using Shared.Enums.ProjectNeedTypes;
using Shared.Models.FileResults.Generics.Request;
using Shared.Models.Projects.Reponses;
using Shared.Models.StakeHolders.Responses;
using Shared.StaticClasses;

namespace Shared.Models.Projects.Request
{
    public class CreateProjectRequest : CreateMessageResponse, IRequest
    {
        public string Name { get; set; } = string.Empty;
        public double InitialBudget { get; set; }
        public string ProjectDescription { get; set; } = string.Empty;
        public string EndPointName => StaticClass.Projects.EndPoint.Create;
        public ProjectNeedTypeEnum ProjectNeedType { get; set; }= ProjectNeedTypeEnum.None;
        public override string Legend => Name;

        public override string ClassName => StaticClass.Projects.ClassName;
        public double PercentageEngineering { get; set; }
        public double PercentageContingency { get; set; }
        public StakeHolderResponse Manager { get; set; } = null!;
        public StakeHolderResponse Sponsor { get; set; } = null!;
        public DateTime? InitialProjectDate { get; set; } = DateTime.Today;
        public ProjectStatusEnum Status { get; set; } = ProjectStatusEnum.Created;
        public double PercentageTaxProductive { get; set; }
        public bool IsProductiveAsset { get; set; } = true;

    }
}
