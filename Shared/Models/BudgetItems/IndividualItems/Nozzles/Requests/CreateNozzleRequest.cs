using Shared.Enums.ConnectionTypes;
using Shared.Enums.CostCenter;
using Shared.Enums.DiameterEnum;
using Shared.Enums.NozzleTypes;
using Shared.Models.FileResults.Generics.Request;
using UnitSystem;

namespace Shared.Models.BudgetItems.IndividualItems.Nozzles.Requests
{
    public class CreateNozzleRequest : CreateMessageResponse, IRequest
    {

        public string Name { get; set; } = string.Empty;
        public Guid EngineeringItemId { get; set; }
        public string EndPointName => StaticClass.Nozzles.EndPoint.Create;

        public override string Legend => Name;

        public override string ClassName => StaticClass.Nozzles.ClassName;

        public ConnectionTypeEnum ConnectionType { get; set; } = ConnectionTypeEnum.None;
        public NominalDiameterEnum NominalDiameter { get; set; } = NominalDiameterEnum.None;
        public NozzleTypeEnum NozzleType { get; set; } = NozzleTypeEnum.None;

        public Length OuterDiameter = new Length(LengthUnits.Inch);
        public Length Thickness = new Length(LengthUnits.Inch);
        public Length InnerDiameter = new Length(LengthUnits.Inch);
        public Length Height = new Length(LengthUnits.MilliMeter);

        public int Order { get; set; }
    }
}
