using Server.Database.Entities.PurchaseOrders;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Database.Entities.ProjectManagements
{
    public class Deliverable : AuditableEntity<Guid>, ITenantEntity
    {
        public string TenantId { get; set; } = string.Empty;
        public Project Project { get; set; } = null!;
        public Guid ProjectId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Answer { get; set; } = string.Empty;
        public int LabelOrder { get; set; }
        public string WBS { get; set; } = string.Empty;
        public static Deliverable Create(Guid ProjectId, Guid? StartId, Guid? PlanningId)
        {
            return new()
            {
                Id = Guid.NewGuid(),
                StartId = StartId,
                ProjectId = ProjectId,
         
                PlanningId = PlanningId,
            };
        }
        public Guid? StartId { get; set; }
        public Guid? PlanningId { get; set; }


        // Relación padre-hijo
        public Guid? ParentDeliverableId { get; set; } // Referencia al padre (opcional)
        public Deliverable ParentDeliverable { get; set; } = null!;

        public List<Deliverable> SubDeliverables { get; set; } = new List<Deliverable>(); // Colección de subtareas

        // Dependencias
        public Deliverable? Dependant { get; set; } = null!;
        public Guid? DependentantId { get; set; }

        [ForeignKey("DependentantId")]
        public List<Deliverable> Dependants { get; set; } = new List<Deliverable>(); // Colección de subtareas
        public string? Lag { get; set; } = string.Empty;
        public string? Duration { get; set; } = string.Empty;
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string DependencyType { get; set; } = string.Empty;

        [ForeignKey("DeliverableId")]
        public ICollection<BudgetItem> BudgetItems { get; set; } = new List<BudgetItem>();

        public ICollection<DeliverableResource> DeliverableResources { get; set; } = new List<DeliverableResource>();

        [ForeignKey("DeliverableId")]
        public ICollection<PurchaseOrder> PurchaseOrders { get; set; } = new List<PurchaseOrder>(); // Colección de subtareas
        public bool ShowBudgetItems { get; set; }
    }
}
