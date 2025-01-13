using Shared.Models.FileResults.Generics.Request;

namespace Shared.Models.BudgetItems.Testings.Validators
{
    public class ValidateTestingRequest : ValidateMessageResponse, IRequest
    {
        public Guid? Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Guid DeliverableId { get; set; }
        public Guid ProjectId { get; set; }
        public string EndPointName => StaticClass.Testings.EndPoint.Validate;

        public override string Legend => Name;

        public override string ClassName => StaticClass.Testings.ClassName;
    }

}
