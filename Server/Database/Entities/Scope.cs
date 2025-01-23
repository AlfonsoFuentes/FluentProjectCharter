using Server.Database.Contracts;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Database.Entities
{
    public class Scope : AuditableEntity<Guid>, ITenantEntity
    {
        public string TenantId { get; set; } = string.Empty;
        public Case Case { get; set; } = null!;
        public Guid CaseId { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<Deliverable> Deliverables { get; set; } = new();
        public bool IsNodeOpen { get; set; }
        public string? Tab { get; set; } = string.Empty;
        public static Scope Create(Guid CaseId)
        {
            return new()
            {
                Id = Guid.NewGuid(),
                CaseId = CaseId,
            };
        }
        public List<DeliverableRisk> DeliverableRisks { get; set; } = new();
        public List<Bennefit> Bennefits { get; set; } = new();
        public List<AcceptanceCriteria> AcceptanceCriterias { get; set; } = new();


        [ForeignKey("DeliverableId")]
        public List<Requirement> Requirements { get; set; } = new();
        [ForeignKey("DeliverableId")]
        public List<Assumption> Assumptions { get; set; } = new();
        [ForeignKey("DeliverableId")]
        public List<Constrainst> Constraints { get; set; } = new();

    }
}
