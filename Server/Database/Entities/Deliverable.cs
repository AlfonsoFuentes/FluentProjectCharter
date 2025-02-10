using Server.Database.Contracts;
using Server.Database.Entities.BudgetItems;
using Server.Database.Entities.BudgetItems.ProcessFlowDiagrams;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Database.Entities
{
    public class Deliverable : AuditableEntity<Guid>, ITenantEntity
    {
        public string TenantId { get; set; } = string.Empty;
        public Project Project { get; set; } = null!;
        public Guid ProjectId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Answer { get; set; } = string.Empty;


        public static Deliverable Create(Guid ProjectId, Guid? StartId, Guid? PlanningId, int Order)
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




    }
    
}
