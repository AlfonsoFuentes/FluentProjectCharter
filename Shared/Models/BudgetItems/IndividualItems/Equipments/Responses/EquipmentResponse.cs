using Shared.Enums.BudgetItemTypes;
using Shared.Enums.CostCenter;
using Shared.Enums.Materials;
using Shared.Models.Brands.Responses;
using Shared.Models.BudgetItems.IndividualItems.Nozzles.Responses;
using Shared.Models.BudgetItems.Responses;
using Shared.Models.Templates.Equipments.Responses;
using System.Globalization;

namespace Shared.Models.BudgetItems.IndividualItems.Equipments.Responses
{
    public class EquipmentResponse : BudgetItemWithPurchaseOrdersResponse
    {

        public Guid DeliverableId { get; set; }



        public override string UpadtePageName { get; set; } = StaticClass.Equipments.PageName.Update;

        public string Reference { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public MaterialEnum InternalMaterial { get; set; } = MaterialEnum.None;
        public MaterialEnum ExternalMaterial { get; set; } = MaterialEnum.None;

        public string Type { get; set; } = string.Empty;
        public string SubType { get; set; } = string.Empty;
        public string TagLetter { get; set; } = string.Empty;
        public BrandResponse BrandResponse { get; set; } = new();
        public string Brand => BrandResponse == null ? string.Empty : BrandResponse.Name;


        public override string Tag => ShowProvisionalTag ? ProvisionalTag : !string.IsNullOrEmpty(TagLetter)
            && !string.IsNullOrEmpty(TagNumber) ? $"{TagLetter}-{TagNumber}" : string.Empty;

        public string TagNumber { get; set; } = string.Empty;
        public bool ShowDetails { get; set; } = false;
        public List<NozzleResponse> Nozzles { get; set; } = new();
        public bool IsExisting { get; set; }
        public string ProvisionalTag { get; set; } = string.Empty;
        public bool ShowProvisionalTag { get; set; } = false;

      

    }
}
