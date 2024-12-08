using Shared.Models.FileResults.Generics.Request;

namespace Shared.Models.Backgrounds.Requests
{
    public class UpdateBackGroundRequest : UpdateMessageResponse, IRequest
    {
        public Guid ProjectId { get; set; }
        public Guid CaseId { get; set; }
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public string EndPointName => StaticClass.BackGrounds.EndPoint.Update;

        public override string Legend => Name;

        public override string ClassName => StaticClass.BackGrounds.ClassName;
    }
}
