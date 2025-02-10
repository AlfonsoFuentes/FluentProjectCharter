using Shared.Enums.CostCenter;
using Shared.Models.FileResults.Generics.Request;

namespace Shared.Models.BudgetItems.IndividualItems.EngineeringDesigns.Requests
{
    public class CreateEngineeringDesignRequest : CreateMessageResponse, IRequest
    {

        public Guid ProjectId { get; set; }
        public string Name { get; set; } = string.Empty;

        public string EndPointName => StaticClass.EngineeringDesigns.EndPoint.Create;

        public override string Legend => Name;

        public override string ClassName => StaticClass.EngineeringDesigns.ClassName;

        public CostCenterEnum CostCenter { get; set; } = CostCenterEnum.None;

        public double Budget { get; set; }


        public string sBudget => string.Format(new CultureInfo("en-US"), "{0:C0}", Budget);
    }
}
