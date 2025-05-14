using Shared.Models.BudgetItems.Exports;
using Shared.Models.BudgetItems.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models.BudgetItems.Mappers
{
    public static class BudgetItemMapper
    {
        public static BudgetItemWithPurchaseOrdersResponse Map(this BudgetItemWithPurchaseOrdersResponse budgetItem)
        {
            return new BudgetItemWithPurchaseOrdersResponse
            {
                Id = budgetItem.Id,
                Name = budgetItem.Name,
                Tag = budgetItem.Tag,
                Nomenclatore = budgetItem.Nomenclatore,
                ProjectId = budgetItem.ProjectId,
                ActualUSD = budgetItem.ActualUSD,
                CommitmentUSD = budgetItem.CommitmentUSD,
                PotentialUSD = budgetItem.PotentialUSD,
                BudgetUSD = budgetItem.BudgetUSD,
                IsAlteration = budgetItem.IsAlteration,
                IsTaxes = budgetItem.IsTaxes,
                ShowDetails = budgetItem.ShowDetails,
            };
        }
        public static BudgetItemExport MapToExport(this IBudgetItemResponse budgetItem)
        {
            return new BudgetItemExport(budgetItem.Nomenclatore, budgetItem.Name, budgetItem.BudgetUSD);
        }
        public static BudgetItemWithPurchaseordersExport MapToExportPO(this BudgetItemWithPurchaseOrdersResponse budgetItem)
        {
            return new BudgetItemWithPurchaseordersExport(budgetItem.Nomenclatore, budgetItem.Name, budgetItem.BudgetUSD, budgetItem.ActualUSD, budgetItem.CommitmentUSD,
                budgetItem.PotentialUSD, budgetItem.ToCommitUSD);
        }
    }
}
