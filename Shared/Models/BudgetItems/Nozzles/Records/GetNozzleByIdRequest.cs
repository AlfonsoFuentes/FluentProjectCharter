using Shared.Models.FileResults.Generics.Records;
using Shared.Models.FileResults.Generics.Request;

namespace Shared.Models.BudgetItems.Nozzles.Records
{
    public class GetNozzleByIdRequest : GetByIdMessageResponse, IGetById
    {
        public Guid ProjectId { get; set; }
        public Guid Id { get; set; }
        public string EndPointName => StaticClass.Nozzles.EndPoint.GetById;
        public override string ClassName => StaticClass.Nozzles.ClassName;
    }

}
