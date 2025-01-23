using Server.Database.Contracts;

namespace Server.Database.Entities
{
    public class Constrainst : AuditableEntity<Guid>, ITenantEntity
    {
        public string TenantId { get; set; } = string.Empty;
        public Project Project { get; set; } = null!;
        public Guid ProjectId { get; set; }
        public Scope? Scope { get; set; } = null!;
        public Guid? ScopeId { get; set; }


        public string Name { get; set; } = string.Empty;
        public static Constrainst Create(Guid ProjectId, Guid? ScopeId)
        {
            return new()
            {
                Id = Guid.NewGuid(),
                ScopeId = ScopeId,
                ProjectId = ProjectId,
            };
        }
    }
   
}
