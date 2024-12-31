using Server.Database.Contracts;

namespace Server.Database.Entities
{
    public class AcceptanceCriteria : AuditableEntity<Guid>, ITenantEntity
    {
        public string TenantId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;

        public Deliverable Deliverable {  get; set; } = null!;
        public Guid DeliverableId {  get; set; }

    }
}
