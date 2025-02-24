using Shared.Models.FileResults.Generics.Records;

namespace Shared.Models.BudgetItems.Records
{
    public class BudgetItemGetAllByDeliverable : IGetAll
    {

        public string EndPointName => StaticClass.BudgetItems.EndPoint.GetAll;
        public Guid DeliverableId { get; set; }

    }
}
