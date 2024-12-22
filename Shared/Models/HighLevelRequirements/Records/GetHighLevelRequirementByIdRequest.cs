using Shared.Models.FileResults.Generics.Records;
using Shared.Models.FileResults.Generics.Request;

namespace Shared.Models.HighLevelRequirements.Records
{
   public class GetHighLevelRequirementByIdRequest : GetByIdMessageResponse, IGetById
    {

        public Guid Id { get; set; }
        public string EndPointName => StaticClass.HighLevelRequirements.EndPoint.GetById;
        public override string ClassName => StaticClass.HighLevelRequirements.ClassName;
    }

}
