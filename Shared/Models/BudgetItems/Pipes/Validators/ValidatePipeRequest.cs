using Shared.Models.FileResults.Generics.Request;

namespace Shared.Models.BudgetItems.Pipes.Validators
{
    public class ValidatePipeRequest : ValidateMessageResponse, IRequest
    {
        public Guid? Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Guid DeliverableId { get; set; }
        public Guid ProjectId { get; set; }
        public string EndPointName => StaticClass.Pipes.EndPoint.Validate;

        public override string Legend => Name;

        public override string ClassName => StaticClass.Pipes.ClassName;
    }

}
