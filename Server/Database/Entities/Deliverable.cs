using Server.Database.Contracts;

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
        public static Deliverable Create(Guid ScopeId)
        {
            return new()
            {
                Id = Guid.NewGuid(),
                ScopeId = ScopeId,
            };
        }

    }
}
