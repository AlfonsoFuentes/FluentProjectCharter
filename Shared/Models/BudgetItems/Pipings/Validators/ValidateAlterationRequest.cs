using Shared.Models.FileResults.Generics.Request;

namespace Shared.Models.BudgetItems.Pipings.Validators
{
    public class ValidatePipingRequest : ValidateMessageResponse, IRequest
    {
        public Guid? Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Guid DeliverableId { get; set; }
        public Guid ProjectId { get; set; }
        public string EndPointName => StaticClass.Pipings.EndPoint.Validate;

        public override string Legend => Name;

        public override string ClassName => StaticClass.Pipings.ClassName;
    }

}
