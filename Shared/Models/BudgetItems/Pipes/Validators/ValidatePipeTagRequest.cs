using Shared.Models.FileResults.Generics.Request;

namespace Shared.Models.BudgetItems.Pipes.Validators
{
    public class ValidatePipeTagRequest : ValidateMessageResponse, IRequest
    {
        public Guid? Id { get; set; }
        public string Name { get; set; } = string.Empty;
      
        public string Tag { get; set; }= string.Empty;
        public Guid ProjectId { get; set; }
        public string EndPointName => StaticClass.Pipes.EndPoint.ValidateTag;

        public override string Legend => Name;

        public override string ClassName => StaticClass.Pipes.ClassName;
    }

}
