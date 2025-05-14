using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Database.Entities.ProjectManagements
{
    public class NewGanttTask : AuditableEntity<Guid>, ITenantEntity
    {
        public Deliverable Deliverable { get; set; } = null!;
        public Guid DeliverableId { get; set; }
        public string TenantId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        [NotMapped]
        public Guid ProjectId => Deliverable == null ? Guid.Empty : Deliverable.ProjectId;
        public int InternalOrder { get; set; }

        public string ParentWBS { get; set; } = string.Empty;
        [NotMapped]
        public string WBS => $"{ParentWBS}.{InternalOrder}";
        public int MainOrder { get; set; }
        public static NewGanttTask Create(Guid DeliverableId)
        {
            return new()
            {
                Id = Guid.NewGuid(),

                DeliverableId = DeliverableId,


            };
        }
        public static NewGanttTask AddSubTask(Guid ParenTaskId, Guid DeliverableId)
        {
            return new()
            {
                Id = Guid.NewGuid(),

                ParentId = ParenTaskId,
                DeliverableId= DeliverableId,

            };
        }

        
        // Relación padre-hijo
        public Guid? ParentId { get; set; } // Referencia al padre (opcional)
        public NewGanttTask Parent { get; set; } = null!;
        [ForeignKey("ParentId")]
        public List<NewGanttTask> SubTasks { get; set; } = new List<NewGanttTask>(); // Colección de subtareas

        public string? DurationUnit { get; set; } = string.Empty;
        public double DurationInDays { get; set; } = 0;
        public double DurationInUnit { get; set; } = 0;
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public string? LagUnit { get; set; } = string.Empty;
        public double LagInDays { get; set; } = 0;
        public double LagInUnits { get; set; } = 0;

       
        

        public string? DependencyList { get; set; } = string.Empty;
        public int DependencyType { get; set; }

    }
}
