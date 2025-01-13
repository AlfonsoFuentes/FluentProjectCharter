using Shared.Enums.BudgetItemTypes;
using Shared.Enums.CostCenter;
using Shared.Enums.Materials;
using Shared.Models.Brands.Responses;
using Shared.Models.BudgetItems;
using System.Globalization;

namespace Shared.Models.BudgetItems.Instruments.Responses
{
    public class InstrumentResponse : BaseResponse, IBudgetItemResponse
    {

        public Guid DeliverableId { get; set; }
        public Guid ProjectId { get; set; }

 
        public double Budget { get; set; }

       
        public string sBudget => string.Format(new CultureInfo("en-US"), "{0:C0}", Budget);

        public string Nomenclatore { get; set; } = string.Empty;
        public  BudgetItemTypeEnum BudgetItemTypeEnum { get; set; }= BudgetItemTypeEnum.Instruments;
        public string UpadtePageName { get; set; } = StaticClass.Instruments.PageName.Update;

        public string SignalType { get; set; } = string.Empty;

        public string VariableInstrument { get; set; } = string.Empty;
        public string ModifierInstrument { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public MaterialEnum Material { get; set; } = MaterialEnum.None;

        public string Reference { get; set; } = string.Empty;
        public string TagNumber { get; set; } = string.Empty;
        public string TagLetter { get; set; } = string.Empty;
        public BrandResponse? Brand { get; set; } 
  
        public string Tag { get; set; } = string.Empty;
    }
}
