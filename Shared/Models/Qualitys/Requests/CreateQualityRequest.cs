using Shared.Models.FileResults.Generics.Request;

namespace Shared.Models.Qualitys.Requests
{
    public class CreateQualityRequest : CreateMessageResponse, IRequest
    {
        
        
       
        public Guid ProjectId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string EndPointName => StaticClass.Qualitys.EndPoint.Create;
     
        public override string Legend => Name;

        public override string ClassName => StaticClass.Qualitys.ClassName;
    }
}
