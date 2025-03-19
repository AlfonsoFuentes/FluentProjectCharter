using Shared.Enums.BudgetItemTypes;
using Shared.Enums.CostCenter;
using Shared.Models.BudgetItems.Responses;
using System.Globalization;

namespace Shared.Models.BudgetItems.IndividualItems.Engineerings.Responses
{
    public class EngineeringResponse : BudgetItemWithPurchaseOrdersResponse
    {
   
        public Guid DeliverableId { get; set; }
    

        public override string UpadtePageName { get; set; } = StaticClass.Paintings.PageName.Update;


    }
}
