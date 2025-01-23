using Server.Database.Contracts;

namespace Server.Database.Entities
{
    public class WBSComponent : AuditableEntity<Guid>, ITenantEntity
    {
        public string TenantId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;

        public List<WBSComponent> SubComponents { get; set; } = new();

        public WBSComponent? SubComponentRelation { get; set; } = null!;
        public Guid? SubComponentRelationId { get; set; }

        public Project Project { get; set; } = null!;
        public Guid ProjectId { get; set; }

        public Deliverable? Deliverable { get; set; }
        public Guid? DeliverableId { get; set; }

        public static WBSComponent Create(Guid ProjecId, Guid DeliverableId)
        {
            return new WBSComponent
            {
                Id = Guid.NewGuid(),
                DeliverableId = DeliverableId,
                ProjectId = ProjecId,
            };
        }
        public static WBSComponent Create(Guid ProjecId, Guid DeliverableId, Guid WBSId)
        {
            return new WBSComponent
            {
                Id = Guid.NewGuid(),
                DeliverableId = DeliverableId,
                ProjectId = ProjecId,
                SubComponentRelationId = WBSId,
            };
        }

    }

}
