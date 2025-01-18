using Server.Database.Contracts;

namespace Server.Database.Entities
{
    public class Constrainst : AuditableEntity<Guid>, ITenantEntity
    {
        public string TenantId { get; set; } = string.Empty;
        public Project Project { get; set; } = null!;
        public Guid ProjectId { get; set; }
        public Deliverable? Deliverable { get; set; } = null!;
        public Guid? DeliverableId { get; set; }


        public string Name { get; set; } = string.Empty;
        public static Constrainst Create(Guid ProjectId, Guid? DeliverableId)
        {
            return new()
            {
                Id = Guid.NewGuid(),
                DeliverableId = DeliverableId,
                ProjectId = ProjectId,
            };
        }
    }
   
}
