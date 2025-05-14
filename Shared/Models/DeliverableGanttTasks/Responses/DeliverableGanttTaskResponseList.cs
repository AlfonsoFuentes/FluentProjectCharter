using Shared.Models.FileResults.Generics.Reponses;
using Shared.Models.FileResults.Generics.Request;
using System.Xml.Linq;

namespace Shared.Models.DeliverableGanttTasks.Responses
{
    public class DeliverableGanttTaskResponseList : IMessageResponse, IResponseAll, IRequest
    {
        public string ProjectName { get; set; } = string.Empty;
        public string EndPointName => StaticClass.DeliverableGanttTasks.EndPoint.UpdateAll;
        public Guid ProjectId { get; set; }
        public string Legend => ProjectName;
        public DateTime? InitialProjectStartDate { get; set; }
        public string ActionType => "updated";
        public string ClassName => StaticClass.DeliverableGanttTasks.ClassName;
        public string Succesfully => StaticClass.ResponseMessages.ReponseSuccesfullyMessage(Legend, ClassName, ActionType);
        public string Fail => StaticClass.ResponseMessages.ReponseFailMessage(Legend, ClassName, ActionType);
        public string NotFound => StaticClass.ResponseMessages.ReponseNotFound(ClassName);
        public List<DeliverableGanttTaskResponse> Items { get; set; } = new();
        public List<DeliverableGanttTaskResponse> OrderedItems => Items.OrderBy(x => x.MainOrder).ToList();
        public int LastMainOrder => OrderedItems.LastOrDefault()?.MainOrder ?? 0;
        public int FirstMainOrder => OrderedItems.FirstOrDefault()?.MainOrder ?? 0;

        public int LastDeliverableOrder => OrderedItems.LastOrDefault(x => x.IsDeliverable)?.MainOrder ?? 0;
        public int FirstDeliverableOrder => OrderedItems.FirstOrDefault(x => x.IsDeliverable)?.MainOrder ?? 0;
    }
}
