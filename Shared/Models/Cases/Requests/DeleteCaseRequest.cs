using Shared.Models.FileResults.Generics.Request;

namespace Shared.Models.Cases.Requests
{
    public class DeleteCaseRequest : DeleteMessageResponse, IRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public override string Legend => Name;
        public Guid ProjectId { get; set; }
        public override string ClassName => StaticClass.Cases.ClassName;



        public string EndPointName => StaticClass.Cases.EndPoint.Delete;
    }
}
