using Shared.Enums.CostCenter;
using Shared.Enums.Focuses;
using Shared.Enums.ProjectNeedTypes;
using Shared.Models.StakeHolders.Responses;

namespace Shared.Models.Projects.Reponses
{
    public class ProjectResponse : BaseResponse
    {
        public ProjectNeedTypeEnum ProjectNeedType { get; set; } = ProjectNeedTypeEnum.None;
      
        public DateTime? InitialProjectDate { get; set; } = DateTime.Today;
        public string ProjectNumber { get; set; } = string.Empty;
        public ProjectStatusEnum Status { get; set; } = ProjectStatusEnum.None;
        public double PercentageEngineering { get; set; }
        public double PercentageContingency { get; set; }
        public double PercentageTaxes { get; set; }
        public bool IsProductive { get; set; } = true;
        public CostCenterEnum CostCenter { get; set; } = CostCenterEnum.None;
        public FocusEnum Focus { get; set; } = FocusEnum.None;

    }

}
