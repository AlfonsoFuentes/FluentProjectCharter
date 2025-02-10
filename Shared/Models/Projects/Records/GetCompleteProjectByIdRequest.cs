
using Shared.Models.FileResults.Generics.Records;
using Shared.Models.FileResults.Generics.Request;

namespace Shared.Models.Projects.Records
{
    public class GetCompleteProjectByIdRequest : GetByIdMessageResponse, IGetById
    {

        public Guid Id { get; set; }
        public string EndPointName => StaticClass.Projects.EndPoint.GetCompleteById;
        public override string ClassName => StaticClass.Projects.ClassName;
    }
}
