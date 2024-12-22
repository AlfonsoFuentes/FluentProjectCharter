using Shared.Models.Backgrounds.Responses;
using Shared.Models.DecissionCriterias.Responses;
using Shared.Models.ExpertJudgements.Responses;
using Shared.Models.KnownRisks.Responses;
using Shared.Models.OrganizationStrategies.Responses;
using Shared.Models.Scopes.Responses;
using Shared.Models.StakeHolders.Responses;
using Shared.Models.SucessfullFactors.Responses;
using static Shared.StaticClasses.StaticClass;
using static System.Formats.Asn1.AsnWriter;

namespace Shared.Models.Cases.Responses
{
    public class CaseResponse : BaseResponse
    {
        public Guid ProjectId { get; set; }
        //Determinacion de que esta motivando la necesidad de accion
        public List<BackGroundResponse> BackGrounds { get; set; } = new();
        //Enunciado situacional que documente el problema o la oportunidad
    

       
        //Identificacion del Alcance
        public List<ScopeResponse> Scopes { get; set; } = new();

        //Identificacion de estrategias, metas y objetivos de la organizacion
        public OrganizationStrategyResponse? OrganizationStrategy { get; set; } = null!;
   

        //Identificacion de los riesgos conocidos
        public List<KnownRiskResponse> KnownRisks { get; set; } = new();
        //Identificacion de factores criticos de exito
        public List<SucessfullFactorResponse> SucessfullFactors { get; set; } = new();
        //Identificacion de los criterios de decision
        public List<DecissionCriteriaResponse> DecissionCriterias { get; set; } = new();
        //Juicio de expertos
        public List<ExpertJudgementResponse> ExpertJudgements { get; set; } = new();
    }
}
