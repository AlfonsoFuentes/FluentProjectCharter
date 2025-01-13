using Shared.Enums.CostCenter;
using Shared.Enums.Materials;
using Shared.Models.Brands.Responses;
using Shared.Models.FileResults.Generics.Request;

namespace Shared.Models.BudgetItems.Instruments.Requests
{
    public class CreateInstrumentRequest : CreateMessageResponse, IRequest
    {
        public Guid DeliverableId { get; set; }
        public string Name { get; set; } = string.Empty;
        public Guid ProjectId { get; set; }
        public string EndPointName => StaticClass.Instruments.EndPoint.Create;

        public override string Legend => Name;

        public override string ClassName => StaticClass.Instruments.ClassName;

  
        public double Budget { get; set; }

     
        public string sBudget => string.Format(new CultureInfo("en-US"), "{0:C0}", Budget);
       
        public string SignalType { get; set; } = string.Empty;

        public string VariableInstrument { get; set; } = string.Empty;
        public string ModifierInstrument { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public MaterialEnum Material { get; set; } = MaterialEnum.None;

        public string Reference { get; set; } = string.Empty;
        public string TagNumber { get; set; } = string.Empty;
        public string TagLetter { get; set; } = string.Empty;
        public BrandResponse? Brand { get; set; } 
   
    }
}
