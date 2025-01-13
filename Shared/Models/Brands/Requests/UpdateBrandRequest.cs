using Shared.Models.FileResults.Generics.Request;

namespace Shared.Models.Brands.Requests
{
    public class UpdateBrandRequest : UpdateMessageResponse, IRequest
    {

       
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
  
        public string EndPointName => StaticClass.Brands.EndPoint.Update;

        public override string Legend => Name;

        public override string ClassName => StaticClass.Brands.ClassName;
    }
}
