using Shared.Models.FileResults.Generics.Request;

namespace Shared.Models.BudgetItems.Structurals.Requests
{
    public class DeleteStructuralRequest : DeleteMessageResponse, IRequest
    {
        public string Name { get; set; } = string.Empty;
        public override string Legend => Name;
        public Guid ProjectId { get; set; }
        public override string ClassName => StaticClass.Structurals.ClassName;
        public Guid? DeliverableId { get; set; }
        public Guid Id { get; set; }

        public string EndPointName => StaticClass.Structurals.EndPoint.Delete;


    }
}
