namespace Server.Database.Entities.ProjectManagements
{
    public class DeliverableResource : AuditableEntity<Guid>, ITenantEntity
    {
        public string TenantId { get; set; } = string.Empty;
        public Deliverable Deliverable { get; set; } = null!;
        public Guid DeliverableId { get; set; }
        public Guid ResourceId { get; set; }
        public string Avalabilty { get; set; } = string.Empty;

    }
}
