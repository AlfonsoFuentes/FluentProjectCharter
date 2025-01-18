using Shared.Models.FileResults.Generics.Records;
using Shared.Models.FileResults.Generics.Request;

namespace Shared.Models.BudgetItems.Pipes.Records
{
    public class GetPipeByIdRequest : GetByIdMessageResponse, IGetById
    {
        public Guid ProjectId { get; set; }
        public Guid Id { get; set; }
        public string EndPointName => StaticClass.Pipes.EndPoint.GetById;
        public override string ClassName => StaticClass.Pipes.ClassName;
    }

}
