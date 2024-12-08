using Server.Database.Contracts;

namespace Server.Database.Entities
{
    public class BackGround : AuditableEntity<Guid>, ITenantEntity
    {
        public string TenantId { get; set; } = string.Empty;
        public Case Case { get; set; } = null!;
        public Guid CaseId { get; set; }
        public string Name { get; set; } = string.Empty;
        public static BackGround Create(Guid CaseId)
        {
            return new BackGround()
            {
                Id = Guid.NewGuid(),
                CaseId = CaseId,
            };
        }
    }
}
