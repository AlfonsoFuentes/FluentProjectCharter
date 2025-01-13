namespace Server.Database.Entities.BudgetItems.ProcessFlowDiagrams.Nozzles
{
    public class NozzleTemplate : AuditableEntity<Guid>, ITenantCommon
    {
        public string ConnectionType { get; set; } = string.Empty;
        public string NominalDiameter { get; set; } = string.Empty;
        public string NozzleType { get; set; } = string.Empty;
        public Template Template { get; set; } = null!;
        public Guid? TemplateId { get; set; }
        public static NozzleTemplate Create(Guid templateId)
        {
            return new()
            {
                Id = Guid.NewGuid(),
                TemplateId = templateId
            };
        }


    }

}
