using Shared.Models.FileResults.Generics.Records;
using Shared.Models.FileResults.Generics.Request;

namespace Shared.Models.Deliverables.Records
{
    public class GetAllDeliverableRequest : GetByIdMessageResponse, IGetAll
    {
        public string EndPointName => StaticClass.Deliverables.EndPoint.GetAll;
        public Guid DeliverableId { get; set; }
        public Guid ProjectId { get; set; }

        public override string ClassName => StaticClass.Deliverables.ClassName;
    }
}
