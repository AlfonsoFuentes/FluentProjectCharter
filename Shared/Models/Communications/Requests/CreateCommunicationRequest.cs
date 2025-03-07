using Shared.Models.FileResults.Generics.Request;

namespace Shared.Models.Communications.Requests
{
    public class CreateCommunicationRequest : CreateMessageResponse, IRequest
    {
        
        
       
        public Guid ProjectId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string EndPointName => StaticClass.Communications.EndPoint.Create;
     
        public override string Legend => Name;

        public override string ClassName => StaticClass.Communications.ClassName;
    }
}
