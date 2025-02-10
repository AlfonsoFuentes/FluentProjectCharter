using Shared.Models.FileResults.Generics.Request;

namespace Shared.Models.Acquisitions.Requests
{
    public class UpdateAcquisitionRequest : UpdateMessageResponse, IRequest
    {
        public Guid ProjectId { get; set; }
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public string EndPointName => StaticClass.Acquisitions.EndPoint.Update;
     
        public override string Legend => Name;

        public override string ClassName => StaticClass.Acquisitions.ClassName;
    }
}
