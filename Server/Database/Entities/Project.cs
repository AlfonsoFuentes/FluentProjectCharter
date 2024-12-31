using Server.Database.Contracts;
using Server.Database.Entities.BudgetItems;
using Server.Database.Entities.BudgetItems.ProcessFlowDiagrams;

namespace Server.Database.Entities
{
    public class Project : AuditableEntity<Guid>, ITenantEntity
    {
        public string TenantId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;

        public List<LearnedLesson> LearnedLessons { get; set; } = new();
        public List<Case> Cases { get; set; } = new();
        public List<IssueLog> IssueLogs { get; set; } = new();
        public List<BudgetItem> BudgetItems { get; set; } = new();
        public List<ProcessFlowDiagram> ProcessFlowDiagrams { get; set; } = new();
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

        public DateTime? Version0Date { get; set; }
        public string ProjectNumber {  get; set; } = string.Empty;
        public StakeHolder? Manager { get; set; } = null!;
        public Guid ? ManagerId {  get; set; }
        public StakeHolder? Sponsor { get; set; } = null!;
        public Guid? SponsorId { get; set; }
      
        public string? ProjectTab {  get; set; } = string.Empty;
        public Guid? CaseId { get; set; }

        public List<WBSComponent> WBSComponents { get; set; } = new();
    }

}
