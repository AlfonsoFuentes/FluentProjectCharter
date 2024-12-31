using Server.Database.Contracts;
using Server.Database.Entities.BudgetItems;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Database.Entities
{
    public class Deliverable : AuditableEntity<Guid>, ITenantEntity
    {
        public string TenantId { get; set; } = string.Empty;
        public Scope Scope { get; set; } = null!;
        public Guid ScopeId { get; set; }
        public string Name { get; set; }=string.Empty;
        public List<Requirement> Requirements { get; set; } = new();
        public List<Assumption> Assumptions { get; set; } = new();
        public List<DeliverableRisk> DeliverableRisks { get; set; } = new();
        public List<Constrainst> Constraints { get; set; } = new();
        public List<Bennefit> Bennefits { get; set; } = new();
        public List<AcceptanceCriteria> AcceptanceCriterias { get; set; } = new();
        public string? DeliverableTab { get; set; } = string.Empty;

        public static Deliverable Create(Guid ScopeId)
        {
            return new()
            {
                Id = Guid.NewGuid(),
                ScopeId = ScopeId,
            };
        }

        public Deliverable? SubDeliverable { get; set; } = null!;
        public Guid? SubDeliverableId { get; set; } = Guid.Empty;
        [ForeignKey("SubDeliverableId")]
        public List<Deliverable> SubDeliverables { get; set; } = new();

        [ForeignKey("BudgetItemId")]
        public List<BudgetItem> BudgetItems { get; set; } = new();

    }
}
