using Server.Database.Contracts;

namespace Server.Database.Entities
{
    public class Scope : AuditableEntity<Guid>, ITenantEntity
    {
        public string TenantId { get; set; } = string.Empty;
        public Case Case { get; set; } = null!;
        public Guid CaseId { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<Deliverable> Deliverables { get; set; } = new();
        public string? ScopeTab { get; set; } = string.Empty;
        public Guid? DeliverableId { get; set; }
        public static Scope Create(Guid CaseId)
        {
            return new()
            {
                Id = Guid.NewGuid(),
                CaseId = CaseId,
            };
        }

    }
}
