using Shared.Models.FileResults.Generics.Records;
using Shared.Models.FileResults.Generics.Request;

namespace Shared.Models.BudgetItems.Pipings.Records
{
    public class GetPipingByIdRequest : GetByIdMessageResponse, IGetById
    {
        public Guid ProjectId { get; set; }
        public Guid Id { get; set; }
        public string EndPointName => StaticClass.Pipings.EndPoint.GetById;
        public override string ClassName => StaticClass.Pipings.ClassName;
    }

}
