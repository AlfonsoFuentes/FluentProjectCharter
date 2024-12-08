using Server.Database.Contracts;

namespace Server.Database.Entities
{
    public class DeliverableRisk : AuditableEntity<Guid>, ITenantEntity
    {
        public string TenantId { get; set; } = string.Empty;
        public Deliverable Deliverable { get; set; } = null!;
        public Guid DeliverableId { get; set; }
        public string Name { get; set; } = string.Empty;
        public static DeliverableRisk Create(Guid DeliverableId)
        {
            return new()
            {
                Id = Guid.NewGuid(),
                DeliverableId = DeliverableId,
            };
        }
    }
}
