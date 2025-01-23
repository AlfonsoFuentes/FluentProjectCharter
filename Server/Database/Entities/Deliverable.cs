using Server.Database.Contracts;
using Server.Database.Entities.BudgetItems;
using Server.Database.Entities.BudgetItems.ProcessFlowDiagrams;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Database.Entities
{
    public class Deliverable : AuditableEntity<Guid>, ITenantEntity
    {
        public string TenantId { get; set; } = string.Empty;
        public Scope Scope { get; set; } = null!;
        public Guid ScopeId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Order { get; set; }
        public bool IsNodeOpen { get; set; }
        public string? Tab { get; set; } = string.Empty;

        public static Deliverable Create(Guid ScopeId, int order)
        {
            return new()
            {
                Id = Guid.NewGuid(),
                ScopeId = ScopeId,
                Order = order,
            };
        }



        [ForeignKey("DeliverableId")]
        public List<BudgetItem> BudgetItems { get; set; } = new();

        [ForeignKey("DeliverableId")]
        public List<WBSComponent> WBSComponents { get; set; } = new();


    }
}
