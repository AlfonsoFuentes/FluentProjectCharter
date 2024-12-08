using Shared.Models.FileResults.Generics.Request;

namespace Shared.Models.Backgrounds.Requests
{
    public class CreateBackGroundRequest : CreateMessageResponse, IRequest
    {

        public Guid ProjectId { get; set; }
        public Guid CaseId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string EndPointName => StaticClass.BackGrounds.EndPoint.Create;

        public override string Legend => Name;

        public override string ClassName => StaticClass.BackGrounds.ClassName;
    }
}
