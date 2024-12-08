using Server.Database.Contracts;

namespace Server.Database.Entities
{
    public class DecissionCriteria : AuditableEntity<Guid>, ITenantEntity
    {
        public string TenantId { get; set; } = string.Empty;
        public Case Case { get; set; } = null!;
        public Guid CaseId { get; set; }
        public string Name { get; set; } = string.Empty;
        public static DecissionCriteria Create(Guid CaseId)
        {
            return new()
            {
                Id = Guid.NewGuid(),
                CaseId = CaseId,
            };
        }
    }
}
