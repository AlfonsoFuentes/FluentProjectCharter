using Shared.Models.FileResults.Generics.Request;

namespace Shared.Models.DeliverableRisks.Validators
{
   
    public class ValidateDeliverableRiskRequest : ValidateMessageResponse, IRequest
    {
        public Guid? Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Guid ScopeId { get; set; }

        public string EndPointName => StaticClass.DeliverableRisks.EndPoint.Validate;

        public override string Legend => Name;

        public override string ClassName => StaticClass.DeliverableRisks.ClassName;
    }
}
