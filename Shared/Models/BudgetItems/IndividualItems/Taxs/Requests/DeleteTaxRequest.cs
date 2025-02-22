using Shared.Models.FileResults.Generics.Request;

namespace Shared.Models.BudgetItems.Taxs.Requests
{
    public class DeleteTaxRequest : DeleteMessageResponse, IRequest
    {
        public string Name { get; set; } = string.Empty;
        public override string Legend => Name;
        public Guid ProjectId { get; set; }
        public override string ClassName => StaticClass.Taxs.ClassName;
        public Guid? DeliverableId { get; set; }
        public Guid Id { get; set; }

        public string EndPointName => StaticClass.Taxs.EndPoint.Delete;


    }
}
