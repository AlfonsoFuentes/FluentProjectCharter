using Shared.Models.FileResults.Generics.Request;

namespace Shared.Models.BudgetItems.Pipings.Requests
{
    public class DeletePipingRequest : DeleteMessageResponse, IRequest
    {
        public string Name { get; set; } = string.Empty;
        public override string Legend => Name;
        public Guid ProjectId { get; set; }
        public override string ClassName => StaticClass.Pipings.ClassName;

        public Guid Id { get; set; }

        public string EndPointName => StaticClass.Pipings.EndPoint.Delete;


    }
}
