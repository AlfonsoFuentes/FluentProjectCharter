using Shared.Models.FileResults.Generics.Request;

namespace Shared.Models.SucessfullFactors.Validators
{
   
    public class ValidateSucessfullFactorRequest : ValidateMessageResponse, IRequest
    {
        public Guid? Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Guid CaseId { get; set; }

        public string EndPointName => StaticClass.SucessfullFactors.EndPoint.Validate;

        public override string Legend => Name;

        public override string ClassName => StaticClass.SucessfullFactors.ClassName;
    }
}
