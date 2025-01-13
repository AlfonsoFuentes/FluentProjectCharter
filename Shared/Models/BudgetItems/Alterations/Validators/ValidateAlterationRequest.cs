using Shared.Models.FileResults.Generics.Request;

namespace Shared.Models.BudgetItems.Alterations.Validators
{
    public class ValidateAlterationRequest : ValidateMessageResponse, IRequest
    {
        public Guid? Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Guid DeliverableId { get; set; }
        public Guid ProjectId { get; set; }
        public string EndPointName => StaticClass.Alterations.EndPoint.Validate;

        public override string Legend => Name;

        public override string ClassName => StaticClass.Alterations.ClassName;
    }

}
