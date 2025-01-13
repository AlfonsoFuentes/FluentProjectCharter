using Shared.Models.FileResults.Generics.Records;
using Shared.Models.FileResults.Generics.Request;

namespace Shared.Models.BudgetItems.Structurals.Records
{
    public class GetStructuralByIdRequest : GetByIdMessageResponse, IGetById
    {
        public Guid ProjectId { get; set; }
        public Guid Id { get; set; }
        public string EndPointName => StaticClass.Structurals.EndPoint.GetById;
        public override string ClassName => StaticClass.Structurals.ClassName;
    }

}
