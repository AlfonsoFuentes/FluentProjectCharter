using Shared.Models.FileResults.Generics.Request;

namespace Shared.Models.WBSs.Validators
{
   
    public class ValidateWBSRequest : ValidateMessageResponse, IRequest
    {
        public Guid? Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Guid CaseId { get; set; }

        public string EndPointName => StaticClass.WBSs.EndPoint.Validate;

        public override string Legend => Name;

        public override string ClassName => StaticClass.WBSs.ClassName;
    }
}
