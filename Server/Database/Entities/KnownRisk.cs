using Server.Database.Contracts;

namespace Server.Database.Entities
{
    public class KnownRisk : AuditableEntity<Guid>, ITenantEntity
    {
        public string TenantId { get; set; } = string.Empty;
        public Project Project { get; set; } = null!;
        public Guid ProjectId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Answer { get; set; } = string.Empty;
        public static KnownRisk Create(Guid ProjectId, Guid? StartId, Guid? PlanningId, int Order)
        {
            return new()
            {
                Id = Guid.NewGuid(),
                ProjectId = ProjectId,
                Order = Order,
                StartId = StartId,
                PlanningId = PlanningId,
            };
        }

        public Guid? StartId { get; set; }
        public Guid? PlanningId { get; set; }
    }
}
