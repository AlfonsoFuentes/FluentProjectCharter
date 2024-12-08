using Shared.Models.FileResults.Generics.Request;

namespace Shared.Models.DecissionCriterias.Validators
{
    public class ValidateDecissionCriteriaRequest : ValidateMessageResponse, IRequest
    {
        public Guid? Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Guid CaseId { get; set; }

        public string EndPointName => StaticClass.DecissionCriterias.EndPoint.Validate;

        public override string Legend => Name;

        public override string ClassName => StaticClass.DecissionCriterias.ClassName;
    }

}
