using Shared.Models.FileResults.Generics.Records;
using Shared.Models.FileResults.Generics.Request;

namespace Shared.Models.Deliverables.Records
{
   public class GetDeliverableByIdRequest : GetByIdMessageResponse, IGetById
    {
        public Guid ProjectId { get; set; }
        public Guid Id { get; set; }
        public string EndPointName => StaticClass.Deliverables.EndPoint.GetById;
        public override string ClassName => StaticClass.Deliverables.ClassName;
    }

}
