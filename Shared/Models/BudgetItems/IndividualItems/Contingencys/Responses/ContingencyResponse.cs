using Shared.Models.BudgetItems.Responses;

namespace Shared.Models.BudgetItems.IndividualItems.Contingencys.Responses
{
    public class ContingencyResponse : BudgetItemWithPurchaseOrdersResponse
    {

        public Guid DeliverableId { get; set; }
        

        public override string UpadtePageName { get; set; } = StaticClass.Paintings.PageName.Update;
        

    }
}
