using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Database.Entities.BudgetItems
{
    public class BudgetItemNewGanttTask : AuditableEntity<Guid>, ITenantEntity
    {

        public string TenantId { get; set; } = string.Empty;
        public BudgetItem BudgetItem { get; set; } = null!;
        public Guid BudgetItemId { get; set; }
        public NewGanttTask NewGanttTask { get; set; } = null!;
        public Guid NewGanttTaskId { get; set; }
        public double PercentageBudget { get; set; }
        public double BudgetAssigned { get; set; }
       
        public static BudgetItemNewGanttTask Create(Guid _BudgetItemId, Guid _NewGanttTaskId)
        {
            return new BudgetItemNewGanttTask()
            {
                Id = Guid.NewGuid(),
                BudgetItemId = _BudgetItemId,
                NewGanttTaskId = _NewGanttTaskId,


            };
        }

    }

}
