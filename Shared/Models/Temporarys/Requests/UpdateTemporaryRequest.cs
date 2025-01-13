using Shared.Models.FileResults.Generics.Request;

namespace Shared.Models.Temporarys.Requests
{
    public class UpdateTemporaryRequest : UpdateMessageResponse, IRequest
    {
     

        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
       
        public string EndPointName => StaticClass.Temporarys.EndPoint.Update;

        public override string Legend => Name;

        public override string ClassName => StaticClass.Temporarys.ClassName;
    }
}
