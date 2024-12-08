using Server.Database.Contracts;

namespace Server.Database.Entities
{
    public class Requirement : AuditableEntity<Guid>, ITenantEntity
    {
        public string TenantId { get; set; } = string.Empty;

        public Deliverable Deliverable { get; set; } = null!;
        public Guid DeliverableId { get; set; }
        public string Name { set; get; } = string.Empty;
        public static Requirement Create(Guid DeliverableId)
        {
            return new()
            {
                Id = Guid.NewGuid(),
                DeliverableId= DeliverableId,
            };
        }
    }
}
