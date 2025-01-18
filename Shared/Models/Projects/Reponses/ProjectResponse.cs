using Shared.Enums.ProjectNeedTypes;
using Shared.Models.Assumptions.Responses;
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
using Shared.Models.Cases.Responses;
using Shared.Models.Constrainsts.Responses;
using Shared.Models.FileResults.Generics.Reponses;
using Shared.Models.HighLevelRequirements.Responses;
using Shared.Models.Meetings.Responses;
using Shared.Models.Requirements.Responses;
using Shared.Models.StakeHolders.Responses;

namespace Shared.Models.Projects.Reponses
{
    public class ProjectResponse : BaseResponse, IUpdateStateResponse
    {
        public string EndPointName => StaticClass.Projects.EndPoint.UpdateState;
        public string ProjectId => Id.ToString();
        public ProjectNeedTypeEnum ProjectNeedType { get; set; } = ProjectNeedTypeEnum.None;

        public double InitialBudget { get; set; }
        public string ProjectDescription { get; set; } = string.Empty;
        public StakeHolderResponse Manager { get; set; } = null!;
        public StakeHolderResponse Sponsor { get; set; } = null!;
        public DateTime? InitialProjectDate { get; set; } = DateTime.Today;
        public List<HighLevelRequirementResponse> HighLevelRequirements { get; set; } = new();
        public List<CaseResponse> Cases { get; set; } = new();
        public List<StakeHolderInsideProjectResponse> StakeHolders { get; set; } = new();
        public List<MeetingResponse> Meetings { get; set; } = new();
        public List<AssumptionResponse> Assumptions { get; set; } = new();
        public List<ConstrainstResponse> Constrainsts { get; set; } = new();
        public List<RequirementResponse> Requirements { get; set; } = new();
        public string ProjectNumber { get; set; } = string.Empty;
        public ProjectStatusEnum Status { get; set; } = ProjectStatusEnum.None;
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
        public List<IBudgetItemResponse> Expenses => [.. Alterations];
        public List<IBudgetItemResponse> Capital => [..Foundations,..Structurals,..Equipments,..Valves,..Electricals,
            ..Pipings,..Instruments,..EHSs,..Paintings,..Taxes,..Testings,..EngineeringDesigns];

        public List<IBudgetItemResponse> BudgetItems => [.. Expenses, .. Capital];
        public double TotalCapital => Capital.Sum(x => x.Budget) + TaxesBudget;

        public double TotalCapitalWithOutVAT => Capital.Sum(x => x.Budget);
        public double TotalExpenses => Expenses.Sum(x => x.Budget);
        public double PercentageEngineering { get; set; }
        public double PercentageContingency { get; set; }
        public double PercentageTaxes { get; set; }
        public bool IsProductive { get; set; } = true;

        public double EngineeringBudget => TotalCapital * PercentageEngineering / 100;
        public double ContingenyBudget => TotalCapital * PercentageContingency / 100;
        public double TaxesBudget => IsProductive ? 0 : TotalCapitalWithOutVAT * PercentageTaxes / 100;
        public double TotalBudget => TotalCapital + TotalExpenses + EngineeringBudget + ContingenyBudget;

        public string sTotalCapital => string.Format(new CultureInfo("en-US"), "{0:C0}", TotalCapital);
        public string sTotalExpenses => string.Format(new CultureInfo("en-US"), "{0:C0}", TotalExpenses);
        public string sEngineeringBudget => string.Format(new CultureInfo("en-US"), "{0:C0}", EngineeringBudget);
        public string sContingenyBudget => string.Format(new CultureInfo("en-US"), "{0:C0}", ContingenyBudget);
        public string sTotalBudget => string.Format(new CultureInfo("en-US"), "{0:C0}", TotalBudget);
        public string sTaxesBudget => string.Format(new CultureInfo("en-US"), "{0:C0}", TaxesBudget);
        public string sTotalCapitalWithOutVAT => string.Format(new CultureInfo("en-US"), "{0:C0}", TotalCapitalWithOutVAT);

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
        public CaseResponse? CurrentCase { get; set; }
        public MeetingResponse? CurrentMeeting { get; set; }

    }
}
