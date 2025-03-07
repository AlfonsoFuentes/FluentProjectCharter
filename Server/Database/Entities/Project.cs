using Server.Database.Contracts;
using Server.Database.Entities.BudgetItems;
using Server.Database.Entities.BudgetItems.ProcessFlowDiagrams;
using static Shared.StaticClasses.StaticClass;
using System.ComponentModel.DataAnnotations.Schema;
using Server.Database.Entities.ProjectManagements;
using Server.Database.Entities.PurchaseOrders;

namespace Server.Database.Entities
{
    public class Project : AuditableEntity<Guid>, ITenantEntity
    {
        public string TenantId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;


        public DateTime? StartDate { get; set; }
        public string ProjectNeedType { get; set; } = string.Empty;
        public static Project Create(int Order)
        {
            return new Project()
            {
                Id = Guid.NewGuid(),
                Order = Order,
            };
        }

        public string Status { get; set; } = string.Empty;
        public string ProjectNumber { get; set; } = string.Empty;

        public double PercentageEngineering { get; set; }
        public double PercentageContingency { get; set; }
        public double PercentageTaxProductive { get; set; }
        public bool IsProductiveAsset { get; set; } = true;

        #region Scope Management
        public List<BackGround> BackGrounds { get; set; } = new();
        public List<Objective> Objectives { get; set; } = new();
        public List<Requirement> Requirements { get; set; } = new();
        public List<Scope> Scopes { get; set; } = new();
        public List<AcceptanceCriteria> AcceptanceCriterias { get; set; } = new();
        public List<Bennefit> Bennefits { get; set; } = new();
        public List<Constrainst> Constrainsts { get; set; } = new();
        public List<Assumption> Assumptions { get; set; } = new();
        public List<LearnedLesson> LearnedLessons { get; set; } = new();
        public List<ExpertJudgement> ExpertJudgements { get; set; } = new();
        #endregion
        #region Timeline
        public List<Deliverable> Deliverables { get; set; } = new();
  

        #endregion
        #region Budget
        public List<BudgetItem> BudgetItems { get; set; } = new();
        #endregion
        #region Quality
        public List<Quality> Qualitys { get; set; } = new();
        #endregion
        #region Risks
        public List<KnownRisk> KnownRisks { get; set; } = new();
        #endregion
        #region Communication
        public List<Communication> Communications { get; set; } = new();
        #endregion
        #region Resources
        public List<Resource> Resources { get; set; } = new();
        #endregion
        public List<Meeting> Meetings { get; set; } = new();
        public List<PurchaseOrder> PurchaseOrders { get; set; } = new();
        public List<Acquisition> Acquisitions { get; set; } = new();
        public List<StakeHolder> StakeHolders { get; } = [];
        public StakeHolder? Manager { get; set; } = null!;
        public Guid? ManagerId { get; set; }
        public StakeHolder? Sponsor { get; set; } = null!;
        public Guid? SponsorId { get; set; }

        public Guid StartId { get; set; }
        public Guid PlanningId { get; set; }
        public Guid ExecutingId { get; set; }
        public Guid MonitoringId { get; set; }
        public Guid ClosingId { get; set; }


    }


}
