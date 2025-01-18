using Shared.Enums.BudgetItemTypes;
using Shared.Enums.CostCenter;
using Shared.Enums.Materials;
using Shared.Models.Brands.Responses;
using Shared.Models.BudgetItems;
using Shared.Models.BudgetItems.Nozzles.Responses;
using Shared.Models.Templates.Equipments.Responses;
using System.Globalization;

namespace Shared.Models.BudgetItems.Equipments.Responses
{
    public class EquipmentResponse : BaseResponse, IBudgetItemResponse
    {

        public Guid DeliverableId { get; set; }
        public Guid ProjectId { get; set; }
            
        public double Budget { get; set; }

       public string sBudget => string.Format(new CultureInfo("en-US"), "{0:C0}", Budget);

        public string Nomenclatore { get; set; } = string.Empty;
       

        public string UpadtePageName { get; set; } = StaticClass.Equipments.PageName.Update;

        public string Reference { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public MaterialEnum InternalMaterial { get; set; } = MaterialEnum.None;
        public MaterialEnum ExternalMaterial { get; set; } = MaterialEnum.None;

        public string Type { get; set; } = string.Empty;
        public string SubType { get; set; } = string.Empty;
        public string TagLetter { get; set; } = string.Empty;
        public BrandResponse BrandResponse { get; set; } = new();
        public string Brand => BrandResponse == null ? string.Empty : BrandResponse.Name;


        public string Tag => $"{TagLetter}-{TagNumber}";

        public string TagNumber { get; set; } = string.Empty;
        public bool ShowDetails { get; set; } = false;
        public List<NozzleResponse> Nozzles { get; set; } = new();

      
      
    }
}
