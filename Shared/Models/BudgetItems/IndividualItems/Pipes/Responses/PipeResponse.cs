using Shared.Enums.DiameterEnum;
using Shared.Enums.Materials;
using Shared.Models.BudgetItems.Responses;
using Shared.Models.EngineeringFluidCodes.Responses;
using Shared.Models.FileResults.Generics.Request;
using Shared.Models.Templates.Pipings.Responses;

namespace Shared.Models.BudgetItems.IndividualItems.Pipes.Responses
{
    public class PipeResponse : BudgetItemWithPurchaseOrdersResponse, IMessageResponse, IRequest
    {

        public string EndPointName => StaticClass.Pipes.EndPoint.CreateUpdate;

        public string Legend => Name;
        public PipeTemplateResponse Template { get; set; } = new();
        public string ActionType => Id == Guid.Empty ? "created" : "updated";
        public string ClassName => StaticClass.Pipes.ClassName;
        public string Succesfully => StaticClass.ResponseMessages.ReponseSuccesfullyMessage(Legend, ClassName, ActionType);
        public string Fail => StaticClass.ResponseMessages.ReponseFailMessage(Legend, ClassName, ActionType);
        public string NotFound => StaticClass.ResponseMessages.ReponseNotFound(ClassName);
        public Guid? GanttTaskId { get; set; }
        public double BudgetCalculated => MaterialQuantity * EquivalentLenghPrice + LaborDayPrice * LaborQuantity;
        public string TagNumber { get; set; } = string.Empty;
        public override string Tag => $"{Template.Diameter.Name}-{FluidCodeCode}-{TagNumber}-{Template.Material.Name}-{InsulationCode}";
      

        double materialQuantity;

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
            get { return Template.LaborDayPrice; }
            set
            {
                Template.LaborDayPrice = value;
                if (ShowDetails)
                    BudgetUSD = BudgetCalculated;
            }
        }
        public double EquivalentLenghPrice
        {
            get { return Template.EquivalentLenghPrice; }
            set
            {
                Template.EquivalentLenghPrice = value;
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
       
        public EngineeringFluidCodeResponse? FluidCode { get; set; }
        public string FluidCodeCode => FluidCode != null ? FluidCode.Code : string.Empty;


        public string InsulationCode => Template.Insulation ? "1" : "0";

        public bool IsExisting { get; set; }



    }
}
