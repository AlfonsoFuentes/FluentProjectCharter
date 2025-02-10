using Shared.Models.FileResults.Generics.Request;

namespace Shared.Models.BudgetItems
{
    public class DeleteBudgetItemRequest : DeleteMessageResponse, IRequest
    {
        public string Name { get; set; } = string.Empty;
        public override string Legend => Name;

        public override string ClassName => StaticClass.BudgetItems.ClassName;

        public Guid Id { get; set; }

        public string EndPointName => StaticClass.BudgetItems.EndPoint.Delete;


    }
}
