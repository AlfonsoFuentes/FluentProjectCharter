using Shared.Enums.ProjectNeedTypes;
using Shared.Models.Cases.Responses;
using Shared.Models.HighLevelRequirements.Responses;
using Shared.Models.StakeHolders.Responses;
using static Shared.StaticClasses.StaticClass;

namespace Shared.Models.Projects.Reponses
{
    public class ProjectResponse : BaseResponse
    {
        public string ProjectId=>Id.ToString();
        public ProjectNeedTypeEnum ProjectNeedType { get; set; } = ProjectNeedTypeEnum.None;
        public List<CaseResponse> Cases { get; set; } = new();
        public double InitialBudget { get; set; }
        public string ProjectDescription { get; set; } = string.Empty;
        public StakeHolderResponse Manager { get; set; } = null!;
        public StakeHolderResponse Sponsor { get; set; } = null!;
        public DateTime? InitialProjectDate { get; set; } = DateTime.Today;
        public List<HighLevelRequirementResponse> HighLevelRequirements { get; set; } = new();

        public List<StakeHolderInsideProjectResponse> StakeHolders { get; set; } = new();
        public string ProjectNumber { get; set; } = string.Empty;
    }
}
