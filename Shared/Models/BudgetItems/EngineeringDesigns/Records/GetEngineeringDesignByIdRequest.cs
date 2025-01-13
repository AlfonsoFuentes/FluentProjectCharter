using Shared.Models.FileResults.Generics.Records;
using Shared.Models.FileResults.Generics.Request;

namespace Shared.Models.BudgetItems.EngineeringDesigns.Records
{
    public class GetEngineeringDesignByIdRequest : GetByIdMessageResponse, IGetById
    {
        public Guid ProjectId { get; set; }
        public Guid Id { get; set; }
        public string EndPointName => StaticClass.EngineeringDesigns.EndPoint.GetById;
        public override string ClassName => StaticClass.EngineeringDesigns.ClassName;
    }

}
