using Shared.Models.FileResults.Generics.Request;

namespace Shared.Models.BudgetItems
{
    public class DeleteBudgetItemGroupRequest : DeleteMessageResponse, IRequest
    {
        public string Name { get; set; } = "Delete Group BudgetItem";
        public override string Legend => Name;

        public override string ClassName => StaticClass.BudgetItems.ClassName;

        public Guid ProjectId { get; set; }
        public List<Guid> DeleteGroup { get; set; } = new();

        public string EndPointName => StaticClass.BudgetItems.EndPoint.DeleteGroup;


    }
}
