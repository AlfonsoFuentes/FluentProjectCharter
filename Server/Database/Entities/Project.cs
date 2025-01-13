using Server.Database.Contracts;
using Server.Database.Entities.BudgetItems;
using Server.Database.Entities.BudgetItems.ProcessFlowDiagrams;

namespace Server.Database.Entities
{
    public class Project : AuditableEntity<Guid>, ITenantEntity
    {
        public string TenantId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public List<Case> Cases { get; set; } = new();

        public string ProjectNeedType { get; set; } = string.Empty;
        public static Project Create()
        {
            return new Project()
            {
                Id = Guid.NewGuid(),
            };
        }
        public double InitialBudget { get; set; }
        public string ProjectDescription { get; set; } = string.Empty;
        public List<HighLevelRequirement> HighLevelRequirements { get; set; } = new();
        public List<StakeHolder> StakeHolders { get; } = [];

        public StakeHolder? Manager { get; set; } = null!;
        public Guid? ManagerId { get; set; }
        public StakeHolder? Sponsor { get; set; } = null!;
        public Guid? SponsorId { get; set; }

        public DateTime? Version0Date { get; set; }
        public string Status { get; set; } = string.Empty;
        public string ProjectNumber { get; set; } = string.Empty;
        public Guid? MeetingId { set; get; }
        public string? MeetingTab { get; set; } = string.Empty;
        public List<Meeting> Meetings { get; set; } = new();

        //Desactualizado

        public List<IssueLog> IssueLogs { get; set; } = new();
        public List<LearnedLesson> LearnedLessons { get; set; } = new();
        public List<WBSComponent> WBSComponents { get; set; } = new();
        public List<BudgetItem> BudgetItems { get; set; } = new();
        public List<ProcessFlowDiagram> ProcessFlowDiagrams { get; set; } = new();

        public double PercentageEngineering { get; set; }
        public double PercentageContingency { get; set; }
        public double PercentageTaxProductive { get; set; }
        public bool IsProductiveAsset { get; set; } = true;
        public bool IsNodeOpen { get; set; }
        public string? Tab { get; set; } = string.Empty;
    }

}
