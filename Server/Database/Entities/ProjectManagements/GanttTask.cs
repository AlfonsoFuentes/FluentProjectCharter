using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Database.Entities.ProjectManagements
{
    public class GanttTask : AuditableEntity<Guid>, ITenantEntity
    {
        public Deliverable Deliverable { get; set; } = null!;
        public Guid DeliverableId { get; set; }
        public string TenantId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        [NotMapped]
        public Guid ProjectId => Deliverable == null ? Guid.Empty : Deliverable.ProjectId;
        public int LabelOrder { get; set; }
        public string WBS { get; set; } = string.Empty;
        public bool IsExpanded { get; set; }
        public static GanttTask Create(Guid DeliverableId)
        {
            return new()
            {
                Id = Guid.NewGuid(),

                DeliverableId = DeliverableId,


            };
        }


        // Relación padre-hijo
        public Guid? ParentId { get; set; } // Referencia al padre (opcional)
        public GanttTask Parent { get; set; } = null!;

        public List<GanttTask> SubTasks { get; set; } = new List<GanttTask>(); // Colección de subtareas

        // Dependencias
        public GanttTask? Dependant { get; set; } = null!;
        public Guid? DependentantId { get; set; }

        [ForeignKey("DependentantId")]
        public List<GanttTask> Dependants { get; set; } = new List<GanttTask>(); // Colección de subtareas
        public string? Lag { get; set; } = string.Empty;
        public string? Duration { get; set; } = string.Empty;
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string DependencyType { get; set; } = string.Empty;

        [ForeignKey("GanttTaskId")]
        public ICollection<BudgetItem> BudgetItems { get; set; } = new List<BudgetItem>();

        public ICollection<DeliverableResource> DeliverableResources { get; set; } = new List<DeliverableResource>();


        public bool ShowBudgetItems { get; set; }
    }

}
