using Shared.Models.FileResults.Generics.Request;

namespace Shared.Models.BudgetItems.IndividualItems.Nozzles.Validators
{
    public class ValidateNozzleRequest : ValidateMessageResponse, IRequest
    {
        public Guid? Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Guid EngineeringItemId { get; set; }

        public string EndPointName => StaticClass.Nozzles.EndPoint.Validate;

        public override string Legend => Name;

        public override string ClassName => StaticClass.Nozzles.ClassName;
    }


}
