using Shared.Models.FileResults.Generics.Records;
using Shared.Models.FileResults.Generics.Request;

namespace Shared.Models.Temporarys.Records
{
   public class GetTemporaryByIdRequest : GetByIdMessageResponse, IGetById
    {
      
        public Guid Id { get; set; }
        public string EndPointName => StaticClass.Temporarys.EndPoint.GetById;
        public override string ClassName => StaticClass.Temporarys.ClassName;
    }

}
