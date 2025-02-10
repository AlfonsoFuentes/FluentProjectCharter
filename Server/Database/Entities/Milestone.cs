using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Database.Entities
{
    public class Milestone : AuditableEntity<Guid>, ITenantEntity
    {
        public string TenantId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;


        public Project Project { get; set; } = null!;
        public Guid ProjectId { get; set; }

        public static Milestone Create(Guid ProjectId, Guid? StartId, Guid? PlanningId, int Order)
        {
            return new()
            {
                Id = Guid.NewGuid(),
                StartId = StartId,
                ProjectId = ProjectId,
                Order = Order,
                PlanningId = PlanningId,
            };
        }
        public Guid? StartId { get; set; }
        public Guid? PlanningId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public string Duration { get; set; } = string.Empty;

        // Relación padre-hijo
        public Guid? ParentMilestoneId { get; set; } // Referencia al padre (opcional)
        public Milestone ParentMilestone { get; set; } = null!;

        public List<Milestone> SubMilestones { get; set; } = new List<Milestone>(); // Colección de subtareas

        // Dependencias
        public Milestone? Dependant { get; set; } = null!;
        public Guid? DependentantId { get; set; }

        [ForeignKey("DependentantId")]
        public ICollection<Milestone> Dependants { get; set; } = new List<Milestone>(); // Colección de subtareas

        public string DependencyType { get; set; } = string.Empty;

    }
}
