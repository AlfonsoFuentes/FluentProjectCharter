using Shared.Models.FileResults.Generics.Records;
using Shared.Models.FileResults.Generics.Request;

namespace Shared.Models.DeliverableRisks.Records
{
   public class GetDeliverableRiskByIdRequest : GetByIdMessageResponse, IGetById
    {
        public Guid ProjectId { get; set; }
        public Guid Id { get; set; }
        public string EndPointName => StaticClass.DeliverableRisks.EndPoint.GetById;
        public override string ClassName => StaticClass.DeliverableRisks.ClassName;
    }

}
