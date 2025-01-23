using Server.Database.Contracts;

namespace Server.Database.Entities
{
    public class Assumption : AuditableEntity<Guid>, ITenantEntity
    {
        public string TenantId { get; set; } = string.Empty;

        public Project Project { get; set; } = null!;
        public Guid ProjectId { get; set; }
        public Scope? Scope { get; set; } = null!;
        public Guid? ScopeId { get; set; }


        public string Name { set; get; } = string.Empty;

        public static Assumption Create(Guid ProjectId, Guid? ScopeId)
        {
            return new Assumption()
            {
                Id = Guid.NewGuid(),
                ScopeId = ScopeId,
                ProjectId = ProjectId,
            };
        }
    }
   
}
