using Shared.Models.FileResults.Generics.Records;

namespace Shared.Models.DeliverableGanttTasks.Records
{
    public class GetAllBudgetItemNewGanttTask : IGetAll
    {
        public string EndPointName => StaticClass.BudgetItemNewGanttTasks.EndPoint.GetAll;
        public string Legend => "Get All Deliverables";
        public string ClassName => StaticClass.BudgetItemNewGanttTasks.ClassName;
        public string ActionType => "GetAll";
        public string Succesfully => StaticClass.ResponseMessages.ReponseSuccesfullyMessage(Legend, ClassName, ActionType);
        public string Fail => StaticClass.ResponseMessages.ReponseFailMessage(Legend, ClassName, ActionType);
        public string NotFound => StaticClass.ResponseMessages.ReponseNotFound(ClassName);
        public Guid? BudgetItemId { get; set; }
        public Guid? NewGanttTaskId { get; set; }

    }
}
