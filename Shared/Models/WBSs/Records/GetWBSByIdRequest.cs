using Shared.Models.FileResults.Generics.Records;
using Shared.Models.FileResults.Generics.Request;

namespace Shared.Models.WBSs.Records
{
   public class GetWBSByIdRequest : GetByIdMessageResponse, IGetById
    {
        public Guid ProjectId { get; set; }
        public Guid Id { get; set; }
        public string EndPointName => StaticClass.WBSs.EndPoint.GetById;
        public override string ClassName => StaticClass.WBSs.ClassName;
    }

}
