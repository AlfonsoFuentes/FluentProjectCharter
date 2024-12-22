using Shared.Models.FileResults.Generics.Records;
using Shared.Models.FileResults.Generics.Request;

namespace Shared.Models.SucessfullFactors.Records
{
   public class GetSucessfullFactorByIdRequest : GetByIdMessageResponse, IGetById
    {
        public Guid ProjectId { get; set; }
        public Guid Id { get; set; }
        public string EndPointName => StaticClass.SucessfullFactors.EndPoint.GetById;
        public override string ClassName => StaticClass.SucessfullFactors.ClassName;
    }

}
