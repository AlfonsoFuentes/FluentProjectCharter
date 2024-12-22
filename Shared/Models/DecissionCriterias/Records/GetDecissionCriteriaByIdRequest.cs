using Shared.Models.FileResults.Generics.Records;
using Shared.Models.FileResults.Generics.Request;

namespace Shared.Models.DecissionCriterias.Records
{
   public class GetDecissionCriteriaByIdRequest : GetByIdMessageResponse, IGetById
    {
        public Guid ProjectId { get; set; }
        public Guid Id { get; set; }
        public string EndPointName => StaticClass.DecissionCriterias.EndPoint.GetById;
        public override string ClassName => StaticClass.DecissionCriterias.ClassName;
    }

}
