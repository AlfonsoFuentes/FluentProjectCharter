using Shared.Models.FileResults.Generics.Request;

namespace Shared.Models.Acquisitions.Requests
{
    public class CreateAcquisitionRequest : CreateMessageResponse, IRequest
    {
        
        
       
        public Guid ProjectId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string EndPointName => StaticClass.Acquisitions.EndPoint.Create;
     
        public override string Legend => Name;

        public override string ClassName => StaticClass.Acquisitions.ClassName;
    }
}
