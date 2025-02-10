using Shared.Enums.ProjectNeedTypes;
using Shared.Models.StakeHolders.Responses;

namespace Shared.Models.Projects.Reponses
{
    public class ProjectResponse : BaseResponse
    {
    
        public ProjectNeedTypeEnum ProjectNeedType { get; set; } = ProjectNeedTypeEnum.None;
        public StakeHolderResponse Manager { get; set; } = null!;
        public StakeHolderResponse Sponsor { get; set; } = null!;
        public DateTime? InitialProjectDate { get; set; } = DateTime.Today;
        public string ProjectNumber { get; set; } = string.Empty;
        public ProjectStatusEnum Status { get; set; } = ProjectStatusEnum.None;
        public double PercentageEngineering { get; set; }
        public double PercentageContingency { get; set; }
        public double PercentageTaxes { get; set; }
        public bool IsProductive { get; set; } = true;
        public Guid StartId { get; set; }
        public Guid PlanningId { get; set; }
        public Guid ExecutingId { get; set; }
        public Guid MonitoringId { get; set; }
        public Guid ClosingId { get; set; }



    }
  
}
