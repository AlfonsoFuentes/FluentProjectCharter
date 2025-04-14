using Shared.Enums.DiameterEnum;
using Shared.Enums.Materials;
using Shared.Models.Brands.Responses;
using Shared.Models.BudgetItems.IndividualItems.Nozzles.Responses;
using Shared.Models.BudgetItems.Responses;
using Shared.Models.EngineeringFluidCodes.Responses;
using Shared.Models.FileResults.Generics.Request;

namespace Shared.Models.BudgetItems.IndividualItems.Pipes.Responses
{
    public class PipeResponse : BudgetItemWithPurchaseOrdersResponse, IMessageResponse, IRequest
    {


        public string EndPointName => StaticClass.Pipes.EndPoint.CreateUpdate;

        public string Legend => Name;

        public string ActionType => Id == Guid.Empty ? "created" : "updated";
        public string ClassName => StaticClass.Pipes.ClassName;
        public string Succesfully => StaticClass.ResponseMessages.ReponseSuccesfullyMessage(Legend, ClassName, ActionType);
        public string Fail => StaticClass.ResponseMessages.ReponseFailMessage(Legend, ClassName, ActionType);
        public string NotFound => StaticClass.ResponseMessages.ReponseNotFound(ClassName);
        public Guid? GanttTaskId { get; set; }
        public double BudgetCalculated => MaterialQuantity * EquivalentLenghPrice + LaborDayPrice * LaborQuantity;
        public string TagNumber { get; set; } = string.Empty;
        public override string Tag => $"{Diameter.Name}-{FluidCodeCode}-{TagNumber}-{Material.Name}-{InsulationCode}";
        double materialQuantity;
        double laborDayPrice;
        double equivalentLenghPrice;
        double laborQuantity;
        public double MaterialQuantity
        {
            get
            {
                return materialQuantity;
            }
            set
            {
                materialQuantity = value;
                if (ShowDetails)
                    BudgetUSD = BudgetCalculated;
            }
        }
        public double LaborDayPrice
        {
            get { return laborDayPrice; }
            set
            {
                laborDayPrice = value;
                if (ShowDetails)
                    BudgetUSD = BudgetCalculated;
            }
        }
        public double EquivalentLenghPrice
        {
            get { return equivalentLenghPrice; }
            set
            {
                equivalentLenghPrice = value;
                if (ShowDetails)
                    BudgetUSD = BudgetCalculated;
            }
        }
        public double LaborQuantity
        {
            get { return laborQuantity; }
            set
            {
                laborQuantity = value;
                if (ShowDetails)
                    BudgetUSD = BudgetCalculated;
            }
        }
        public NominalDiameterEnum Diameter { get; set; } = NominalDiameterEnum.None;
        public EngineeringFluidCodeResponse? FluidCode { get; set; }
        public string FluidCodeCode => FluidCode != null ? FluidCode.Code : string.Empty;
        public MaterialEnum Material { get; set; } = MaterialEnum.None;
        public bool Insulation { get; set; }
        public string InsulationCode => Insulation ? "1" : "0";
        public PipeClassEnum PipeClass { get; set; } = PipeClassEnum.None;
        public bool IsExisting { get; set; }



    }
}
