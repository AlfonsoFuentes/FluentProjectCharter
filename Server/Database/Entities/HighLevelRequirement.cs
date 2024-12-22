using Server.Database.Contracts;

namespace Server.Database.Entities
{
    public class HighLevelRequirement : AuditableEntity<Guid>, ITenantEntity
    {
        public string TenantId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public static HighLevelRequirement Create(Guid _projectId)
        {
            return new HighLevelRequirement()
            {
                Id = Guid.NewGuid(),
                ProjectId = _projectId,
            };
        }
        public Project Project { get; set; } = null!;
        public Guid ProjectId { get; set; }
    }

}
