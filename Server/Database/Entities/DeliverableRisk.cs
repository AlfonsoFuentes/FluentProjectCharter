using Server.Database.Contracts;

namespace Server.Database.Entities
{
    public class DeliverableRisk : AuditableEntity<Guid>, ITenantEntity
    {
        public string TenantId { get; set; } = string.Empty;
        public Scope Scope { get; set; } = null!;
        public Guid ScopeId { get; set; }

   
        public string Name { get; set; } = string.Empty;
        public static DeliverableRisk Create(Guid ScopeId)
        {
            return new()
            {
                Id = Guid.NewGuid(),
                ScopeId = ScopeId,
            };
        }
    }
}
