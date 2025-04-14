using Shared.Enums.BudgetItemTypes;
using Shared.Enums.ConnectionTypes;
using Shared.Enums.CostCenter;
using Shared.Enums.DiameterEnum;
using Shared.Enums.Materials;
using Shared.Enums.ValvesEnum;
using Shared.Models.Brands.Responses;
using Shared.Models.BudgetItems.IndividualItems.Nozzles.Responses;
using Shared.Models.BudgetItems.Responses;
using Shared.Models.FileResults.Generics.Request;
using System.Globalization;

namespace Shared.Models.BudgetItems.IndividualItems.Valves.Responses
{
    public class ValveResponse : BudgetItemWithPurchaseOrdersResponse, IMessageResponse, IRequest
    {


        public string EndPointName => StaticClass.Valves.EndPoint.CreateUpdate;

        public string Legend => Name;

        public string ActionType => Id == Guid.Empty ? "created" : "updated";
        public string ClassName => StaticClass.Valves.ClassName;
        public string Succesfully => StaticClass.ResponseMessages.ReponseSuccesfullyMessage(Legend, ClassName, ActionType);
        public string Fail => StaticClass.ResponseMessages.ReponseFailMessage(Legend, ClassName, ActionType);
        public string NotFound => StaticClass.ResponseMessages.ReponseNotFound(ClassName);

        public Guid? GanttTaskId { get; set; }


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
        public override string Tag => ShowProvisionalTag ? ProvisionalTag :$"{TagLetter}-{TagNumber}";



        public string TagLetter { get; set; } = string.Empty;
        public BrandResponse? Brand { get; set; } 
        public string BrandName => Brand == null ? string.Empty : Brand.Name;
        public Guid? BrandId => Brand == null ? null : Brand.Id;
        public List<NozzleResponse> Nozzles { get; set; } = new();
 
        public bool IsExisting { get; set; }
        public string ProvisionalTag {  get; set; } = string.Empty;
        public bool ShowProvisionalTag { get; set; } = false;

       


    
    }
}
