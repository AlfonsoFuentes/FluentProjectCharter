using Server.Database.Contracts;
using Server.Database.Entities.BudgetItems.ProcessFlowDiagrams;

namespace Server.Database.Entities.BudgetItems.ProcessFlowDiagrams.Nozzles
{
    public class NozzleTemplate : AuditableEntity<Guid>, ITenantCommon
    {
        public string ConnectionType { get; set; } = string.Empty;
        public string Diameter { get; set; } = string.Empty;
        public Template Template { get; set; } = null!;
        public Guid? TemplateId { get; set; }


    }

}
