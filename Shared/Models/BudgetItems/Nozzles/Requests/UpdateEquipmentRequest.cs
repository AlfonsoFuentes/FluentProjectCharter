using Shared.Enums.ConnectionTypes;
using Shared.Enums.DiameterEnum;
using Shared.Enums.NozzleTypes;
using Shared.Models.FileResults.Generics.Request;

namespace Shared.Models.BudgetItems.Nozzles.Requests
{
    public class UpdateNozzleRequest : UpdateMessageResponse, IRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        
        public string EndPointName => StaticClass.Nozzles.EndPoint.Update;

        public override string Legend => Name;

        public override string ClassName => StaticClass.Nozzles.ClassName;

        public ConnectionTypeEnum ConnectionType { get; set; } = ConnectionTypeEnum.None;
        public NominalDiameterEnum NominalDiameter { get; set; } = NominalDiameterEnum.None;
        public NozzleTypeEnum NozzleType { get; set; } = NozzleTypeEnum.None;

        public Length OuterDiameter { get; set; } = new Length(LengthUnits.Inch);
        public Length Thickness { get; set; } = new Length(LengthUnits.Inch);
        public Length InnerDiameter { get; set; } = new Length(LengthUnits.Inch);
        public Length Height { get; set; } = new Length(LengthUnits.MilliMeter);

        public int Order { get; set; }
    }
}
