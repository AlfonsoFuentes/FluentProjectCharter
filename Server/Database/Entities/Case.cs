using DocumentFormat.OpenXml.Drawing.Diagrams;
using Server.Database.Contracts;

namespace Server.Database.Entities
{
    public class Case : AuditableEntity<Guid>, ITenantEntity
    {
        public string TenantId { get; set; } = string.Empty;
        public Project Project { get; set; } = null!;
        public Guid ProjectId { get; set; }
        public string Name { get; set; } = string.Empty;
        public static Case Create(Guid ProjectId)
        {
            return new()
            {
                Id = Guid.NewGuid(),
                ProjectId = ProjectId,
            };
        }
        //Determinacion de que esta motivando la necesidad de accion
        public List<BackGround> BackGrounds { get; set; } = new();
        //Enunciado situacional que documente el problema o la oportunidad
      
       
        //Identificacion del Alcance
        public List<Scope> Scopes { get; set; } = new();

        //Identificacion de estrategias, metas y objetivos de la organizacion
        public OrganizationStrategy? OrganizationStrategy { get; set; } = null!;
        public Guid? OrganizationStrategyId { get; set; }

        //Identificacion de los riesgos conocidos
        public List<KnownRisk> KnownRisks { get; set; } = new();
        //Identificacion de factores criticos de exito
        public List<SucessfullFactor> SucessfullFactors { get; set; } = new();
        //Identificacion de los criterios de decision
        public List<DecissionCriteria> DecissionCriterias { get; set; } = new();
        public List<ExpertJudgement> ExpertJudgements { get; set; } = new();
        public string? CaseTab { get; set; } = string.Empty;
        public Guid? ScopeId { get; set; }

    }
}
