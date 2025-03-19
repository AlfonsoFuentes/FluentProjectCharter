using Shared.Enums.BudgetItemTypes;
using Shared.Enums.CostCenter;
using Shared.Models.BudgetItems.Responses;
using System.Globalization;

namespace Shared.Models.BudgetItems.IndividualItems.EngineeringDesigns.Responses
{
    public class EngineeringDesignResponse : BudgetItemWithPurchaseOrdersResponse
    {
    
        public Guid DeliverableId { get; set; }

        public override string UpadtePageName { get; set; } = StaticClass.EngineeringDesigns.PageName.Update;

    }
}
