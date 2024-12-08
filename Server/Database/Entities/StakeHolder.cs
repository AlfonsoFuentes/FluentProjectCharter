using Server.Database.Contracts;

namespace Server.Database.Entities
{
    public class StakeHolder : AuditableEntity<Guid>, ITenantCommon
    {
        public Case Case { get; set; } = null!;
        public Guid CaseId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public static StakeHolder Create(Guid CaseId)
        {
            return new()
            {
                Id = Guid.NewGuid(),
                CaseId = CaseId,
            };
        }
    }
}
