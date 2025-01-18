using Shared.Models.AcceptanceCriterias.Responses;
using Shared.Models.Assumptions.Responses;
using Shared.Models.Bennefits.Responses;
using Shared.Models.BudgetItems;
using Shared.Models.BudgetItems.Alterations.Responses;
using Shared.Models.BudgetItems.EHSs.Responses;
using Shared.Models.BudgetItems.Electricals.Responses;
using Shared.Models.BudgetItems.EngineeringDesigns.Responses;
using Shared.Models.BudgetItems.Equipments.Responses;
using Shared.Models.BudgetItems.Foundations.Responses;
using Shared.Models.BudgetItems.Instruments.Responses;
using Shared.Models.BudgetItems.Paintings.Responses;
using Shared.Models.BudgetItems.Pipes.Responses;
using Shared.Models.BudgetItems.Structurals.Responses;
using Shared.Models.BudgetItems.Taxs.Responses;
using Shared.Models.BudgetItems.Testings.Responses;
using Shared.Models.BudgetItems.Valves.Responses;
using Shared.Models.Constrainsts.Responses;
using Shared.Models.DeliverableRisks.Responses;
using Shared.Models.FileResults.Generics.Reponses;
using Shared.Models.Requirements.Responses;
namespace Shared.Models.Deliverables.Responses
{
    public class DeliverableResponse : BaseResponse, IUpdateStateResponse
    {
        public Guid ProjectId { get; set; }

        public Guid ScopeId { get; set; }

        public List<AssumptionResponse> Assumptions { get; set; } = new();
        public List<ConstrainstResponse> Constrainsts { get; set; } = new();
        public List<RequirementResponse> Requirements { get; set; } = new();
       
        public List<DeliverableRiskResponse> DeliverableRisks { get; set; } = new();
   

        public List<BennefitResponse> Bennefits { get; set; } = new();
        public List<AcceptanceCriteriaResponse> AcceptanceCriterias { get; set; } = new();
        public List<AlterationResponse> Alterations { get; set; } = new();
        public List<FoundationResponse> Foundations { get; set; } = new();
        public List<StructuralResponse> Structurals { get; set; } = new();
        public List<EquipmentResponse> Equipments { get; set; } = new();
        public List<ElectricalResponse> Electricals { get; set; } = new();
        public List<PipeResponse> Pipings { get; set; } = new();
        public List<InstrumentResponse> Instruments { get; set; } = new();
        public List<EHSResponse> EHSs { get; set; } = new();
        public List<PaintingResponse> Paintings { get; set; } = new();
        public List<TaxResponse> Taxes { get; set; } = new();
        public List<TestingResponse> Testings { get; set; } = new();
        public List<ValveResponse> Valves { get; set; } = new();
        public List<EngineeringDesignResponse> EngineeringDesigns { get; set; } = new();

        public List<IBudgetItemResponse> BudgetItems => [.. Alterations,..Foundations,..Structurals,..Equipments,..Valves,..Electricals,
            ..Pipings,..Instruments,..EHSs,..Paintings,..Taxes,..Testings,..EngineeringDesigns];


        public bool IsNodeOpen { get; set; }
        public string? Tab { get; set; } = string.Empty;
        public void Open()
        {
            IsNodeOpen = true;
        }
        public void Close()
        {
            IsNodeOpen = false;
        }

      

        public string EndPointName => StaticClass.Deliverables.EndPoint.UpdateState;
    }
}
