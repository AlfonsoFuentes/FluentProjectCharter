using Shared.Enums.BudgetItemTypes;
using Shared.Enums.CostCenter;
using Shared.Enums.Materials;
using Shared.Models.Brands.Responses;
using Shared.Models.BudgetItems.IndividualItems.Nozzles.Responses;
using Shared.Models.BudgetItems.Responses;
using Shared.Models.FileResults.Generics.Request;
using Shared.Models.Templates.Equipments.Responses;
using System.Globalization;

namespace Shared.Models.BudgetItems.IndividualItems.Equipments.Responses
{
    public class EquipmentResponse : BudgetItemWithPurchaseOrdersResponse, IMessageResponse, IRequest
    {


        public string EndPointName => StaticClass.Equipments.EndPoint.CreateUpdate;

        public string Legend => Name;

        public string ActionType => Id == Guid.Empty ? "created" : "updated";
        public string ClassName => StaticClass.Equipments.ClassName;
        public string Succesfully => StaticClass.ResponseMessages.ReponseSuccesfullyMessage(Legend, ClassName, ActionType);
        public string Fail => StaticClass.ResponseMessages.ReponseFailMessage(Legend, ClassName, ActionType);
        public string NotFound => StaticClass.ResponseMessages.ReponseNotFound(ClassName);

        public Guid? GanttTaskId { get; set; }



        public string Reference { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public MaterialEnum InternalMaterial { get; set; } = MaterialEnum.None;
        public MaterialEnum ExternalMaterial { get; set; } = MaterialEnum.None;

        public string Type { get; set; } = string.Empty;
        public string SubType { get; set; } = string.Empty;
        public string TagLetter { get; set; } = string.Empty;
        public BrandResponse? Brand { get; set; }
        public string BrandName => Brand == null ? string.Empty : Brand.Name;
        public Guid? BrandId => Brand == null ? null : Brand.Id;

        public override string Tag => ShowProvisionalTag ? ProvisionalTag : !string.IsNullOrEmpty(TagLetter)
            && !string.IsNullOrEmpty(TagNumber) ? $"{TagLetter}-{TagNumber}" : string.Empty;

        public string TagNumber { get; set; } = string.Empty;
    
        public List<NozzleResponse> Nozzles { get; set; } = new();
        public bool IsExisting { get; set; }
        public string ProvisionalTag { get; set; } = string.Empty;
        public bool ShowProvisionalTag { get; set; } = false;



    }
}
