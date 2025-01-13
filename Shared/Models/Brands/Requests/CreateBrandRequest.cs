using Shared.Models.FileResults.Generics.Request;

namespace Shared.Models.Brands.Requests
{
    public class CreateBrandRequest : CreateMessageResponse, IRequest
    {
       
        
        public string Name { set; get; } = string.Empty;
        public string EndPointName => StaticClass.Brands.EndPoint.Create;
 
        public override string Legend => Name;

        public override string ClassName => StaticClass.Brands.ClassName;
    }
}
