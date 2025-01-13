using Server.Database.Contracts;

namespace Server.Database.Entities
{
    public class LearnedLesson : AuditableEntity<Guid>, ITenantCommon
    {
        public string Name { get; set; } = string.Empty;
        public Guid ProjectId { get; set; }
        public Project Project { get; set; } = null!;
    }
}
