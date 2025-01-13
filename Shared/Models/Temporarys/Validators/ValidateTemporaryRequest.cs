using Shared.Models.FileResults.Generics.Request;

namespace Shared.Models.Temporarys.Validators
{
   
    public class ValidateTemporaryRequest : ValidateMessageResponse, IRequest
    {
        public Guid? Id { get; set; }
        public string Name { get; set; } = string.Empty;
      

        public string EndPointName => StaticClass.Temporarys.EndPoint.Validate;

        public override string Legend => Name;

        public override string ClassName => StaticClass.Temporarys.ClassName;
    }
}
