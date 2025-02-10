using Shared.Models.FileResults.Generics.Request;

namespace Shared.Models.BudgetItems.IndividualItems.Nozzles.Requests
{
    public class DeleteNozzleRequest : DeleteMessageResponse, IRequest
    {
        public string Name { get; set; } = string.Empty;
        public override string Legend => Name;

        public override string ClassName => StaticClass.Nozzles.ClassName;

        public Guid Id { get; set; }

        public string EndPointName => StaticClass.Nozzles.EndPoint.Delete;


    }
}
