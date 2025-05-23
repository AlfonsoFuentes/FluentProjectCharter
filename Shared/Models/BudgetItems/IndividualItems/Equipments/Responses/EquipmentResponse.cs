using Shared.Models.BudgetItems.IndividualItems.Nozzles.Responses;
using Shared.Models.BudgetItems.Responses;
using Shared.Models.FileResults.Generics.Request;
using Shared.Models.Templates.Equipments.Responses;

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


        public override string Tag => ShowProvisionalTag ? ProvisionalTag :
            string.IsNullOrEmpty(Template.TagLetter) ? 
            string.Empty : 
            string.IsNullOrEmpty(TagNumber) ? 
            $"{Template.TagLetter}" :
            $"{Template.TagLetter}-{TagNumber}";

        public string TagNumber { get; set; } = string.Empty;

        public List<NozzleResponse> Nozzles { get; set; } = new();
        public bool IsExisting { get; set; }
        public string ProvisionalTag { get; set; } = string.Empty;
        public bool ShowProvisionalTag { get; set; } = false;

        public EquipmentTemplateResponse Template { get; set; } = new();

    }
}
