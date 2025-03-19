﻿using Shared.Models.BudgetItems.IndividualItems.Alterations.Responses;
using Shared.Models.BudgetItems.IndividualItems.Contingencys.Responses;
using Shared.Models.BudgetItems.IndividualItems.EHSs.Responses;
using Shared.Models.BudgetItems.IndividualItems.Electricals.Responses;
using Shared.Models.BudgetItems.IndividualItems.EngineeringDesigns.Responses;
using Shared.Models.BudgetItems.IndividualItems.Engineerings.Responses;
using Shared.Models.BudgetItems.IndividualItems.Equipments.Responses;
using Shared.Models.BudgetItems.IndividualItems.Foundations.Responses;
using Shared.Models.BudgetItems.IndividualItems.Instruments.Responses;
using Shared.Models.BudgetItems.IndividualItems.Paintings.Responses;
using Shared.Models.BudgetItems.IndividualItems.Pipes.Responses;
using Shared.Models.BudgetItems.IndividualItems.Structurals.Responses;
using Shared.Models.BudgetItems.IndividualItems.Taxs.Responses;
using Shared.Models.BudgetItems.IndividualItems.Testings.Responses;
using Shared.Models.BudgetItems.IndividualItems.Valves.Responses;
using Shared.Models.FileResults.Generics.Reponses;

namespace Shared.Models.BudgetItems.Responses
{
    public class BudgetItemWithPurchaseOrderResponseList : IResponseAll
    {

        public string ProjectName { get; set; } = string.Empty;
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
        public List<EngineeringResponse> Engineerings { get; set; } = new();
        public List<ContingencyResponse> Contingencies { get; set; } = new();
        public List<IBudgetItemWithPurchaseOrderResponse> Expenses => [.. Alterations];
        public List<IBudgetItemWithPurchaseOrderResponse> Capital => [..Foundations,..Structurals,..Equipments,..Valves,..Electricals,
            ..Pipings,..Instruments,..EHSs,..Paintings,..Taxes,..Testings,..EngineeringDesigns,..Engineerings,..Contingencies];

        public List<IBudgetItemWithPurchaseOrderResponse> Items => BudgetItems.OrderBy(x => x.Nomenclatore).ToList();
        public List<IBudgetItemWithPurchaseOrderResponse> BudgetItems => [.. Expenses, .. Capital];
        public double TotalCapital => Capital.Sum(x => x.BudgetUSD) + TaxesBudget;

        public double TotalCapitalWithOutVAT => Capital.Sum(x => x.BudgetUSD);
        public double TotalExpenses => Expenses.Sum(x => x.BudgetUSD);
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

        public double CapitalActualUSD=> Capital.Sum(x=>x.ActualUSD);
        public double CapitalCommitemntUSD => Capital.Sum(x => x.CommitmentUSD);
        public double CapitalPotentialUSD => Capital.Sum(x => x.PotentialUSD);
        public double CapitalAssignedUSD=>CapitalActualUSD+CapitalCommitemntUSD+CapitalPotentialUSD;
        public double CapitalToCommitUSD => TotalCapital - CapitalAssignedUSD;

        public double ExpensesActualUSD => Expenses.Sum(x => x.ActualUSD);
        public double ExpensesCommitemntUSD => Expenses.Sum(x => x.CommitmentUSD);
        public double ExpensesPotentialUSD => Expenses.Sum(x => x.PotentialUSD);
        public double ExpensesAssignedUSD => ExpensesActualUSD + ExpensesCommitemntUSD + ExpensesPotentialUSD;
        public double ExpensesToCommitUSD => TotalExpenses - ExpensesAssignedUSD;

        public string sCapitalActualUSD => string.Format(new CultureInfo("en-US"), "{0:C0}", CapitalActualUSD);
        public string sCapitalCommitemntUSD => string.Format(new CultureInfo("en-US"), "{0:C0}", CapitalCommitemntUSD);
        public string sCapitalPotentialUSD => string.Format(new CultureInfo("en-US"), "{0:C0}", CapitalPotentialUSD);
        public string sCapitalAssignedUSD => string.Format(new CultureInfo("en-US"), "{0:C0}", CapitalAssignedUSD);
        public string sCapitalToCommitUSD => string.Format(new CultureInfo("en-US"), "{0:C0}", CapitalToCommitUSD);
        public string sExpensesActualUSD => string.Format(new CultureInfo("en-US"), "{0:C0}", ExpensesActualUSD);
        public string sExpensesCommitemntUSD => string.Format(new CultureInfo("en-US"), "{0:C0}", ExpensesCommitemntUSD);
        public string sExpensesPotentialUSD => string.Format(new CultureInfo("en-US"), "{0:C0}", ExpensesPotentialUSD);
        public string sExpensesAssignedUSD => string.Format(new CultureInfo("en-US"), "{0:C0}", ExpensesAssignedUSD);
        public string sExpensesToCommitUSD => string.Format(new CultureInfo("en-US"), "{0:C0}", ExpensesToCommitUSD);

    }
}
