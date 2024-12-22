using Shared.Models.FileResults.Generics.Records;
using Shared.Models.FileResults.Generics.Request;

namespace Shared.Models.Cases.Records
{
   public class GetCaseByIdRequest : GetByIdMessageResponse, IGetById
    {

        public Guid Id { get; set; }
        public string EndPointName => StaticClass.Cases.EndPoint.GetById;
        public override string ClassName => StaticClass.Cases.ClassName;
    }

}
