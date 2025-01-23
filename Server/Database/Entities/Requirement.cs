using Server.Database.Contracts;

namespace Server.Database.Entities
{
    public class Requirement : AuditableEntity<Guid>, ITenantEntity
    {
        public string TenantId { get; set; } = string.Empty;

        public Project Project { get; set; } = null!;
        public Guid ProjectId { get; set; }
        public Scope? Scope { get; set; } = null!;
        public Guid? ScopeId { get; set; }
        public string Name { set; get; } = string.Empty;
        public string Type {  set; get; } = string.Empty;
        public StakeHolder? RequestedBy { get; set; }
        public Guid? RequestedById { get; set; }
        public StakeHolder? Responsible { get; set; }
        public Guid? ResponsibleId { get; set; }
        public DateTime? DueDate { get; set; }
        public string Priority {  set; get; } = string.Empty;
        public static Requirement Create(Guid ProjectId, Guid? ScopeId)
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
