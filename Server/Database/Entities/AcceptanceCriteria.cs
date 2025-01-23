using Server.Database.Contracts;

namespace Server.Database.Entities
{
    public class AcceptanceCriteria : AuditableEntity<Guid>, ITenantEntity
    {
        public string TenantId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;

        public Scope Scope { get; set; } = null!;
        public Guid ScopeId { get; set; }

   
        public static AcceptanceCriteria Create(Guid ScopeId)
        {
            return new()
            {
                Id = Guid.NewGuid(),
                ScopeId = ScopeId,
            };
        }
    }
}
