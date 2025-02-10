using Shared.Models.FileResults.Generics.Request;

namespace Shared.Models.Communications.Requests
{
    public class UpdateCommunicationRequest : UpdateMessageResponse, IRequest
    {
        public Guid ProjectId { get; set; }
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public string EndPointName => StaticClass.Communications.EndPoint.Update;
     
        public override string Legend => Name;

        public override string ClassName => StaticClass.Communications.ClassName;
    }
}
