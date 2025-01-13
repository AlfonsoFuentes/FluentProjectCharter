using Server.Database.Contracts;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Database.Entities.BudgetItems.ProcessFlowDiagrams
{
    public class ProcessFlowDiagram : AuditableEntity<Guid>, ITenantEntity
    {
        public string TenantId { get; set; } = string.Empty;
        public Project Project { get; set; } = null!;
        public Guid ProjectId { get; set; }

        [ForeignKey("ProcessFlowDiagramId")]
        public ICollection<EngineeringItem> EngineeringItems { get; set; } = new List<EngineeringItem>();

        public string Name { get; set; } = string.Empty;
        public string Tag { get; set; } = string.Empty;

        

    }

}
