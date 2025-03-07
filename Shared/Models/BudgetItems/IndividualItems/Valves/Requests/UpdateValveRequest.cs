using Shared.Enums.CostCenter;
using Shared.Enums.DiameterEnum;
using Shared.Enums.Materials;
using Shared.Enums.ValvesEnum;
using Shared.Models.Brands.Responses;
using Shared.Models.BudgetItems.IndividualItems.Nozzles.Responses;
using Shared.Models.FileResults.Generics.Request;

namespace Shared.Models.BudgetItems.IndividualItems.Valves.Requests
{
    public class UpdateValveRequest : UpdateMessageResponse, IRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Guid? GanttTaskId { get; set; }
        public Guid ProjectId { get; set; }
        public string EndPointName => StaticClass.Valves.EndPoint.Update;

        public override string Legend => Name;

        public override string ClassName => StaticClass.Valves.ClassName;
        
        public double Budget { get; set; }

         public string sBudget => string.Format(new CultureInfo("en-US"), "{0:C0}", Budget);
        public string Model { get; set; } = string.Empty;
        public MaterialEnum Material { get; set; } = MaterialEnum.None;
        public ActuatorTypeEnum ActuatorType { get; set; } = ActuatorTypeEnum.None;
        public PositionerTypeEnum PositionerType { get; set; } = PositionerTypeEnum.None;
        public bool HasFeedBack { get; set; }
        public NominalDiameterEnum Diameter { get; set; } = NominalDiameterEnum.None;
        public FailTypeEnum FailType { get; set; } = FailTypeEnum.None;
        public SignalTypeEnum SignalType { get; set; } = SignalTypeEnum.None;
       
        public ValveTypesEnum Type { get; set; } = ValveTypesEnum.None;
        public string TagNumber { get; set; } = string.Empty;
        public string Tag => $"{TagLetter}-{TagNumber}";
        public string ProvisionalTag { get; set; } = string.Empty;
        public string TagLetter => $"{PositionerType.Letter}{Type.Letter}";
        public BrandResponse BrandResponse { get; set; } = new();
        public string Brand => BrandResponse == null ? string.Empty : BrandResponse.Name;
        public List<NozzleResponse> Nozzles { get; set; } = new();
        public bool ShowDetails { get; set; } = false;
        public bool ShowProvisionalTag { get; set; } = false;
        public bool IsExisting { get; set; }
    }
}
